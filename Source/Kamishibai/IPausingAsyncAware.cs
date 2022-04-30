namespace Kamishibai;

public interface IPausingAsyncAware
{
    Task OnPausingAsync(PreForwardEventArgs args);
}