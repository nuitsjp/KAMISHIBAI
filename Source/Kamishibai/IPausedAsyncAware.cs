namespace Kamishibai;

public interface IPausedAsyncAware
{
    Task OnPausedAsync(PostForwardEventArgs args);
}