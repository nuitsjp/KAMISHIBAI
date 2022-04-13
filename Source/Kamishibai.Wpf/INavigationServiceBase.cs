namespace Kamishibai.Wpf;

public interface INavigationServiceBase
{
    public event EventHandler<PausingEventArgs>? Pausing;
    public event EventHandler<NavigatingEventArgs>? Navigating;
    public event EventHandler<NavigatedEventArgs>? Navigated;
    public event EventHandler<PausedEventArgs>? Paused;

    public event EventHandler<DisposingEventArgs>? Disposing;
    public event EventHandler<ResumingEventArgs>? Resuming;
    public event EventHandler<ResumedEventArgs>? Resumed;
    public event EventHandler<DisposedEventArgs>? Disposed;

    public bool CanGoBack { get; }
    public Task<bool> NavigateAsync(Type viewModelType, string frameName = "");
    public Task<bool> NavigateAsync<TViewModel>(string frameName = "") where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, string frameName = "") where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, string frameName = "") where TViewModel : class;
    Task<bool> GoBackAsync(string frameName = "");
}