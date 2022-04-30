namespace Kamishibai;

public interface INavigationFrame : IObservable<object>
{
    public event EventHandler<PreForwardEventArgs>? Pausing;
    public event EventHandler<PreForwardEventArgs>? Navigating;
    public event EventHandler<PostForwardEventArgs>? Navigated;
    public event EventHandler<PostForwardEventArgs>? Paused;

    public event EventHandler<PreBackwardEventArgs>? Disposing;
    public event EventHandler<PreBackwardEventArgs>? Resuming;
    public event EventHandler<PostBackwardEventArgs>? Resumed;
    public event EventHandler<PostBackwardEventArgs>? Disposed;

    public string FrameName { get; }
    public int Count { get; }
    public bool CanGoBack { get; }
    public Task<bool> NavigateAsync(Type viewModelType, IServiceProvider serviceProvider);
    public Task<bool> NavigateAsync<TViewModel>(IServiceProvider serviceProvider) where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, IServiceProvider serviceProvider) where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, IServiceProvider serviceProvider) where TViewModel : class;
    public Task<bool> GoBackAsync();

}