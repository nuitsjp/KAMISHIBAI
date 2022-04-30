namespace Kamishibai;

public interface INavigatingAsyncAware
{
    Task OnNavigatingAsync(PreForwardEventArgs args);
}