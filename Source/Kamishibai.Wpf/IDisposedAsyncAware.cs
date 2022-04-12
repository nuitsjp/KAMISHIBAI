namespace Kamishibai.Wpf;

public interface IDisposedAsyncAware
{
    Task OnDisposedAsync();
}