namespace Kamishibai;

public interface IPausingAware
{
    void OnPausing(PreForwardEventArgs args);
}