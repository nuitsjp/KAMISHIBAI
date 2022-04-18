namespace Kamishibai;

public interface IPausedAsyncAware
{
    Task OnPausedAsync();
}