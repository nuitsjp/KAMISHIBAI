namespace Kamishibai.Wpf;

public interface IWindowService
{
    Task OpenWindowAsync(Type viewModelType, OpenWindowOptions options);
    Task OpenWindowAsync<TViewModel>(TViewModel viewModel, OpenWindowOptions options) where TViewModel : notnull;
    Task OpenDialogAsync(Type viewModelType, OpenWindowOptions options);
    Task OpenDialogAsync<TViewModel>(TViewModel viewModel, OpenWindowOptions options) where TViewModel : notnull;
}