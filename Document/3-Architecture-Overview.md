[[Home]](../README-ja.md) > Architecture Overview

# Architecture Overview  
Important elements of KAMISHIBAI are the following three.  

* [Navigator](#navigator)
* [NavigationRequest](#navigationrequest)  
* [NavigationBehavior（Parent class of PushAsync or PushModalAsync and others）](#navigationbehavior)  

![](3-Architecture-Overview/KAMISHIBAI.png)

# Navigator

Navigator is a wrapper class of Xamarin.Forms.INavigation instructing Page navigation.    
By wrapping INavigation, Navigator gives Page navigaiton the following functions:  

* State change event notification of Page with recursion to children and high consistency  
    * OnInitialize
    * OnLoaded
    * OnUnloaded
    * OnClosed
* Type safety Page navigation parameter  

## State change event notification of Page  
Navigator does event notification with recursion to children and highly consistent.    
There are four types of events. By each implementing interface Page and View Model notifications to receive.  

* [IPageInitializeAware](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/IPageInitializeAware.cs)  
* [IPageLoadedAware](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/IPageLoadedAware.cs)  
* [IPageUnloadedAware](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/IPageUnloadedAware.cs)  
* [IPageClosedAware](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/IPageClosedAware.cs)

Also receive Application Sleep and Resume events.    

* [IApplicationOnSleepAware](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/IApplicationOnSleepAware.cs)  
* [IApplicationOnResumeAware](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/IApplicationOnResumeAware.cs)  

### Recursive event notification to child page

In KAMISHIBAI, for example, if MasterDetailPage has Tabbedpage, it will notify Tabbedpage's tab. Notification to the end page.  
In fact, it has been transferred to the LifecycleNoticeService, and the event is recursively notified in LifecycleNotifier.  

* [LifecycleNoticeService](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/LifecycleNoticeService.cs)  
* [LifecycleNotifier](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/LifecycleNotifier.cs)  

### Consistent event Notifications  

In the Navigator, following a difficult case consistently, to notify the event.  

1. Back processing by NavigationPage, NavigationBar or physical back button  
2. TabbedPage tab switching and CarouselPage page switching  
3. When returning by physical back button at Modal navigation  

1 and 2 are resolved by injecting behavior from the outside into the destination page when Page navigation.  
Specifically, see the implementation of the following class:   

* [BehaviorInjector](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/BehaviorInjector.cs)
* [NavigationPageBehavior](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/NavigationPageBehavior.cs)
* [MultiPageBehavior&lt;TPage>](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/MultiPageBehavior.cs)  

Modal transition: physical back button see your following class:    

* [ApplicationService](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/ApplicationService.cs)  

# NavigationRequest

This is the INavigationRequest implementation class. From the View Model to View the requested Page navigation.  
There are two types of [INavigationRequest interfaces with no arguments](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/INavigationRequest.cs#L5) and an [INavigationRequest&;t;TPara> interface with arguments](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/INavigationRequest.cs#L12).  

KAMISHIBAI, not call direct Page navigation from the View Model layer. The View model layer only requires navigation. This is the most valuable point in design.  

![](3-Architecture-Overview/KAMISHIBAI.png)

All Page navigation is realized in View layer. Thanks to this, every page navigattion of Xamarin is possible.  
For high-level page navigation services, it is difficult to deal with unexpected navigations. This architecture solves that.  
And KAMISHIBAI is low-cost learning.

# NavigationBehavior  

Navigationbehavior binds Navigationrequest, receives a Page navigation request and calls navigator. NavigationBehavior is an abstract class. There the concrete classes such as PushAsync and PopAsync.  

Destination Page in XAML on the type parameter to specify the idea was most important. KAMISHIBAI itself wouldn't exist without the idea. 

Provides the Behavior that corresponds to all Page navigation methods now exist in INavigation. You can then inherit the Navigationbehavior and freely implement the new page navigation.  

* [InsertPageBefore](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/InsertPageBefore.cs)  
* [PopAsync](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/PopAsync.cs)  
* [PopModalAsync](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/PopModalAsync.cs)  
* [PopToRootAsync](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/PopToRootAsync.cs)  
* [PushAsync](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/PushAsync.cs)  
* [PushModalAsync](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/PushModalAsync.cs)  
* [RemovePage](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/RemovePage.cs)  
* [SetMainPage](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/SetMainPage.cs)  

Only SetMainPage does not exist in the INavigation method.  As a result, the call ApplicationService rather than Navigator at KAMISCIBAI.  
