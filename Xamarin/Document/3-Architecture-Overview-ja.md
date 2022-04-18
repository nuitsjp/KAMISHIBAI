[[Home]](../README-ja.md) > アーキテクチャ概要

# アーキテクチャ概要  
KAMISHIBAIの特に重要な要素に、つぎの三つがあります。  

* [Navigator](#navigator)
* [NavigationRequest](#navigationrequest)  
* [NavigationBehavior（PushAsyncやPushModalAsyncの親クラス）](#navigationbehavior)  

![](3-Architecture-Overview/KAMISHIBAI.png)

# Navigator

NavigatorはXamarin.Formsで画面遷移を指示するXamarin.Forms.INavigationのラッパークラスです。  
INavigationをラップすることで、Navigatorは画面遷移にあたり、つぎの機能を付与します。  

* [子Pageへの再帰性](#子pageへの再帰性をもったイベント通知)と[高い一貫性](#一貫性を保ったイベント通知)を保った画面遷移時のイベント通知  
    * OnInitialize
    * OnLoaded
    * OnUnloaded
    * OnClosed
* 型安全性の保たれた画面遷移パラメーター  

## 画面遷移時のイベント通知
Navigatorでは子Pageへの再帰性と高い一貫性を持ったイベントの通知を行います。  
イベントは4種類あり、それぞれ対応するインターフェースを実装することによってViewModelだけではなく、View（Page）でもイベントを受け取ることが可能です。  

* [IPageInitializeAware](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/IPageInitializeAware.cs)  
* [IPageLoadedAware](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/IPageLoadedAware.cs)  
* [IPageUnloadedAware](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/IPageUnloadedAware.cs)  
* [IPageClosedAware](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/IPageClosedAware.cs)

また厳密には画面遷移とは異なりますが、ApplicationのSleepとResumeのイベントも受け取ることが可能です。  

* [IApplicationOnSleepAware](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/IApplicationOnSleepAware.cs)  
* [IApplicationOnResumeAware](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/IApplicationOnResumeAware.cs)  

### 子Pageへの再帰性をもったイベント通知

KAMISHIBAIでは、例えばMasterDetailPageがTabbedPageを持っているような場合、TabbedPageのTabに該当するPageのような、末端のPageまで遷移イベントを通知します。  
イベントの通知そのものはNavigatorからつぎのクラスに移譲して行っており、LifecycleNotifierの中で再帰的にイベント通知を処理しています。  

* [LifecycleNoticeService](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/LifecycleNoticeService.cs)  
* [LifecycleNotifier](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/LifecycleNotifier.cs)  

### 一貫性を保ったイベント通知  

Navigatorでは、例えばつぎのような解決の難しい画面遷移であっても、一貫性を保ってイベントを通知します（一部はNavigator以外によって実現している）。  

1. NavigationPageのNavigationBar、物理バックボタン（AndroidやWin10m）によって戻る時  
2. TabbedPageのタブの切り替えや、CarouselPageのページ切り替え時  
3. Modal遷移時の物理バックボタン（AndroidやWin10m）によって戻る時  

1.および2.は画面遷移する際に、遷移先のPageに外部からBehaviorをインジェクションすることによって解決しています。  
具体的にはつぎのクラスの実装を参照。  

* [BehaviorInjector](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/BehaviorInjector.cs)
* [NavigationPageBehavior](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/NavigationPageBehavior.cs)
* [MultiPageBehavior&lt;TPage>](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/MultiPageBehavior.cs)  

3.のModal遷移時の物理バックボタンについては、つぎのクラスを参照。  

* [ApplicationService](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/ApplicationService.cs)  

正直、Initializeメソッドはなくても動作するようにしたかったが、テスト性を確保しつつ必要な要件を満たす為には、回避策が思い当たらりませんでした。（UnitTestをマルチスレッドで実施する前提）  
初期画面から物理Backボタンで戻って、再度アプリケーションアイコンからアプリを起動された場合に、Applicationクラスが再生成されてCurrentが変わりますが、それを検知する方法がないと思われる為、AppクラスのコンストラクタでApplicationServiceのInitializeメソッドを呼ばせざるを得ません。  

# NavigationRequest

INavigationRequestの実装クラスで、ViewModelからViewへ画面遷移要求を通知します。  
通知時に引数を持たない[INavigationRequestインターフェース](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/INavigationRequest.cs#L5)と、引数を持つ[INavigationRequest<TPara>インターフェース](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/INavigationRequest.cs#L12)の２種類があります。  

正直[この通り](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/NavigationRequest.cs)たいした実装を持ちませんが、画面遷移機能を持ったクラスをViewModelから操作するのではなく、ViewModel層からはあくまで遷移を通知させるだけという設計にこそ価値があります。  

![](3-Architecture-Overview/KAMISHIBAI.png)

冒頭と同じ図ですが、画面遷移の実態をView層へ完全に切り出したことで、Xamarin.Formsで可能なあらゆる画面遷移を実現可能としています。  
高レベルな画面遷移サービスを作成した場合、想定外の遷移が必要となった場合の対処が困難ですが、このアーキテクチャによってその点が解決されています。  
また高レベルな画面遷移サービスは、その振舞について正しく理解するコストがそれなりに必要となりますが、KAMISHIBAIはあくまでINavigationの薄いラッパーをViewModelから呼び出しやすくしているだけであるため、習得コストの面でも有利です。  

# NavigationBehavior  

NavigationBehaviorは、NavigationRequestをバインドし、画面遷移要求を受け取ってNavigatorを呼び出し画面遷移します。NavigationBehaviorは抽象クラスであり、実際にはPushAsyncやPopAsyncなどの具象クラスが存在します。  

画面遷移先をXAML上の型パラメーターで指定するアイディアがほぼすべてで、その発想がなければKAMISHIBAI自体が存在しなかったでしょう（ただしINavigationを薄くラップして遷移通知と遷移パラメーターに対応するNavigator自体は作っていたと思われます）。  

さてKAMISHIBAIとしては以下の通り、INavigationに存在する画面遷移メソッドに対応する全てのメソッドが用意されていると共に、利用者が独自にNavigationBehaviorを継承して、複雑で固有な画面遷移を実装することが可能となっています。  

* [InsertPageBefore](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/InsertPageBefore.cs)  
* [PopAsync](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/PopAsync.cs)  
* [PopModalAsync](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/PopModalAsync.cs)  
* [PopToRootAsync](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/PopToRootAsync.cs)  
* [PushAsync](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/PushAsync.cs)  
* [PushModalAsync](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/PushModalAsync.cs)  
* [RemovePage](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/RemovePage.cs)  
* [SetMainPage](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Mvvm/SetMainPage.cs)  

SetMainPageだけはINavigationにはメソッドが存在しません。そのため、内部の実装もNavigatorではなくApplicationServiceを呼び出しています。  
