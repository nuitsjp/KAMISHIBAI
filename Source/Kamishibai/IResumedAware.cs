namespace Kamishibai;

public interface IResumedAware
{
    void OnResumed(PostBackwardEventArgs args);
}