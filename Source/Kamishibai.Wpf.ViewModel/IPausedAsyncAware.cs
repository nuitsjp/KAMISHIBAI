namespace Kamishibai.Wpf.ViewModel;

public interface IPausedAsyncAware
{
    Task OnPausedAsync();
}