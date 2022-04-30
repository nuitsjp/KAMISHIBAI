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

    public event EventHandler<PreForwardEventArgs>? Pausing;
    public event EventHandler<PreForwardEventArgs>? Navigating;
    public event EventHandler<PostForwardEventArgs>? Navigated;
    public event EventHandler<PostForwardEventArgs>? Paused;
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

        PreForwardEventArgs preForwardEventArgs = new(FrameName, sourceViewModel, destinationViewModel);
        if (await NotifyPausing(preForwardEventArgs) is false) return false;
        if (await NotifyNavigating(preForwardEventArgs) is false) return false;

        _pages.Push(view);
        Content = view;

        PostForwardEventArgs postForwardEventArgs = new(FrameName, sourceViewModel, destinationViewModel);
        await NotifyNavigated(postForwardEventArgs);
        await NotifyPaused(postForwardEventArgs);

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

    private async Task<bool> NotifyPausing(PreForwardEventArgs args)
    {
        if (args.SourceViewModel is IPausingAsyncAware pausingAsyncAware)
        {
            await pausingAsyncAware.OnPausingAsync(args);
            if (args.Cancel)
            {
                return false;
            }
        }

        if (args.SourceViewModel is IPausingAware pausingAware)
        {
            pausingAware.OnPausing(args);
            if (args.Cancel)
            {
                return false;
            }
        }

        if (args.SourceViewModel is not null)
        {
            Pausing?.Invoke(this, args);
        }

        return true;
    }

    private async Task<bool> NotifyNavigating(PreForwardEventArgs args)
    {
        if (args.DestinationViewModel is INavigatingAsyncAware navigatingAsyncAware)
        {
            await navigatingAsyncAware.OnNavigatingAsync(args);
            if (args.Cancel)
            {
                return false;
            }
        }

        if (args.DestinationViewModel is INavigatingAware navigatingAware)
        {
            navigatingAware.OnNavigating(args);
            if (args.Cancel)
            {
                return false;
            }
        }

        Navigating?.Invoke(this, args);

        return true;
    }

    private async Task NotifyNavigated(PostForwardEventArgs args)
    {
        if (args.DestinationViewModel is INavigatedAsyncAware navigatedAsyncAware) await navigatedAsyncAware.OnNavigatedAsync(args);
        if (args.DestinationViewModel is INavigatedAware navigatedAware) navigatedAware.OnNavigated(args);
        Navigated?.Invoke(this, args);
    }

    private async Task NotifyPaused(PostForwardEventArgs args)
    {
        if (args.SourceViewModel is IPausedAsyncAware pausedAsyncAware) await pausedAsyncAware.OnPausedAsync(args);
        if (args.SourceViewModel is IPausedAware pausedAware) pausedAware.OnPaused(args);
        if (args.SourceViewModel is not null)
        {
            Paused?.Invoke(this, args);
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