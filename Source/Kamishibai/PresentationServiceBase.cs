namespace Kamishibai;

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

    public Task<bool> NavigateAsync<TViewModel>(string frameName = "")
    {
        return _navigationFrameProvider
            .GetNavigationFrame(frameName)
            .NavigateAsync<TViewModel>(_serviceProvider);
    }

    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, string frameName = "") where TViewModel : notnull
    {
        return _navigationFrameProvider
            .GetNavigationFrame(frameName)
            .NavigateAsync(viewModel, _serviceProvider);
    }

    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, string frameName = "")
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

    public Task<IWindowHandle> OpenWindowAsync(Type viewModelType, object? owner = null, OpenWindowOptions? options = null)
        => _windowService.OpenWindowAsync(viewModelType, owner, options ?? new OpenWindowOptions());

    public Task<IWindowHandle> OpenWindowAsync<TViewModel>(object? owner = null, OpenWindowOptions? options = null)
        => _windowService.OpenWindowAsync<TViewModel>(owner, options ?? new OpenWindowOptions());

    public Task<IWindowHandle> OpenWindowAsync<TViewModel>(TViewModel viewModel, object? owner = null, OpenWindowOptions? options = null) where TViewModel : notnull
        => _windowService.OpenWindowAsync(viewModel, owner, options ?? new OpenWindowOptions());

    public Task<IWindowHandle> OpenWindowAsync<TViewModel>(Action<TViewModel> init, object? owner = null, OpenWindowOptions? options = null)
        => _windowService.OpenWindowAsync(init, owner, options ?? new OpenWindowOptions());

    public Task<bool> OpenDialogAsync(Type viewModelType, object? owner = null, OpenDialogOptions? options = null)
        => _windowService.OpenDialogAsync(viewModelType, owner, options ?? new OpenDialogOptions());

    public Task<bool> OpenDialogAsync<TViewModel>(object? owner = null, OpenDialogOptions? options = null)
        => _windowService.OpenDialogAsync<TViewModel>(owner, options ?? new OpenDialogOptions());

    public Task<bool> OpenDialogAsync<TViewModel>(TViewModel viewModel, object? owner = null, OpenDialogOptions? options = null) where TViewModel : notnull
        => _windowService.OpenDialogAsync(viewModel, owner, options ?? new OpenDialogOptions());

    public Task<bool> OpenDialogAsync<TViewModel>(Action<TViewModel> init, object? owner = null, OpenDialogOptions? options = null)
        => _windowService.OpenDialogAsync(init, owner, options ?? new OpenDialogOptions());

    public Task CloseWindowAsync(object? window = null) => _windowService.CloseWindowAsync(window);

    public Task CloseDialogAsync(bool dialogResult, object? window = null) => _windowService.CloseDialogAsync(dialogResult, window);

    public MessageBoxResult ShowMessage(string messageBoxText, string caption = "",
        MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None,
        MessageBoxResult defaultResult = MessageBoxResult.None, MessageBoxOptions options = MessageBoxOptions.None,
        object? owner = null)
        => _windowService.ShowMessage(messageBoxText, caption, button, icon, defaultResult, options, owner);

    public DialogResult OpenFile(OpenFileDialogContext context) => _windowService.OpenFile(context);
    public DialogResult SaveFile(SaveFileDialogContext context) => _windowService.SaveFile(context);
}