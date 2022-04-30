namespace Kamishibai;

public interface IDisposedAware
{
    void OnDisposed(PostBackwardEventArgs args);
}