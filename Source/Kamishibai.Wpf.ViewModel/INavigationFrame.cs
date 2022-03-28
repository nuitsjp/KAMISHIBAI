namespace Kamishibai.Wpf.ViewModel;

public partial interface INavigationFrame
{
    public static readonly string DefaultFrameName = string.Empty;
    public Task<bool> NavigateAsync<TViewModel>() where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel) where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init) where TViewModel : class;
    public Task<bool> GoBackAsync();

}