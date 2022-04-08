namespace Kamishibai.Wpf.ViewModel;

public interface IPausingAsyncAware
{
    Task<bool> OnPausingAsync();
}