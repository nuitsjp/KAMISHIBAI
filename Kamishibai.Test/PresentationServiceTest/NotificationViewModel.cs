using System;
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
    IPausedAsyncAware,
    IDisposingAware,
    IDisposingAsyncAware,
    IResumingAware,
    IResumingAsyncAware,
    IResumedAware,
    IResumedAsyncAware,
    IDisposable,
    IDisposedAware,
    IDisposedAsyncAware
{
    protected abstract void OnNotify(IForwardEventArgs args, [CallerMemberName] string memberName = "");

    protected Task OnNotifyAsync(IForwardEventArgs args, [CallerMemberName] string memberName = "")
    {
        OnNotify(args, memberName);
        return Task.CompletedTask;
    }

    protected abstract void OnNotify(IBackwardEventArgs args, [CallerMemberName] string memberName = "");

    protected Task OnNotifyAsync(IBackwardEventArgs args, [CallerMemberName] string memberName = "")
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
    public void OnDisposing(PreBackwardEventArgs args) => OnNotify(args);
    public Task OnDisposingAsync(PreBackwardEventArgs args) => OnNotifyAsync(args);
    public void OnResuming(PreBackwardEventArgs args) => OnNotify(args);
    public Task OnResumingAsync(PreBackwardEventArgs args) => OnNotifyAsync(args);
    public void OnResumed(PostBackwardEventArgs args) => OnNotify(args);
    public Task OnResumedAsync(PostBackwardEventArgs args) => OnNotifyAsync(args);
    public void Dispose() => OnNotify(new PostBackwardEventArgs(null, null!, null));
    public void OnDisposed(PostBackwardEventArgs args) => OnNotify(args);
    public Task OnDisposedAsync(PostBackwardEventArgs args) => OnNotifyAsync(args);
}
