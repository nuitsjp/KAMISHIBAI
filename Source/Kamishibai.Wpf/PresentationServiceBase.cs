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

    public Task OpenWindowAsync(Type viewModelType, OpenWindowOptions? options = null)
        => _windowService.OpenWindowAsync(viewModelType, options ?? new OpenWindowOptions());

    public Task OpenWindowAsync<TViewModel>(TViewModel viewModel, OpenWindowOptions? options = null) where TViewModel : notnull
        => _windowService.OpenWindowAsync(viewModel, options ?? new OpenWindowOptions());

    public Task OpenWindowAsync<TViewModel>(Action<TViewModel> init, OpenWindowOptions? options = null)
        => _windowService.OpenWindowAsync(init, options ?? new OpenWindowOptions());

    public Task OpenDialogAsync(Type viewModelType, OpenWindowOptions? options = null)
        => _windowService.OpenDialogAsync(viewModelType, options ?? new OpenWindowOptions());

    public Task OpenDialogAsync<TViewModel>(TViewModel viewModel, OpenWindowOptions? options = null) where TViewModel : notnull
        => _windowService.OpenDialogAsync(viewModel, options ?? new OpenWindowOptions());

    public Task OpenDialogAsync<TViewModel>(Action<TViewModel> init, OpenWindowOptions? options = null)
        => _windowService.OpenDialogAsync(init, options ?? new OpenWindowOptions());
}