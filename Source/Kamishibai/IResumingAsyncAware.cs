namespace Kamishibai;

public interface IResumingAsyncAware
{
    Task OnResumingAsync(PreBackwardEventArgs args);
}