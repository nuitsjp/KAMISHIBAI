namespace Kamishibai.Wpf;

public class PresentationServiceBase : IPresentationServiceBase
{
    private readonly INavigationFrameProvider _navigationFrameProvider;
    private readonly IServiceProvider _serviceProvider;
    private readonly IWindowService _windowService;

    public PresentationServiceBase(
        IServiceProvider serviceProvider, 
        INavigationFrameProvider navigationFrameProvider, 
        IWindowService windowService)
    {
        _serviceProvider = serviceProvider;
        _navigationFrameProvider = navigationFrameProvider;
        _windowService = windowService;
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

    bool IPresentationServiceBase.CanGoBack(string frameName) => _navigationFrameProvider.GetNavigationFrame(frameName).CanGoBack;

    public INavigationFrame GetNavigationFrame(string frameName = "") =>
        _navigationFrameProvider.GetNavigationFrame(frameName);

    public Task OpenWindow(Type viewModelType, OpenWindowOptions? options = null)
        => _windowService.OpenWindow(viewModelType, options ?? new OpenWindowOptions());
}