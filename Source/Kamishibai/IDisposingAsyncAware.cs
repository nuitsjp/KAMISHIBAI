namespace Kamishibai;

public interface IDisposingAsyncAware
{
    Task<bool> OnDisposingAsync();
}