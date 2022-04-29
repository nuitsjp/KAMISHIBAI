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
    protected abstract void OnNotify([CallerMemberName] string memberName = "");

    private async Task OnNotifyAsync([CallerMemberName] string memberName = "")
    {
        OnNotify(memberName);
        await Task.CompletedTask;
    }

    public bool OnPausing()
    {
        OnNotify();
        return true;
    }

    public async Task<bool> OnPausingAsync()
    {
        await OnNotifyAsync();
        return true;
    }

    public void OnNavigating() => OnNotify();

    public Task OnNavigatingAsync() => OnNotifyAsync();

    public void OnNavigated() => OnNotify();

    public Task OnNavigatedAsync() => OnNotifyAsync();

    public void OnPaused() => OnNotify();

    public Task OnPausedAsync() => OnNotifyAsync();
}
