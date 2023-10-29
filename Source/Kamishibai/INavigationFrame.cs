namespace Kamishibai;

public interface INavigationFrame : IObservable<object>
{
    event EventHandler<PreForwardEventArgs>? Pausing;
    event EventHandler<PreForwardEventArgs>? Navigating;
    event EventHandler<PostForwardEventArgs>? Navigated;
    event EventHandler<PostForwardEventArgs>? Paused;

    event EventHandler<PreBackwardEventArgs>? Disposing;
    event EventHandler<PreBackwardEventArgs>? Resuming;
    event EventHandler<PostBackwardEventArgs>? Resumed;
    event EventHandler<PostBackwardEventArgs>? Disposed;

    string FrameName { get; }
    int Count { get; }
    bool CanGoBack { get; }
    object CurrentDataContext { get; }
    Task<bool> NavigateAsync(Type viewModelType, IServiceProvider serviceProvider);
    Task<bool> NavigateAsync<TViewModel>(IServiceProvider serviceProvider);
    Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, IServiceProvider serviceProvider) where TViewModel : notnull;
    Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, IServiceProvider serviceProvider);
    Task<bool> GoBackAsync();

}