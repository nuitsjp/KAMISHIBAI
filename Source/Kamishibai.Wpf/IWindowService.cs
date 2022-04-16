namespace Kamishibai.Wpf;

public interface IWindowService
{
    Task OpenWindow(Type viewModelType);
}