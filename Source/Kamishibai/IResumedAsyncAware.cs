namespace Kamishibai;

public interface IResumedAsyncAware
{
    Task OnResumedAsync(PostBackwardEventArgs args);
}