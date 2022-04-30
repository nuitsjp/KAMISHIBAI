namespace Kamishibai;

public interface IResumingAware
{
    void OnResuming(PreBackwardEventArgs args);
}