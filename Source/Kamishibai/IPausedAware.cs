namespace Kamishibai;

public interface IPausedAware
{
    void OnPaused(PostForwardEventArgs args);
}