namespace Kamishibai;

public interface IDisposedAsyncAware
{
    Task OnDisposedAsync();
}