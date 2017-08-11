[[Home]](../README-ja.md) > Reference

# Reference

1. [Navigation event types](#navigation-event-types)  
    1. [OnInitialize](#oninitialize)  
    2. [OnLoaded](#onloaded)  
    3. [OnUnloaded](#onunloaded)  
    4. [OnClosed](#onclosed)  
2. [画面遷移時パラメーター仕様](#画面遷移時パラメーター仕様)  
3. [Applicationライフサイクルイベント仕様](#applicationライフサイクルイベント仕様)  
    1. [OnSleep](#onsleep)  
    2. [OnResume](#onresume)

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

|項目|説明|
|:--|:--|
|概要|画面遷移後の画面の表示後に通知される。<br>（PushAsyncなどで）前方へ画面遷移した場合だけでなく、何らかの戻る処理で戻ってきたタイミングでも通知される。<br>OnUnloadで、画面の非活性化処理を想定しており、OnLoadedでは逆に活性化処理を想定している。<br>物理バックボタンやスワイプで戻ってくることも想定しているため、画面遷移パラメーターには対応しない。|
|対応インターフェース|IPageLoadedAware|
|基本通知順序|画面遷移処理の完了後、OnUnloadの前に通知される。<br>親から子へトップダウンで通知する。<br>またPageへ先に通知した後に、ViewModelへ通知する。|
|MasterDetailPage|MasterDetailPage、MasterPage、DetailPageの順で通知する。|
|NavigationPage|NavigationPage、Currentの順で通知される。<br>NavigationStackに多数のページがあった場合でも、Currentにのみ通知する。|
|TabbedPage|TabbedPage、Currentタブの順で通知される。<br>タブが複数あった場合、非表示のタブには通知されない。<br>タブが切り替えられた場合、切り替え後のタブにたいしても通知される。|
|CarouselPage|CarouselPage、Currentページの順で通知される。<br>ページが複数あった場合、非表示のページには通知されない。<br>ページが切り替えられた場合、切り替え後のページにも通知される。|

## [OnUnloaded](#Reference)  

|項目|説明|
|:--|:--|
|概要|（PushAsyncなどで）前方へ画面遷移した際に、遷移元の画面に通知される。<br>画面の非活性化処理の実装を想定している。  <br>戻ってくる可能性のあるPageにたいして通知される。|
|対応インターフェース|IPageUnloadedAware|
|基本通知順序|画面遷移処理の完了後、OnLoadedの後に通知される。<br>子から親へボトムアップで通知する。<br>またViewModelへ通知した後、Pageへ先に通知される。|
|MasterDetailPage|DetailPage、MasterPage、MasterDetailPageの順で通知する。|
|NavigationPage|Currentページ、NavigationPageの順で通知される。<br>NavigationStackに多数のページがあった場合でも、Currentにのみ通知する。|
|TabbedPage|Currentタブ、TabbedPageの順で通知される。<br>タブが複数あった場合、非表示のタブには通知されない。<br>タブが切り替えられた場合、切り替え前に表示していたタブのページにたいしても通知される。|
|CarouselPage|Currentページ、CarouselPageの順で通知される。<br>ページが複数あった場合、非表示のページには通知されない。<br>ページが切り替えられた場合、切り替え前に表示していたページにも通知される。|

## [OnClosed](#Reference)  

|項目|説明|
|:--|:--|
|概要|Pageが破棄されるタイミングで通知される。<br>リソースの解放や、イベントの購読解除などを想定している。<br>戻る処理が発生した場合の元画面にたいして通知される。|
|対応インターフェース|IPageClosedAware|
|基本通知順序|戻る処理の完了後、OnLoadedの後に通知される。<br>子から親へボトムアップで通知する。<br>またViewModelへ通知した後、Pageへ先に通知される。|
|MasterDetailPage|DetailPage、MasterPage、MasterDetailPageの順で通知する。|
|NavigationPage|NavigationStackの全ページの降順、NavigationPageの順で通知される。<br>NavigationPageそのものがPopされたり、MainPageが変更された場合が該当する。|
|TabbedPage|全タブの降順、TabbedPageの順で通知される。|
|CarouselPage|全子ページの降順、CarouselPageの順で通知される。|

# [画面遷移時パラメーター仕様](#Reference)  

つぎの条件を満たした場合、型安全性の保障されたパラメーターを遷移時に渡すことが可能です。  

* INavigationRequest&lt;in TParam>を利用した遷移通知  
* PageかViewModelが、IPageInitializeAware&lt;in TParam>もしくはIPageLifecycleAware&lt;in TParam>を実装している  

例えばstring型のパラメーターを渡す場合、ViewModelではつぎのように実装します。  

```cs
public INavigationRequest<string> RequestNavigation { get; } = new NavigationRequest<string>();

public Task Navigation()
{
    return RequestNavigation.RaiseAsync("param");
}
```

そしてViewModel側ではつぎのように受け取ります。  

```cs
public FooPageViewModel : IPageInitializeAware<string>
{
    public void OnInitialize(string parameter)
    {
        ...
    }
}
```

型パラメーターと実際に受け渡すパラメーターは、完全に一致する必要はありませんが、受け渡すパラメーターが受け取る側の型に代入可能である必要があります。  
また、遷移要求時にパラメーターを指定した場合、型パラメーターで修飾されていないIPageInitializeAwareを実装しても、通知を受け取ることができませんので注意が必要です。  

# [Applicationライフサイクルイベント仕様](#Reference)  

KAMISHIBAIは画面遷移フレームワークであり、本来はアプリケーションのライフサイクルイベントを扱うのは適当ではないかもしれません。  
しかし、ApplicationのOnSleepやOnResumeを通知する仕組みは、KAMISHIBAIの画面遷移イベントの通知と非常に似通ったものになるでしょう。  
その為、KAMISHIBAIではApplicationのライフサイクルイベントに関しても、通知できる仕組みを用意しています。  

通知を行う場合、App.csクラスのOnSleepとOnResumeで次のように実装してください。

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

|項目|説明|
|:--|:--|
|概要|アプリケーションが背面に移動した場合などに通知される。<br>通知タイミングはXamarin.Forms.ApplicationのOnSleepの仕様に準ずる|
|対応インターフェース|IApplicationLifecycleAware|
|基本通知順序|全ての画面へ通知する。<br>ModalStack上のページすべてに降順で通知した後、MainPageへ通知する。<br>ModalStack上のページがネストした構造を持っていた場合、子から親へボトムアップで通知する。<br>またViewModelへ通知した後、Pageへ先に通知される。|
|MasterDetailPage|DetailPage、MasterPage、MasterDetailPageの順で通知する。|
|NavigationPage|NavigationStackの全ページの降順、NavigationPageの順で通知される。<br>NavigationPageそのものがPopされたり、MainPageが変更された場合が該当する。|
|TabbedPage|全タブの降順、TabbedPageの順で通知される。|
|CarouselPage|全子ページの降順、CarouselPageの順で通知される。|


## [OnResume](#Reference)  

|項目|説明|
|:--|:--|
|概要|アプリケーションが背面に移動した場合などに通知される。<br>通知タイミングはXamarin.Forms.ApplicationのOnResumeの仕様に準ずる|
|対応インターフェース|IApplicationLifecycleAware|
|基本通知順序|全ての画面へ通知する。<br>MainPageへ通知した後、ModalStack上のすべてのページへ昇順で通知する。<br>ModalStack上のページがネストした構造を持っていた場合、親から子へトップダウンで通知する。<br>またPageへ先に通知した後に、ViewModelへ通知する。|
|MasterDetailPage|MasterDetailPage、MasterPage、DetailPageの順で通知する。|
|NavigationPage|NavigationPage、NavigationStackの全ページの昇順で通知する。<br>DeepLinkを行っていない場合は、実質子ページは一つであるため、NavigationPageへの通知の後に、Currentへ通知されることになる。|
|TabbedPage|TabbedPage、全タブの昇順で通知される。|
|CarouselPage|CarouselPage、全子Pageの昇順で通知される。|
