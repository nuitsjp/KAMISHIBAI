namespace Kamishibai.Wpf;

public interface INavigationFrame : IObservable<object>
{
    public event EventHandler<PausingEventArgs>? Pausing;
    public event EventHandler<NavigatingEventArgs>? Navigating;
    public event EventHandler<NavigatedEventArgs>? Navigated;
    public event EventHandler<PausedEventArgs>? Paused;

    public event EventHandler<DisposingEventArgs>? Disposing;
    public event EventHandler<ResumingEventArgs>? Resuming;
    public event EventHandler<ResumedEventArgs>? Resumed;
    public event EventHandler<DisposedEventArgs>? Disposed;

    public string FrameName { get; }
    public Task<bool> NavigateAsync(Type viewModelType, IServiceProvider serviceProvider);
    public Task<bool> NavigateAsync<TViewModel>(IServiceProvider serviceProvider) where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, IServiceProvider serviceProvider) where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, IServiceProvider serviceProvider) where TViewModel : class;
    public Task<bool> GoBackAsync();

}