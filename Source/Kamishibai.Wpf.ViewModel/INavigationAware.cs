namespace Kamishibai.Wpf.ViewModel;

public interface INavigationAware
{
    Task OnEntryAsync();
    Task OnExitAsync();
}