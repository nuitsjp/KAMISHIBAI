namespace Kamishibai;

public interface INavigatingAware
{
    void OnNavigating(PreForwardEventArgs args);
}