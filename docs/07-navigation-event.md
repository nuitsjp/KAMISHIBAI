# Navigation Event Details

Consistent navigation events are one of KAMISHIBAI's strengths.

A navigation event consists of two elements

1. the pattern of event notification
2. timing of event notification

# Event notification patterns

KAMISHIBAI offers the following two notification patterns

1. notification to the ViewModel interface
2. event handler of NavigationFrame

## Notification to ViewModel interface

You can receive event notifications in the interface corresponding to the event in the ViewModel.

For example, if you want to receive notification of the completion of navigation in the ViewModel after navigation, you can implement the following.

```cs
public class MainViewModel : INavigatedAsyncAware
{
    public Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        ...
    }
```

The various interfaces are described below.

## Event handler for NavigationFrame

If the event notification interface is used, the event must be received by the ViewModel involved in navigation.

If you wish to receive events in a location not involved in navigation, use the INavigationFrame event handler.

```cs
INavigationFrame frame = _presentationService.GetNavigationFrame();
frame.Navigated += FrameOnNavigated;
```

Get the target NavigationFrame from IPresentationService and receive events.

# Event Notification Timing

KAMISHIBAI can receive notifications at any time before or after navigation.

![](/Images/navigation-event.png)

大まかに上図のタイミングでイベントを受信できます。

Events can be received at the timing shown in the figure.

When navigating from Page1 to Page2, events are notified in the following order.


1. "OnPausing" notification to source of navigation
2. "OnNavigating" notification to source of destination
3. navigation
4. "OnNavigated" notification to source of destination
5. "OnPaused" notification to source of navigation

For each of these, there are three types of notifications: synchronous and asynchronous notifications to the ViewModel and INavigationFrame event handlers.
Only OnDisposed has some exceptions. This will be explained later.

The same is true in the case of return.


# Event Details: FORWARD

|Order|Notification|Interface|Member|Cancel|
|--:|:--|--|--|:-:|
|1|Source ViewModel|IPausingAsyncAware|OnPausingAsync|✅|
|2|Source ViewModel|IPausingAware|OnPausing|✅|
|3|any|INavigationFrame|Pausing|✅|
|4|Destination ViewModel|INavigatingAsyncAware|OnNavigatingAsync|✅|
|5|Destination ViewModel|INavigatingAware|OnNavigating|✅|
|6|any|INavigationFrame|Navigating|✅|
|7|-|Navigation|-|-|
|8|Destination ViewModel|INavigatedAsyncAware|OnNavigatedAsync|-|
|9|Destination ViewModel|INavigatedAware|OnNavigated|-|
|10|any|INavigationFrame|Navigated|-|
|11|Source ViewModel|IPausedAsyncAware|OnPausedAsync|-|
|12|Source ViewModel|IPausedAware|OnPaused|-|
|13|any|INavigationFrame|Paused|-|

No. 1 to 6 can cancel navigation.

```cs
public class MainViewModel : INavigatedAsyncAware
{
    public Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        args.Cancel = true
        ...
    }
```

Subsequent events that are cancelled will not be notified.

# Event Details: GO BACK

|Order|Notification|Interface|Member|Cancel|
|--:|:--|--|--|:-:|
|1|Source ViewModel|IDisposingAsyncAware|OnDisposingAsync|✅|
|2|Source ViewModel|IDisposingAware|OnDisposing|✅|
|3|any|INavigationFrame|Disposing|✅|
|4|Destination ViewModel|IResumingAsyncAware|OnResumingAsync|✅|
|5|Destination ViewModel|IResumingAware|OnResuming|✅|
|6|any|INavigationFrame|INavigationFrame|✅|
|7|-|Go Back|-|-|
|8|Destination ViewModel|IResumedAsyncAware|OnResumedAsync|-|
|9|Destination ViewModel|IResumedAware|OnResumed|-|
|10|any|INavigationFrame|Resumed|-|
|11|Source ViewModel|IDisposedAsyncAware|OnDisposedAsync|-|
|12|Source ViewModel|IDisposedAware|OnDisposed|-|
|13|Source ViewModel|IDisposable|Dispose|-|
|14|any|INavigationFrame|Disposed|-|

No. 1 to 6 can cancel navigation.

For interoperability with common .NET implementations, notifications can also be received via the IDisposable interface.

[<< OpenWindow and OpenDialog](06-open-window-and-dialog.md) | [Menu](01-table-of-contents.md) | [Message Dialog >>](08-message-dialog.md)