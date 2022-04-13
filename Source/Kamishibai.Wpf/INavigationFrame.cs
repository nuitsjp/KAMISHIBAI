namespace Kamishibai.Wpf;

public interface INavigationFrame
{
    public string FrameName { get; }
    public Task<bool> NavigateAsync(Type viewModelType, IServiceProvider serviceProvider);
    public Task<bool> NavigateAsync<TViewModel>(IServiceProvider serviceProvider) where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, IServiceProvider serviceProvider) where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, IServiceProvider serviceProvider) where TViewModel : class;
    public Task<bool> GoBackAsync();

}