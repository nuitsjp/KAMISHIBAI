namespace Kamishibai;

public interface IDisposingAsyncAware
{
    Task OnDisposingAsync(PreBackwardEventArgs args);
}