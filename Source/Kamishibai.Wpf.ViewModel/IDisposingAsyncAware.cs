namespace Kamishibai.Wpf.ViewModel;

public interface IDisposingAsyncAware
{
    Task OnDisposingAsync();
}