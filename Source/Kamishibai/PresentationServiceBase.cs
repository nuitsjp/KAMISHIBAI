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

    public Task OpenWindowAsync(Type viewModelType, object? owner = null, OpenWindowOptions? options = null)
        => _windowService.OpenWindowAsync(viewModelType, owner, options ?? new OpenWindowOptions());

    public Task OpenWindowAsync<TViewModel>(TViewModel viewModel, object? owner = null, OpenWindowOptions? options = null) where TViewModel : notnull
        => _windowService.OpenWindowAsync(viewModel, owner, options ?? new OpenWindowOptions());

    public Task OpenWindowAsync<TViewModel>(Action<TViewModel> init, object? owner = null, OpenWindowOptions? options = null)
        => _windowService.OpenWindowAsync(init, owner, options ?? new OpenWindowOptions());

    public Task<bool> OpenDialogAsync(Type viewModelType, object? owner = null, OpenWindowOptions? options = null)
        => _windowService.OpenDialogAsync(viewModelType, owner, options ?? new OpenWindowOptions());

    public Task<bool> OpenDialogAsync<TViewModel>(TViewModel viewModel, object? owner = null, OpenWindowOptions? options = null) where TViewModel : notnull
        => _windowService.OpenDialogAsync(viewModel, owner, options ?? new OpenWindowOptions());

    public Task<bool> OpenDialogAsync<TViewModel>(Action<TViewModel> init, object? owner = null, OpenWindowOptions? options = null)
        => _windowService.OpenDialogAsync(init, owner, options ?? new OpenWindowOptions());

    public Task CloseWindowAsync(object? window = null) => _windowService.CloseWindowAsync(window);

    public Task CloseDialogAsync(bool dialogResult, object? window = null) => _windowService.CloseDialogAsync(dialogResult, window);
}