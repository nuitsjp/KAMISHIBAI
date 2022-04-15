namespace Kamishibai.Wpf;

public class NavigationServiceBase : INavigationServiceBase
{
    private readonly INavigationFrameProvider _navigationFrameProvider;
    private readonly IServiceProvider _serviceProvider;

    public NavigationServiceBase(IServiceProvider serviceProvider, INavigationFrameProvider navigationFrameProvider)
    {
        _serviceProvider = serviceProvider;
        _navigationFrameProvider = navigationFrameProvider;
    }

    public Task<bool> NavigateAsync(Type viewModelType, string frameName = "")
    {
        return _navigationFrameProvider
            .GetNavigationFrame(frameName)
            .NavigateAsync(viewModelType, _serviceProvider);
    }

    public Task<bool> NavigateAsync<TViewModel>(string frameName = "") where TViewModel : class
    {
        return _navigationFrameProvider
            .GetNavigationFrame(frameName)
            .NavigateAsync<TViewModel>(_serviceProvider);
    }

    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, string frameName = "") where TViewModel : class
    {
        return _navigationFrameProvider
            .GetNavigationFrame(frameName)
            .NavigateAsync(viewModel, _serviceProvider);
    }

    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, string frameName = "") where TViewModel : class
    {
        return _navigationFrameProvider
            .GetNavigationFrame(frameName)
            .NavigateAsync(init, _serviceProvider);
    }

    public Task<bool> GoBackAsync(string frameName = "")
    {
        return _navigationFrameProvider
            .GetNavigationFrame(frameName)
            .GoBackAsync();
    }

    bool INavigationServiceBase.CanGoBack(string frameName) => _navigationFrameProvider.GetNavigationFrame(frameName).CanGoBack;

    public INavigationFrame GetNavigationFrame(string frameName = "") =>
        _navigationFrameProvider.GetNavigationFrame(frameName);
}