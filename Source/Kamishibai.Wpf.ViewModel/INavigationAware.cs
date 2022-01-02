namespace Kamishibai.Wpf.ViewModel;

public interface INavigationAware
{
    Task OnEntryAsync();
    Task OnExitAsync();
}

public interface INavigationAware<in T>
{
    Task OnEntryAsync(T obj);
    Task OnExitAsync();
}