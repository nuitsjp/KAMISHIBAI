namespace Kamishibai;

public interface INavigatedAsyncAware
{
    Task OnNavigatedAsync(PostForwardEventArgs args);
}