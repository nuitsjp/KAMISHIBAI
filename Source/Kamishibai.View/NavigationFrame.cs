using System.Windows;
using System.Windows.Controls;

namespace Kamishibai;

public class NavigationFrame : ContentControl, INavigationFrame
{
    public static readonly DependencyProperty FrameNameProperty = DependencyProperty.Register(
        "FrameName", typeof(string), typeof(NavigationFrame), new PropertyMetadata(string.Empty));

    private readonly NavigationStack _pages = new();

    public NavigationFrame()
    {
        NavigationFrameProvider.AddNavigationFrame(this);
    }

    public event EventHandler<PausingEventArgs>? Pausing;
    public event EventHandler<NavigatingEventArgs>? Navigating;
    public event EventHandler<NavigatedEventArgs>? Navigated;
    public event EventHandler<PausedEventArgs>? Paused;
    public event EventHandler<DisposingEventArgs>? Disposing;
    public event EventHandler<ResumingEventArgs>? Resuming;
    public event EventHandler<ResumedEventArgs>? Resumed;
    public event EventHandler<DisposedEventArgs>? Disposed;

    public string FrameName
    {
        get => (string) GetValue(FrameNameProperty);
        set => SetValue(FrameNameProperty, value);
    }

    public int Count => _pages.Count;

    public FrameworkElement Current => _pages.Peek;

    public bool CanGoBack => _pages.CanPop;

    public Task<bool> NavigateAsync(Type viewModelType, IServiceProvider serviceProvider)
    {
        var view = GetPresentation(serviceProvider, viewModelType);
        var viewModel = serviceProvider.GetService(viewModelType)!;
        view.DataContext = viewModel;

        return NavigateAsync(view, viewModel);
    }

    public Task<bool> NavigateAsync<TViewModel>(IServiceProvider serviceProvider) where TViewModel : class
        => NavigateAsync(typeof(TViewModel), serviceProvider);

    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, IServiceProvider serviceProvider) where TViewModel : class
    {
        var view = GetPresentation(serviceProvider, typeof(TViewModel));
        view.DataContext = viewModel;

        return NavigateAsync(view, viewModel);
    }

    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, IServiceProvider serviceProvider) where TViewModel : class
    {
        var view = GetPresentation(serviceProvider, typeof(TViewModel));
        view.DataContext ??= serviceProvider.GetService(typeof(TViewModel));

        var viewModel = (TViewModel) view.DataContext!;
        init(viewModel);

        return NavigateAsync(view, viewModel);
    }

    private static FrameworkElement GetPresentation(IServiceProvider serviceProvider, Type viewModelType)
    {
        Type viewType = ViewTypeCache.GetViewType(viewModelType);

        var frameworkElement = (FrameworkElement)serviceProvider.GetService(viewType)!;

        return frameworkElement;
    }


    private async Task<bool> NavigateAsync(FrameworkElement view, object destinationViewModel)
    {
        _pages.TryPeek(out var  sourceView);
        var sourceViewModel = sourceView?.DataContext;


        if (await NotifyPausing(sourceViewModel, destinationViewModel) is false) return false;
        await NotifyNavigating(sourceViewModel, destinationViewModel);

        _pages.Push(view);
        Content = view;

        await NotifyNavigated(sourceViewModel, destinationViewModel);
        await NotifyPaused(sourceViewModel, destinationViewModel);

        return true;
    }

    public async Task<bool> GoBackAsync()
    {
        if (_pages.Count == 1)
        {
            return false;
        }

        var sourceView = _pages.Peek;
        var sourceViewModel = sourceView.DataContext;
        var destinationView = _pages.Previous;
        var destinationViewModel = destinationView.DataContext;


        if (!await NotifyDisposing(sourceViewModel, destinationViewModel)) return false;

        _pages.Pop();
        try
        {
            await NotifyResuming(sourceViewModel, destinationViewModel);

            Content = destinationView;

            await NotifyResumed(sourceViewModel, destinationViewModel);
            await NotifyDisposed(sourceViewModel, destinationViewModel);

            return true;
        }
        catch
        {
            _pages.Push(sourceView);
            Content = sourceView;
            throw;
        }
    }

    private async Task<bool> NotifyPausing(object? source, object destination)
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

        if (source is not null)
        {
            Pausing?.Invoke(this, new PausingEventArgs(FrameName, source, destination));
        }

        return true;
    }

    private async Task NotifyNavigating(object? source, object destination)
    {
        if (destination is INavigatingAsyncAware navigatingAsyncAware) await navigatingAsyncAware.OnNavigatingAsync();
        if (destination is INavigatingAware navigatingAware) navigatingAware.OnNavigating();
        Navigating?.Invoke(this, new NavigatingEventArgs(FrameName, source, destination));
    }

    private async Task NotifyNavigated(object? source, object destination)
    {
        if (destination is INavigatedAsyncAware navigatedAsyncAware) await navigatedAsyncAware.OnNavigatedAsync();
        if (destination is INavigatedAware navigatedAware) navigatedAware.OnNavigated();
        Navigated?.Invoke(this, new NavigatedEventArgs(FrameName, source, destination));
    }

    private async Task NotifyPaused(object? source, object destination)
    {
        if (source is IPausedAsyncAware pausedAsyncAware) await pausedAsyncAware.OnPausedAsync();
        if (source is IPausedAware pausedAware) pausedAware.OnPaused();
        if (source is not null)
        {
            Paused?.Invoke(this, new PausedEventArgs(FrameName, source, destination));
        }
    }

    private async Task<bool> NotifyDisposing(object sourceViewModel, object destinationViewModel)
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

        Disposing?.Invoke(this, new DisposingEventArgs(FrameName, sourceViewModel, destinationViewModel));
        return true;
    }

    private async Task NotifyResuming(object sourceViewModel, object destinationViewModel)
    {
        if (destinationViewModel is IResumingAsyncAware resumingAsyncAware) await resumingAsyncAware.OnResumingAsync();
        if (destinationViewModel is IResumingAware resumingAware) resumingAware.OnResuming();
        Resuming?.Invoke(this, new ResumingEventArgs(FrameName, sourceViewModel, destinationViewModel));
    }

    private async Task NotifyResumed(object sourceViewModel, object destinationViewModel)
    {
        if (destinationViewModel is IResumedAsyncAware resumedAsyncAware) await resumedAsyncAware.OnResumedAsync();
        if (destinationViewModel is IResumedAware resumedAware) resumedAware.OnResumed();
        Resumed?.Invoke(this, new ResumedEventArgs(FrameName, sourceViewModel, destinationViewModel));
    }

    private async Task NotifyDisposed(object sourceViewModel, object destinationViewModel)
    {
        if (sourceViewModel is IDisposedAsyncAware disposedAsyncAware) await disposedAsyncAware.OnDisposedAsync();
        if (sourceViewModel is IDisposable disposable) disposable.Dispose();
        Disposed?.Invoke(this, new DisposedEventArgs(FrameName, sourceViewModel, destinationViewModel));
    }

    public IDisposable Subscribe(IObserver<object> observer)
    {
        return _pages.Subscribe(new PageObserver(observer));
    }

    private class PageObserver : IObserver<FrameworkElement>
    {
        private readonly IObserver<object> _observer;

        public PageObserver(IObserver<object> observer)
        {
            _observer = observer;
        }

        public void OnCompleted() => _observer.OnCompleted();

        public void OnError(Exception error) => _observer.OnError(error);

        public void OnNext(FrameworkElement value) => _observer.OnNext(value.DataContext);
    }
}