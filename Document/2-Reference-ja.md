[[Home]](../README-ja.md) > 詳細仕様

# 詳細仕様

1. [遷移イベント種別](#遷移イベント種別)  
    1. [OnInitialize](#oninitialize)  
    2. [OnLoaded](#onloaded)  
    3. [OnUnloaded](#onunloaded)  
    4. [OnClosed](#onclosed)  
2. [画面遷移時パラメーター仕様](#画面遷移時パラメーター仕様)  
3. [Applicationライフサイクルイベント仕様](#applicationライフサイクルイベント仕様)  
    1. [OnSleep](#onsleep)  
    2. [OnResume](#onresume)

# [遷移イベント種別](#詳細仕様)  

KAMISHIBAIでは、画面遷移時に複数のイベント通知に対応しています。  
イベントの通知は対応するインターフェース（IPageInitializeAwareなど）を実装したPageまたはViewModelに対して行います。  
ViewModelだけではなく、Pageに対しても通知可能です。  

イベントの通知は、親Pageから子Pageへ再帰的に実行されます。  
ただし、具体的な通知対象や通知順はイベント種別や、Pageの種別（NavigationPageかTabbedPageか...）によって異なります。  

対応するイベント種別は、つぎの通りです。  

* [OnInitialize](#oninitialize)  
* [OnLoaded](#onloaded)  
* [OnUnloaded](#onunloaded)  
* [OnClosed](#onclosed)

なおイベントの通知を受け取るためには、個別の対応インターフェースのほかに、全てのイベントを受け取るつぎのインターフェースも存在します。  
* IPageLifecycleAware
* IPageLifecycleAware&lt;in TParam>

## [OnInitialize](#詳細仕様)  

|項目|説明|
|:--|:--|
|概要|画面遷移時に新しく作成されたPageおよびViewModelにたいして一度だけ通知される。<br>初期化処理の実装に用いることを想定している。<br>遷移要求時に画面遷移パラメーターが指定されていた場合、OnInitializeイベントにてパラメーターを受け取ることが可能。パラメーターを受け取ることが可能なのはOnInitializeイベントのみ。<br>画面遷移時において、遷移処理の実行前（例えばINavigation#PusyAsync()メソッドの呼び出し前）に通知される。<br>戻る処理で戻ってきた場合などは通知されない。|
|対応インターフェース|IPageInitializeAware、IPageInitializeAware&lt;in TParam>|
|基本通知順序|画面遷移処理の実施前（PushAsyncなどの呼び出し前）に通知される。<br>親から子へトップダウンで通知する。<br>またPageへ先に通知した後に、ViewModelへ通知する。|
|MasterDetailPage|MasterDetailPage、MasterPage、DetailPageの順で通知する。|
|NavigationPage|NavigationPage、NavigationStackの全ページの昇順で通知する。<br>DeepLinkを行っていない場合は、実質子ページは一つであるため、NavigationPageへの通知の後に、Currentへ通知されることになる。|
|TabbedPage|TabbedPage、全タブの昇順で通知される。|
|CarouselPage|CarouselPage、全子Pageの昇順で通知される。|

## [OnLoaded](#詳細仕様)  

|項目|説明|
|:--|:--|
|概要|画面遷移後の画面の表示後に通知される。<br>（PushAsyncなどで）前方へ画面遷移した場合だけでなく、何らかの戻る処理で戻ってきたタイミングでも通知される。<br>OnUnloadで、画面の非活性化処理を想定しており、OnLoadedでは逆に活性化処理を想定している。<br>物理バックボタンやスワイプで戻ってくることも想定しているため、画面遷移パラメーターには対応しない。|
|対応インターフェース|IPageLoadedAware|
|基本通知順序|画面遷移処理の完了後、OnUnloadの前に通知される。<br>親から子へトップダウンで通知する。<br>またPageへ先に通知した後に、ViewModelへ通知する。|
|MasterDetailPage|MasterDetailPage、MasterPage、DetailPageの順で通知する。|
|NavigationPage|NavigationPage、Currentの順で通知される。<br>NavigationStackに多数のページがあった場合でも、Currentにのみ通知する。|
|TabbedPage|TabbedPage、Currentタブの順で通知される。<br>タブが複数あった場合、非表示のタブには通知されない。<br>タブが切り替えられた場合、切り替え後のタブにたいしても通知される。|
|CarouselPage|CarouselPage、Currentページの順で通知される。<br>ページが複数あった場合、非表示のページには通知されない。<br>ページが切り替えられた場合、切り替え後のページにも通知される。|

## [OnUnloaded](#詳細仕様)  

|項目|説明|
|:--|:--|
|概要|（PushAsyncなどで）前方へ画面遷移した際に、遷移元の画面に通知される。<br>画面の非活性化処理の実装を想定している。  <br>戻ってくる可能性のあるPageにたいして通知される。|
|対応インターフェース|IPageUnloadedAware|
|基本通知順序|画面遷移処理の完了後、OnLoadedの後に通知される。<br>子から親へボトムアップで通知する。<br>またViewModelへ通知した後、Pageへ先に通知される。|
|MasterDetailPage|DetailPage、MasterPage、MasterDetailPageの順で通知する。|
|NavigationPage|Currentページ、NavigationPageの順で通知される。<br>NavigationStackに多数のページがあった場合でも、Currentにのみ通知する。|
|TabbedPage|Currentタブ、TabbedPageの順で通知される。<br>タブが複数あった場合、非表示のタブには通知されない。<br>タブが切り替えられた場合、切り替え前に表示していたタブのページにたいしても通知される。|
|CarouselPage|Currentページ、CarouselPageの順で通知される。<br>ページが複数あった場合、非表示のページには通知されない。<br>ページが切り替えられた場合、切り替え前に表示していたページにも通知される。|

## [OnClosed](#詳細仕様)  

|項目|説明|
|:--|:--|
|概要|Pageが破棄されるタイミングで通知される。<br>リソースの解放や、イベントの購読解除などを想定している。<br>戻る処理が発生した場合の元画面にたいして通知される。|
|対応インターフェース|IPageClosedAware|
|基本通知順序|戻る処理の完了後、OnLoadedの後に通知される。<br>子から親へボトムアップで通知する。<br>またViewModelへ通知した後、Pageへ先に通知される。|
|MasterDetailPage|DetailPage、MasterPage、MasterDetailPageの順で通知する。|
|NavigationPage|NavigationStackの全ページの降順、NavigationPageの順で通知される。<br>NavigationPageそのものがPopされたり、MainPageが変更された場合が該当する。|
|TabbedPage|全タブの降順、TabbedPageの順で通知される。|
|CarouselPage|全子ページの降順、CarouselPageの順で通知される。|

# [画面遷移時パラメーター仕様](#詳細仕様)  

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

# [Applicationライフサイクルイベント仕様](#詳細仕様)  

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

## [OnSleep](#詳細仕様)  

|項目|説明|
|:--|:--|
|概要|アプリケーションが背面に移動した場合などに通知される。<br>通知タイミングはXamarin.Forms.ApplicationのOnSleepの仕様に準ずる|
|対応インターフェース|IApplicationLifecycleAware|
|基本通知順序|全ての画面へ通知する。<br>ModalStack上のページすべてに降順で通知した後、MainPageへ通知する。<br>ModalStack上のページがネストした構造を持っていた場合、子から親へボトムアップで通知する。<br>またViewModelへ通知した後、Pageへ先に通知される。|
|MasterDetailPage|DetailPage、MasterPage、MasterDetailPageの順で通知する。|
|NavigationPage|NavigationStackの全ページの降順、NavigationPageの順で通知される。<br>NavigationPageそのものがPopされたり、MainPageが変更された場合が該当する。|
|TabbedPage|全タブの降順、TabbedPageの順で通知される。|
|CarouselPage|全子ページの降順、CarouselPageの順で通知される。|


## [OnResume](#詳細仕様)  

|項目|説明|
|:--|:--|
|概要|アプリケーションが背面に移動した場合などに通知される。<br>通知タイミングはXamarin.Forms.ApplicationのOnResumeの仕様に準ずる|
|対応インターフェース|IApplicationLifecycleAware|
|基本通知順序|全ての画面へ通知する。<br>MainPageへ通知した後、ModalStack上のすべてのページへ昇順で通知する。<br>ModalStack上のページがネストした構造を持っていた場合、親から子へトップダウンで通知する。<br>またPageへ先に通知した後に、ViewModelへ通知する。|
|MasterDetailPage|MasterDetailPage、MasterPage、DetailPageの順で通知する。|
|NavigationPage|NavigationPage、NavigationStackの全ページの昇順で通知する。<br>DeepLinkを行っていない場合は、実質子ページは一つであるため、NavigationPageへの通知の後に、Currentへ通知されることになる。|
|TabbedPage|TabbedPage、全タブの昇順で通知される。|
|CarouselPage|CarouselPage、全子Pageの昇順で通知される。|
