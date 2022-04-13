namespace Kamishibai.Wpf;

public interface INavigationFrame
{
    public string FrameName { get; }
    public Task<bool> NavigateAsync(Type viewModelType, IServiceProvider serviceProvider, INavigationHandler navigationHandler);
    public Task<bool> NavigateAsync<TViewModel>(IServiceProvider serviceProvider, INavigationHandler navigationHandler) where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, IServiceProvider serviceProvider, INavigationHandler navigationHandler) where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, IServiceProvider serviceProvider, INavigationHandler navigationHandler) where TViewModel : class;
    public Task<bool> GoBackAsync(INavigationHandler navigationHandler);

}