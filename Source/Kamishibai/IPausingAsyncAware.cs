namespace Kamishibai.Wpf;

public interface IPausingAsyncAware
{
    Task<bool> OnPausingAsync();
}