namespace Kamishibai;

public interface INavigationFrame : IObservable<object>
{
    public event EventHandler<PreForwardEventArgs>? Pausing;
    public event EventHandler<PreForwardEventArgs>? Navigating;
    public event EventHandler<PostForwardEventArgs>? Navigated;
    public event EventHandler<PostForwardEventArgs>? Paused;

    public event EventHandler<DisposingEventArgs>? Disposing;
    public event EventHandler<ResumingEventArgs>? Resuming;
    public event EventHandler<ResumedEventArgs>? Resumed;
    public event EventHandler<DisposedEventArgs>? Disposed;

    public string FrameName { get; }
    public int Count { get; }
    public bool CanGoBack { get; }
    public Task<bool> NavigateAsync(Type viewModelType, IServiceProvider serviceProvider);
    public Task<bool> NavigateAsync<TViewModel>(IServiceProvider serviceProvider) where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, IServiceProvider serviceProvider) where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, IServiceProvider serviceProvider) where TViewModel : class;
    public Task<bool> GoBackAsync();

}