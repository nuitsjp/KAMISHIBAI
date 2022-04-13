namespace Kamishibai.Wpf;

public class NavigationServiceBase : INavigationServiceBase, INavigationHandler
{
    private readonly INavigationFrameProvider _navigationFrameProvider;
    private readonly IServiceProvider _serviceProvider;

    public event EventHandler<PausingEventArgs>? Pausing;
    public event EventHandler<NavigatingEventArgs>? Navigating;
    public event EventHandler<NavigatedEventArgs>? Navigated;
    public event EventHandler<PausedEventArgs>? Paused;
    public event EventHandler<DisposingEventArgs>? Disposing;
    public event EventHandler<ResumingEventArgs>? Resuming;
    public event EventHandler<ResumedEventArgs>? Resumed;
    public event EventHandler<DisposedEventArgs>? Disposed;

    public bool CanGoBack { get; } = false;

    public NavigationServiceBase(IServiceProvider serviceProvider, INavigationFrameProvider navigationFrameProvider)
    {
        _serviceProvider = serviceProvider;
        _navigationFrameProvider = navigationFrameProvider;
    }

    public Task<bool> NavigateAsync(Type viewModelType, string frameName = "")
    {
        return _navigationFrameProvider
            .GetNavigationFrame(frameName)
            .NavigateAsync(viewModelType, _serviceProvider, this);
    }

    public Task<bool> NavigateAsync<TViewModel>(string frameName = "") where TViewModel : class
    {
        return _navigationFrameProvider
            .GetNavigationFrame(frameName)
            .NavigateAsync<TViewModel>(_serviceProvider, this);
    }

    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, string frameName = "") where TViewModel : class
    {
        return _navigationFrameProvider
            .GetNavigationFrame(frameName)
            .NavigateAsync(viewModel, _serviceProvider, this);
    }

    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, string frameName = "") where TViewModel : class
    {
        return _navigationFrameProvider
            .GetNavigationFrame(frameName)
            .NavigateAsync(init, _serviceProvider, this);
    }

    public Task<bool> GoBackAsync(string frameName = "")
    {
        return _navigationFrameProvider
            .GetNavigationFrame(frameName)
            .GoBackAsync(this);
    }

    public void OnPausing(object sourceViewModel, object destinationViewModel)
        => Pausing?.Invoke(this, new(sourceViewModel, destinationViewModel));

    public void OnNavigating(object sourceViewModel, object destinationViewModel)
        => Navigating?.Invoke(this, new(sourceViewModel, destinationViewModel));

    public void OnNavigated(object sourceViewModel, object destinationViewModel)
        => Navigated?.Invoke(this, new(sourceViewModel, destinationViewModel));

    public void OnPaused(object sourceViewModel, object destinationViewModel)
        => Paused?.Invoke(this, new(sourceViewModel, destinationViewModel));

    public void OnDisposing(object sourceViewModel, object destinationViewModel)
        => Disposing?.Invoke(this, new(sourceViewModel, destinationViewModel));

    public void OnResuming(object sourceViewModel, object destinationViewModel)
        => Resuming?.Invoke(this, new(sourceViewModel, destinationViewModel));

    public void OnResumed(object sourceViewModel, object destinationViewModel)
        => Resumed?.Invoke(this, new(sourceViewModel, destinationViewModel));

    public void OnDisposed(object sourceViewModel, object destinationViewModel)
        => Disposed?.Invoke(this, new(sourceViewModel, destinationViewModel));
}