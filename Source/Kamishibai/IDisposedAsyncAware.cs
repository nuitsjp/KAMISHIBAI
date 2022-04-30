namespace Kamishibai;

public interface IDisposedAsyncAware
{
    Task OnDisposedAsync(PostBackwardEventArgs args);
}