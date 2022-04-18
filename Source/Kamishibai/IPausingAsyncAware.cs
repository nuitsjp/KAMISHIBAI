namespace Kamishibai;

public interface IPausingAsyncAware
{
    Task<bool> OnPausingAsync();
}