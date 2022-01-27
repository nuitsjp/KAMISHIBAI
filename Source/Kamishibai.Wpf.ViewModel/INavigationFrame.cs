namespace Kamishibai.Wpf.ViewModel;

public partial interface INavigationFrame
{
    public static readonly string DefaultFrameName = string.Empty;
    public Task<bool> TryNavigateAsync<TViewModel>() where TViewModel : class;
    public Task<bool> TryNavigateAsync<TViewModel>(TViewModel viewModel) where TViewModel : class;
    public Task<bool> TryNavigateAsync<TViewModel>(Action<TViewModel> init) where TViewModel : class;
    public Task<bool> GoBackAsync();

}