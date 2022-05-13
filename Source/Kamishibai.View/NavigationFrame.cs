using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Kamishibai;

public class NavigationFrame : ContentControl, INavigationFrame
{
    public static readonly DependencyProperty ExitForwardStoryboardProperty = DependencyProperty.Register(
        "ExitForwardStoryboard", typeof(Storyboard), typeof(NavigationFrame), new PropertyMetadata(default(Storyboard)));

    public static readonly DependencyProperty EntryForwardStoryboardProperty = DependencyProperty.Register(
        "EntryForwardStoryboard", typeof(Storyboard), typeof(NavigationFrame), new PropertyMetadata(default(Storyboard)));

    public static readonly DependencyProperty ExitBackwardStoryboardProperty = DependencyProperty.Register(
        "ExitBackwardStoryboard", typeof(Storyboard), typeof(NavigationFrame), new PropertyMetadata(default(Storyboard?)));

    public static readonly DependencyProperty EntryBackwardStoryboardProperty = DependencyProperty.Register(
        "EntryBackwardStoryboard", typeof(Storyboard), typeof(NavigationFrame), new PropertyMetadata(default(Storyboard)));

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
    public event EventHandler<PreBackwardEventArgs>? Disposing;
    public event EventHandler<PreBackwardEventArgs>? Resuming;
    public event EventHandler<PostBackwardEventArgs>? Resumed;
    public event EventHandler<PostBackwardEventArgs>? Disposed;

    public Storyboard? ExitForwardStoryboard
    {
        get => (Storyboard?) GetValue(ExitForwardStoryboardProperty);
        set => SetValue(ExitForwardStoryboardProperty, value);
    }

    public Storyboard? EntryForwardStoryboard
    {
        get => (Storyboard?) GetValue(EntryForwardStoryboardProperty);
        set => SetValue(EntryForwardStoryboardProperty, value);
    }

    public Storyboard? ExitBackwardStoryboard
    {
        get => (Storyboard?) GetValue(ExitBackwardStoryboardProperty);
        set => SetValue(ExitBackwardStoryboardProperty, value);
    }

    public Storyboard? EntryBackwardStoryboard
    {
        get => (Storyboard) GetValue(EntryBackwardStoryboardProperty);
        set => SetValue(EntryBackwardStoryboardProperty, value);
    }

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

    public Task<bool> NavigateAsync<TViewModel>(IServiceProvider serviceProvider)
        => NavigateAsync(typeof(TViewModel), serviceProvider);

    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, IServiceProvider serviceProvider) where TViewModel : notnull
    {
        var view = GetPresentation(serviceProvider, typeof(TViewModel));
        view.DataContext = viewModel;

        return NavigateAsync(view, viewModel);
    }

    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, IServiceProvider serviceProvider)
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
        _pages.TryPeek(out var sourceView);
        var sourceViewModel = sourceView?.DataContext;

        Task? exitTask = null;
        if (sourceView is not null)
        {
            exitTask = ExitForwardStoryboard?.BeginAsync();
        }

        PreForwardEventArgs preForwardEventArgs = new(FrameName, sourceViewModel, destinationViewModel);
        if (await NotifyPausing(preForwardEventArgs) is false) return false;
        if (await NotifyNavigating(preForwardEventArgs) is false) return false;

        if (exitTask is not null) await exitTask;

        _pages.Push(view);
        Content = view;

        Task? entryTask = EntryForwardStoryboard?.BeginAsync();

        PostForwardEventArgs postForwardEventArgs = new(FrameName, sourceViewModel, destinationViewModel);
        await NotifyNavigated(postForwardEventArgs);
        await NotifyPaused(postForwardEventArgs);

        if (entryTask is not null) await entryTask;

        return true;
    }

    public async Task<bool> GoBackAsync()
    {
        if (_pages.Count == 1)
        {
            return false;
        }

        Task? exitTask = ExitBackwardStoryboard?.BeginAsync();

        var sourceView = _pages.Peek;
        var sourceViewModel = sourceView.DataContext;
        var destinationView = _pages.Previous;
        var destinationViewModel = destinationView.DataContext;

        PreBackwardEventArgs preBackwardEventArgs = new(FrameName, sourceViewModel, destinationViewModel);
        if (!await NotifyDisposing(preBackwardEventArgs)) return false;
        if (!await NotifyResuming(preBackwardEventArgs)) return false;

        if (exitTask is not null) await exitTask;

        _pages.Pop();
        try
        {
            Content = destinationView;

            Task? entryTask = EntryBackwardStoryboard?.BeginAsync();

            PostBackwardEventArgs postBackwardEventArgs = new(FrameName, sourceViewModel, destinationViewModel);
            await NotifyResumed(postBackwardEventArgs);
            await NotifyDisposed(postBackwardEventArgs);

            if (entryTask is not null) await entryTask;

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

    private async Task<bool> NotifyDisposing(PreBackwardEventArgs args)
    {
        if (args.SourceViewModel is IDisposingAsyncAware disposingAsyncAware)
        {
            await disposingAsyncAware.OnDisposingAsync(args);
            if (args.Cancel)
            {
                return false;
            }
        }

        if (args.SourceViewModel is IDisposingAware disposingAware)
        {
            disposingAware.OnDisposing(args);
            if (args.Cancel)
            {
                return false;
            }
        }

        Disposing?.Invoke(this, args);
        return true;
    }

    private async Task<bool> NotifyResuming(PreBackwardEventArgs args)
    {
        if (args.DestinationViewModel is IResumingAsyncAware resumingAsyncAware)
        {
            await resumingAsyncAware.OnResumingAsync(args);
            if (args.Cancel)
            {
                return false;
            }
        }

        if (args.DestinationViewModel is IResumingAware resumingAware)
        {
            resumingAware.OnResuming(args);
            if (args.Cancel)
            {
                return false;
            }
        }

        Resuming?.Invoke(this, args);
        return true;
    }

    private async Task NotifyResumed(PostBackwardEventArgs args)
    {
        if (args.DestinationViewModel is IResumedAsyncAware resumedAsyncAware) await resumedAsyncAware.OnResumedAsync(args);
        if (args.DestinationViewModel is IResumedAware resumedAware) resumedAware.OnResumed(args);
        Resumed?.Invoke(this, args);
    }

    private async Task NotifyDisposed(PostBackwardEventArgs args)
    {
        if (args.SourceViewModel is IDisposedAsyncAware disposedAsyncAware) await disposedAsyncAware.OnDisposedAsync(args);
        if (args.SourceViewModel is IDisposedAware disposedAware) disposedAware.OnDisposed(args);
        if (args.SourceViewModel is IDisposable disposable) disposable.Dispose();
        Disposed?.Invoke(this, args);
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