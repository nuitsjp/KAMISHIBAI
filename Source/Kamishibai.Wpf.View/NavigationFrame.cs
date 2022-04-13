using System.Windows;
using System.Windows.Controls;

namespace Kamishibai.Wpf.View;

public class NavigationFrame : Grid, INavigationFrame
{
    public static readonly DependencyProperty FrameNameProperty = DependencyProperty.Register(
        "FrameName", typeof(string), typeof(NavigationFrame), new PropertyMetadata(string.Empty));

    private readonly Stack<FrameworkElement> _pages = new();

    public NavigationFrame()
    {
        NavigationFrameProvider.AddNavigationFrame(this);
    }

    public string FrameName
    {
        get => (string) GetValue(FrameNameProperty);
        set => SetValue(FrameNameProperty, value);
    }

    private object CurrentViewModel => 
        _pages.TryPeek(out var currentView) 
            ? currentView.DataContext 
            : throw new InvalidOperationException();

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


    private async Task<bool> NavigateAsync(FrameworkElement view, object destination, INavigationHandler navigationHandler)
    {
        var source = CurrentViewModel;

        if (await NotifyPausing(source, destination, navigationHandler) is false) return false;
        await NotifyNavigating(destination, navigationHandler, source);

        _pages.Push(view);
        Children.Clear();
        Children.Add(view);

        await NotifyNavigated(destination, navigationHandler, source);
        await NotifyPaused(destination, navigationHandler, source);

        return true;
    }

    public async Task<bool> GoBackAsync(INavigationHandler navigationHandler)
    {
        if (_pages.Count == 1)
        {
            return false;
        }

        var sourceView = _pages.Peek();
        var sourceViewModel = sourceView.DataContext;

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

        _pages.Pop();
        var destinationView = _pages.Peek();
        var destinationViewModel = destinationView.DataContext;
        try
        {
            if (destinationViewModel is IResumingAsyncAware resumingAsyncAware) await resumingAsyncAware.OnResumingAsync();
            if (destinationViewModel is IResumingAware resumingAware) resumingAware.OnResuming();

            Children.Clear();
            Children.Add(destinationView);

            if (destinationViewModel is IResumedAsyncAware resumedAsyncAware) await resumedAsyncAware.OnResumedAsync();
            if (destinationViewModel is IResumedAware resumedAware) resumedAware.OnResumed();
            if (sourceViewModel is IDisposedAsyncAware disposedAsyncAware) await disposedAsyncAware.OnDisposedAsync();
            if (sourceViewModel is IDisposable disposable) disposable.Dispose();

            return true;
        }
        catch
        {
            _pages.Push(sourceView);
            Children.Clear();
            Children.Add(sourceView);
            throw;
        }
    }

    private async Task<bool> NotifyPausing(object source, object destination, INavigationHandler navigationHandler)
    {
        if (CurrentViewModel is IPausingAsyncAware pausingAsyncAware)
        {
            if (await pausingAsyncAware.OnPausingAsync() is false)
            {
                return false;
            }
        }

        if (CurrentViewModel is IPausingAware pausingAware)
        {
            if (pausingAware.OnPausing() is false)
            {
                return false;
            }
        }

        navigationHandler.OnPausing(source, destination);

        return true;
    }

    private static async Task NotifyNavigating(object destination, INavigationHandler navigationHandler, object source)
    {
        if (destination is INavigatingAsyncAware navigatingAsyncAware) await navigatingAsyncAware.OnNavigatingAsync();
        if (destination is INavigatingAware navigatingAware) navigatingAware.OnNavigating();
        navigationHandler.OnNavigating(source, destination);
    }

    private static async Task NotifyNavigated(object destination, INavigationHandler navigationHandler, object source)
    {
        if (destination is INavigatedAsyncAware navigatedAsyncAware) await navigatedAsyncAware.OnNavigatedAsync();
        if (destination is INavigatedAware navigatedAware) navigatedAware.OnNavigated();
        navigationHandler.OnNavigated(source, destination);
    }

    private async Task NotifyPaused(object destination, INavigationHandler navigationHandler, object source)
    {
        if (CurrentViewModel is IPausedAsyncAware pausedAsyncAware) await pausedAsyncAware.OnPausedAsync();
        if (CurrentViewModel is IPausedAware pausedAware) pausedAware.OnPaused();
        navigationHandler.OnPaused(source, destination);
    }


}