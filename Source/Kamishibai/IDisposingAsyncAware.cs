namespace Kamishibai.Wpf;

public interface IDisposingAsyncAware
{
    Task<bool> OnDisposingAsync();
}