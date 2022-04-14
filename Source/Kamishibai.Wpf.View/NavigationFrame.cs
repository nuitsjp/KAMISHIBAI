using System.Windows;
using System.Windows.Controls;

namespace Kamishibai.Wpf.View;

public class NavigationFrame : Grid, INavigationFrame
{
    public static readonly DependencyProperty FrameNameProperty = DependencyProperty.Register(
        "FrameName", typeof(string), typeof(NavigationFrame), new PropertyMetadata(string.Empty));

    private readonly List<FrameworkElement> _pages = new();

    public NavigationFrame()
    {
        NavigationFrameProvider.AddNavigationFrame(this);
    }

    public string FrameName
    {
        get => (string) GetValue(FrameNameProperty);
        set => SetValue(FrameNameProperty, value);
    }

    private object CurrentViewModel => _pages.Current();

    public Task<bool> NavigateAsync(Type viewModelType, IServiceProvider serviceProvider, INavigationHandler navigationHandler)
    {
        throw new NotImplementedException();
    }

    public Task<bool> NavigateAsync<TViewModel>(IServiceProvider serviceProvider, INavigationHandler navigationHandler) where TViewModel : class
    {
        throw new NotImplementedException();
    }

    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, IServiceProvider serviceProvider, INavigationHandler navigationHandler) where TViewModel : class
    {
        var view = GetPresentation<TViewModel>(serviceProvider);
        view.DataContext = viewModel;

        return NavigateAsync(view, viewModel, navigationHandler);
    }

    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, IServiceProvider serviceProvider, INavigationHandler navigationHandler) where TViewModel : class
    {
        var view = GetPresentation<TViewModel>(serviceProvider);
        var viewModel = (TViewModel) view.DataContext;
        init(viewModel);

        return NavigateAsync(view, viewModel, navigationHandler);
    }

    private static FrameworkElement GetPresentation<TViewModel>(IServiceProvider serviceProvider) where TViewModel : class
    {
        ViewType? viewType = ViewTypeCache<TViewModel>.ViewType;
        if (viewType is null)
        {
            throw new InvalidOperationException($"View matching the {typeof(TViewModel)} has not been registered.");
        }

        var frameworkElement = (FrameworkElement)serviceProvider.GetService(viewType.Type)!;
        if (viewType.AssignViewModel)
        {
            frameworkElement.DataContext ??= (TViewModel)serviceProvider.GetService(typeof(TViewModel))!;
        }

        return frameworkElement;
    }


    private async Task<bool> NavigateAsync(FrameworkElement view, object destinationViewModel, INavigationHandler navigationHandler)
    {
        var sourceView = _pages.Any()
            ? _pages.Current()
            : null;
        var sourceViewModel = sourceView?.DataContext;


        if (await NotifyPausing(sourceViewModel, destinationViewModel, navigationHandler) is false) return false;
        await NotifyNavigating(sourceViewModel, destinationViewModel, navigationHandler);

        _pages.Add(view);
        Children.Clear();
        Children.Add(view);

        await NotifyNavigated(sourceViewModel, destinationViewModel, navigationHandler);
        await NotifyPaused(sourceViewModel, destinationViewModel, navigationHandler);

        return true;
    }

    public async Task<bool> GoBackAsync(INavigationHandler navigationHandler)
    {
        if (_pages.Count == 1)
        {
            return false;
        }

        var sourceView = _pages.Current();
        var sourceViewModel = sourceView.DataContext;
        var destinationView = _pages.Previous();
        var destinationViewModel = destinationView.DataContext;


        if (!await NotifyDisposing(sourceViewModel, destinationViewModel, navigationHandler)) return false;

        _pages.RemoveCurrent();
        try
        {
            await NotifyResuming(sourceViewModel, destinationViewModel, navigationHandler);

            Children.Clear();
            Children.Add(destinationView);

            await NotifyResumed(sourceViewModel, destinationViewModel, navigationHandler);
            await NotifyDisposed(sourceViewModel, destinationViewModel, navigationHandler);

            return true;
        }
        catch
        {
            _pages.Add(sourceView);
            Children.Clear();
            Children.Add(sourceView);
            throw;
        }
    }

    private async Task<bool> NotifyPausing(object? source, object destination, INavigationHandler navigationHandler)
    {
        if (source is IPausingAsyncAware pausingAsyncAware)
        {
            if (await pausingAsyncAware.OnPausingAsync() is false)
            {
                return false;
            }
        }

        if (source is IPausingAware pausingAware)
        {
            if (pausingAware.OnPausing() is false)
            {
                return false;
            }
        }

        navigationHandler.OnPausing(source, destination);

        return true;
    }

    private static async Task NotifyNavigating(object? source, object destination, INavigationHandler navigationHandler)
    {
        if (destination is INavigatingAsyncAware navigatingAsyncAware) await navigatingAsyncAware.OnNavigatingAsync();
        if (destination is INavigatingAware navigatingAware) navigatingAware.OnNavigating();
        navigationHandler.OnNavigating(source, destination);
    }

    private static async Task NotifyNavigated(object? source, object destination, INavigationHandler navigationHandler)
    {
        if (destination is INavigatedAsyncAware navigatedAsyncAware) await navigatedAsyncAware.OnNavigatedAsync();
        if (destination is INavigatedAware navigatedAware) navigatedAware.OnNavigated();
        navigationHandler.OnNavigated(source, destination);
    }

    private async Task NotifyPaused(object? source, object destination, INavigationHandler navigationHandler)
    {
        if (source is IPausedAsyncAware pausedAsyncAware) await pausedAsyncAware.OnPausedAsync();
        if (source is IPausedAware pausedAware) pausedAware.OnPaused();
        navigationHandler.OnPaused(source, destination);
    }

    private static async Task<bool> NotifyDisposing(object sourceViewModel, object destinationViewModel, INavigationHandler navigationHandler)
    {
        if (sourceViewModel is IDisposingAsyncAware disposingAsyncAware)
        {
            if (await disposingAsyncAware.OnDisposingAsync() is false)
            {
                return false;
            }
        }

        if (sourceViewModel is IDisposingAware disposingAware)
        {
            if (disposingAware.OnDisposing() is false)
            {
                return false;
            }
        }

        navigationHandler.OnDisposing(sourceViewModel, destinationViewModel);
        return true;
    }

    private static async Task NotifyResuming(object sourceViewModel, object destinationViewModel,
        INavigationHandler navigationHandler)
    {
        if (destinationViewModel is IResumingAsyncAware resumingAsyncAware) await resumingAsyncAware.OnResumingAsync();
        if (destinationViewModel is IResumingAware resumingAware) resumingAware.OnResuming();
        navigationHandler.OnResuming(sourceViewModel, destinationViewModel);
    }

    private static async Task NotifyResumed(object sourceViewModel, object destinationViewModel,
        INavigationHandler navigationHandler)
    {
        if (destinationViewModel is IResumedAsyncAware resumedAsyncAware) await resumedAsyncAware.OnResumedAsync();
        if (destinationViewModel is IResumedAware resumedAware) resumedAware.OnResumed();
        navigationHandler.OnResumed(sourceViewModel, destinationViewModel);
    }

    private static async Task NotifyDisposed(object sourceViewModel, object destinationViewModel,
        INavigationHandler navigationHandler)
    {
        if (sourceViewModel is IDisposedAsyncAware disposedAsyncAware) await disposedAsyncAware.OnDisposedAsync();
        if (sourceViewModel is IDisposable disposable) disposable.Dispose();
        navigationHandler.OnDisposed(sourceViewModel, destinationViewModel);
    }

}