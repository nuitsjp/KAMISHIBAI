namespace Kamishibai;

public interface IPresentationServiceBase
{
    Task<bool> NavigateAsync(Type viewModelType, string frameName = "");
    Task<bool> NavigateAsync<TViewModel>(string frameName = "");
    Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, string frameName = "") where TViewModel : notnull;
    Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, string frameName = "");
    Task<bool> GoBackAsync(string frameName = "");
    bool CanGoBack(string frameName = "");
    INavigationFrame GetNavigationFrame(string frameName = "");

    Task<IWindow> OpenWindowAsync(Type viewModelType, object? owner = null, OpenWindowOptions? options = null);
    Task<IWindow> OpenWindowAsync<TViewModel>(object? owner = null, OpenWindowOptions? options = null);
    Task<IWindow> OpenWindowAsync<TViewModel>(TViewModel viewModel, object? owner = null, OpenWindowOptions? options = null) where TViewModel : notnull;
    Task<IWindow> OpenWindowAsync<TViewModel>(Action<TViewModel> init, object? owner = null, OpenWindowOptions? options = null);
    Task CloseWindowAsync(object? window = null);

    Task<bool> OpenDialogAsync(Type viewModelType, object? owner = null, OpenDialogOptions? options = null);
    Task<bool> OpenDialogAsync<TViewModel>(object? owner = null, OpenDialogOptions? options = null);
    Task<bool> OpenDialogAsync<TViewModel>(TViewModel viewModel, object? owner = null, OpenDialogOptions? options = null) where TViewModel : notnull;
    Task<bool> OpenDialogAsync<TViewModel>(Action<TViewModel> init, object? owner = null, OpenDialogOptions? options = null);
    Task CloseDialogAsync(bool dialogResult, object? window = null);

    MessageBoxResult ShowMessage(
        string messageBoxText,
        string caption = "",
        MessageBoxButton button = MessageBoxButton.OK,
        MessageBoxImage icon = MessageBoxImage.None, 
        MessageBoxResult defaultResult = MessageBoxResult.None,
        MessageBoxOptions options = MessageBoxOptions.None,
        object? owner = null);

    DialogResult OpenFile(OpenFileDialogContext context);
    DialogResult SaveFile(SaveFileDialogContext context);
}