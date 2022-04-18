namespace Kamishibai;

public interface IPresentationServiceBase
{
    public Task<bool> NavigateAsync(Type viewModelType, string frameName = "");
    public Task<bool> NavigateAsync<TViewModel>(string frameName = "") where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, string frameName = "") where TViewModel : class;
    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, string frameName = "") where TViewModel : class;
    Task<bool> GoBackAsync(string frameName = "");
    public bool CanGoBack(string frameName = "");
    INavigationFrame GetNavigationFrame(string frameName = "");

    public Task OpenWindowAsync(Type viewModelType, object? owner = null, OpenWindowOptions? options = null);
    public Task OpenWindowAsync<TViewModel>(object? owner = null, OpenWindowOptions? options = null);
    public Task OpenWindowAsync<TViewModel>(TViewModel viewModel, object? owner = null, OpenWindowOptions? options = null) where TViewModel : notnull;
    public Task OpenWindowAsync<TViewModel>(Action<TViewModel> init, object? owner = null, OpenWindowOptions? options = null);
    public Task CloseWindowAsync(object? window = null);

    public Task<bool> OpenDialogAsync(Type viewModelType, object? owner = null, OpenWindowOptions? options = null);
    public Task<bool> OpenDialogAsync<TViewModel>(TViewModel viewModel, object? owner = null, OpenWindowOptions? options = null) where TViewModel : notnull;
    public Task<bool> OpenDialogAsync<TViewModel>(Action<TViewModel> init, object? owner = null, OpenWindowOptions? options = null);
    public Task CloseDialogAsync(bool dialogResult, object? window = null);

    public MessageBoxResult ShowMessage(
        string messageBoxText,
        string? caption = null,
        MessageBoxButton button = MessageBoxButton.OK,
        MessageBoxImage icon = MessageBoxImage.None, 
        MessageBoxResult defaultResult = MessageBoxResult.None,
        MessageBoxOptions options = MessageBoxOptions.None,
        object? owner = null);
}