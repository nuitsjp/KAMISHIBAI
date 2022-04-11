using Kamishibai.Wpf.ViewModel;

namespace Kamishibai.Wpf.Demo.ViewModel;

public class NavigationService : INavigationService
{
    private readonly INavigationFrameProvider _navigationFrameProvider;
    private readonly IServiceProvider _serviceProvider;

    public NavigationService(IServiceProvider serviceProvider, INavigationFrameProvider navigationFrameProvider)
    {
        _serviceProvider = serviceProvider;
        _navigationFrameProvider = navigationFrameProvider;
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

    public Task<bool> NavigateToSafeContentPage(int count, string frameName)
    {
        return NavigateAsync(
            new ContentPageViewModel(count, frameName, this), 
            frameName);
    }

    public Task<bool> GoBackAsync(string frameName = "")
    {
        return _navigationFrameProvider
            .GetNavigationFrame(frameName)
            .GoBackAsync();
    }
}