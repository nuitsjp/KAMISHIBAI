namespace Kamishibai.Wpf;

public interface IWindowService
{
    Task OpenWindowAsync(Type viewModelType, OpenWindowOptions options);
    Task OpenWindowAsync<TViewModel>(TViewModel viewModel, OpenWindowOptions options) where TViewModel : notnull;
    Task OpenWindowAsync<TViewModel>(Action<TViewModel> init, OpenWindowOptions options);
    Task OpenDialogAsync(Type viewModelType, OpenWindowOptions options);
    Task OpenDialogAsync<TViewModel>(TViewModel viewModel, OpenWindowOptions options) where TViewModel : notnull;
    Task OpenDialogAsync<TViewModel>(Action<TViewModel> init, OpenWindowOptions options);
}