namespace Kamishibai.Wpf.ViewModel;

public interface IDisposedAsyncAware
{
    Task OnDisposedAsync();
}