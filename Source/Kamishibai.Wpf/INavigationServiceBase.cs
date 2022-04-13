namespace Kamishibai.Wpf;

public interface INavigationServiceBase
{
    public bool CanGoBack { get; }
    public Task<bool> NavigateAsync(Type viewModelType, string frameName = "");
    public Task<bool> NavigateAsync<TViewModel>(string frameName = "") where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, string frameName = "") where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, string frameName = "") where TViewModel : class;
    Task<bool> GoBackAsync(string frameName = "");

}