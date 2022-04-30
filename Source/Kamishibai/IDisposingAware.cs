namespace Kamishibai;

public interface IDisposingAware
{
    void OnDisposing(PreBackwardEventArgs args);
}