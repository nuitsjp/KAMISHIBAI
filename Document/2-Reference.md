[[Home]](../README-ja.md) > Reference

# Reference

1. [Navigation event types](#navigation-event-types)  
    1. [OnInitialize](#oninitialize)  
    2. [OnLoaded](#onloaded)  
    3. [OnUnloaded](#onunloaded)  
    4. [OnClosed](#onclosed)  
2. [Application life cycle event](#application-life-cycle-event)  
    1. [OnSleep](#onsleep)  
    2. [OnResume](#onresume)
3. [Page navigation parameter](#page-navigation-parameter)  

# [Navigation event types](#Reference)  

KAMISHIBAI supports event notification when Page display status is changed.  
Not page navigation event, the state of the pages is changed.  
Even in the case of switching between Tab and CarouselPage, KAMISHIBAI will notifies consistent events.

Event notifications are made for the Page or View Model that implements the corresponding interface (such as IPageInitializeAware).

Event notifications are executed recursively from the parent Page to the child Page.  
The actual notification target and the order of notification depend on the type of event and the Page type.  

Notify event type is as follows:  

* [OnInitialize](#oninitialize)  
* [OnLoaded](#onloaded)  
* [OnUnloaded](#onunloaded)  
* [OnClosed](#onclosed)

There is also a next interface to receive all events.  

* IPageLifecycleAware
* IPageLifecycleAware&lt;in TParam>

## [OnInitialize](#Reference)  

||Explanation|
|:--|:--|
|Overview|It is notified only once to the newly created Page and ViewModel at the Page navigation.<br>It is assumed to be used for the implementation of the initialization process.<br>If a parameter is specified, it can be received. Only OnInitialize events can receive parameters.<br>Before the navigation process is executed (for example, before calling the INavigation#Pusyasync() method).<br>Not be notified, if you came back in return.|
|Interface|IPageInitializeAware、IPageInitializeAware&lt;in TParam>|
|Basic notification order|Be notified before the screen transitions.<br>Notify parent to child with top down. <br>Notify Page ahead of ViewModel.|
|MasterDetailPage  notification order|1. MasterDetailPage<br>2. MasterPage<br>3. DetailPage.|
|NavigationPage  notification order|1. Navigationpage<br>2. NavigationStack ascending|
|TabbedPage  notification order|1. TabbedPage<br>2. Tabs Ascending|
|CarouselPage  notification order|1. CarouselPage<br>2. Child Page Ascending|


## [OnLoaded](#Reference)  

||Explanation|
|:--|:--|
|Overview|It is notified after displaying the Page.<br>(PushAsync, etc.) not only when forward, but also when returned in some way back processing is notified.<br>OnUnload, designed to be Page deactivation, OnLoaded assumes activation process in reverse.<br>Supposed to come back in a physical back button and swipe. Therefore, it does not correspond to parameters.|
|Interface|IPageLoadedAware|
|Basic notification order|After displaying a page, ago the original Page's OnUnload notified.<br>From parent to child to notify top-down.<br>Notify ViewModel after notifying page.|
|MasterDetailPage  notification order|1. MasterDetailPage<br>2. MasterPage<br>3. DetailPage.|
|NavigationPage  notification order|1. NavigationPage<br>2. Current Page|
|TabbedPage  notification order|1. TabbedPage<br>2. Current Tab Page|
|CarouselPage  notification order|1. CarouselPage<br>2. Current Page|

## [OnUnloaded](#Reference)  

||Explanation|
|:--|:--|
|Overview|It is notified after Page is not displayed.<br>Expects the implementation of Page deactivation process.  <br>It is notified to Page that may be redisplayed.<br>Page that pops from the stack will be notified of onclosed.|
|Interface|IPageUnloadedAware|
|Basic notification order|It is notified after OnLoaded is notified to the displayed Page.<br>From child to parent to notify buttom-up.<br>Notify Page after notifying View Model.|
|MasterDetailPage  notification order|1. DetailPage<br>2. MasterPage<br>3. MasterDetailPage|
|NavigationPage  notification order|1. Current Page<br>2. NavigationPage|
|TabbedPage  notification order|1. Current Tab Page<br>2. TabbedPage<br>When there are multiple tabs, the hidden tab is not notified.<br>Also notified when the tab is hidden from display.|
|CarouselPage  notification order|1. Current Page<br>2. CarouselPage<br>When there are multiple Pages, the hidden page is not notified.<br>Also notified when the Page is hidden from display.|

## [OnClosed](#Reference)  

||説明|
|:--|:--|
|Overview|Notified when the Page is destroyed.<br>If the page is pop from the stack, etc.<br>It is intended to release resources and unsubscribe from events.|
|Interface|IPageClosedAware|
|Basic notification order|It is notified after OnLoaded is notified to the displayed Page.<br>From child to parent to notify buttom-up.<br>Notify Page after notifying View Model.|
|MasterDetailPage  notification order|1. DetailPage<br>2. MasterPage<br>3. MasterDetailPage|
|NavigationPage  notification order|1. NavigationPage<br>2. NavigationStack Descending|
|TabbedPage  notification order|1. TabbedPage<br>2. All Tabs Descending|
|CarouselPage  notification order|1. CarouselPage<br>2. All Child Pages Descending|

# [Application life cycle event](#Reference)  

KAMISHIBAI is the Page Navigation library. Originally dealing with application life-cycle events is not might be appropriate.    
However, the mechanism for notifying Application OnSleep and OnResume will be similar to the KAMISHIBAI notification.
For this reason, KAMISHIBAI provides a mechanism for notifying application lifecycle events.  

For notifications, implement the following in the Onsleep and Onresume of the App.cs class:

```cs
protected override void OnSleep()
{
    LifecycleNoticeService.OnSleep(this);
}

protected override void OnResume()
{
    LifecycleNoticeService.OnResume(this);
}
```

## [OnSleep](#Reference)  

||説明|
|:--|:--|
|Overview|To notify the OnSleep of the Xamarin.Forms.Application|
|Interface|IApplicationLifecycleAware|
|Basic notification order|Notify all Pages and View Models.<br>Notify all pages on ModalStack in descending order, then notify MainPage.<br>If the page on the modalstack has a nested structure, from child to parent to notify buttom-up.<br>Notify Page after notifying View Model.|
|MasterDetailPage  notification order|1. DetailPage<br>2. MasterPage<br>3. MasterDetailPage|
|NavigationPage  notification order|1. NavigationPage<br>2. NavigationStack Descending|
|TabbedPage  notification order|1. TabbedPage<br>2. All Tabs Descending|
|CarouselPage  notification order|1. CarouselPage<br>2. All Child Pages Descending|


## [OnResume](#Reference)  

||説明|
|:--|:--|
|Overview|To notify the OnResume of the Xamarin.Forms.Application|
|Interface|IApplicationLifecycleAware|
|Basic notification order|Notify all Pages and View Models.<br>Notify MainPage, then notify all pages on ModalStack in ascending order.<br>If the page on the modalstack has a nested structure, from parent to child to notify top-down.<br>Notify Page after notifying View Model.|
|MasterDetailPage  notification order|1. MasterDetailPage<br>2. MasterPage<br>3. DetailPage.|
|NavigationPage  notification order|1. NavigationPage<br>2. NavigationStack Ascending|
|TabbedPage  notification order|1. TabbedPage<br>2. Tabs Ascending|
|CarouselPage  notification order|1. CarouselPage<br>2. Child Page Ascending|

# [Page Navigation Parameter](#Reference)  

Kamishibai can pass a type-safe parameter.  
To pass parameters, implement the following:  

* Navigation request by using INavigationRequest&lt;in TParam>  
* Page or View Model to implement IPageInitializeAware&lt;in TParam> or IPageLifecycleAware&lt;in TParam>  

If you pass a parameter of type string, you request navigation as follows:  

```cs
public INavigationRequest<string> RequestNavigation { get; } = new NavigationRequest<string>();

public Task Navigation()
{
    return RequestNavigation.RaiseAsync("param");
}
```

And, it receives it as follows:  

```cs
public FooPageViewModel : IPageInitializeAware<string>
{
    public void OnInitialize(string parameter)
    {
        ...
    }
}
```

There is no problem even if the type parameter of the interface and the parameter passed are different. But it must be assignable to the receiving side.    
Also, if you pass a parameter on a navigation request, you will not receive a notification in IPageInitializeAware without a type parameter.   

