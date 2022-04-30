using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Kamishibai.Test.PresentationServiceTest;

public abstract class NotificationViewModel :
    IPausingAware,
    IPausingAsyncAware,
    INavigatingAware,
    INavigatingAsyncAware,
    INavigatedAware,
    INavigatedAsyncAware,
    IPausedAware,
    IPausedAsyncAware
{
    protected abstract void OnNotify(IForwardEventArgs args, [CallerMemberName] string memberName = "");

    protected Task OnNotifyAsync(IForwardEventArgs args, [CallerMemberName] string memberName = "")
    {
        OnNotify(args, memberName);
        return Task.CompletedTask;
    }

    public void OnPausing(PreForwardEventArgs args) => OnNotify(args);
    public Task OnPausingAsync(PreForwardEventArgs args) => OnNotifyAsync(args);
    public void OnNavigating(PreForwardEventArgs args) => OnNotify(args);
    public Task OnNavigatingAsync(PreForwardEventArgs args) => OnNotifyAsync(args);
    public void OnNavigated(PostForwardEventArgs args) => OnNotify(args);
    public Task OnNavigatedAsync(PostForwardEventArgs args) => OnNotifyAsync(args);
    public void OnPaused(PostForwardEventArgs args) => OnNotify(args);
    public Task OnPausedAsync(PostForwardEventArgs args) => OnNotifyAsync(args);
}
