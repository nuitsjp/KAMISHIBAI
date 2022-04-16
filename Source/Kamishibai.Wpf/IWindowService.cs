namespace Kamishibai.Wpf;

public interface IWindowService
{
    Task OpenWindowAsync(Type viewModelType, OpenWindowOptions options);
    Task OpenDialogAsync(Type viewModelType, OpenWindowOptions options);
}