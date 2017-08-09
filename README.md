[Japanese](README-ja.md)

[![KAMISHIBAI for Xamarin.Forms](https://raw.githubusercontent.com/nuitsjp/KAMISHIBAI/master/logo_wide.png)](https://github.com/nuitsjp/KAMISHIBAI/blob/master/README-ja.md)

# KAMISHIBAI for Xamarin.Forms

KAMISHIBAI for Xamarin.Forms is Page navigation library that supports MVVM patterns.  
KAMISHIBAI will grant the following features to Xamarin.Forms.

* Generic Type Parameters on Page navigation  
* Consistent event notification on Page navigation  
* Supports Page navigation from View Model  

KAMISHIBAI provides Navigator which added these functions to Xamarin.Forms.INavigation. KAMISHIBAI does not limit Xamarin.Forms. And providing a means to use the Navigator from the View Model.  
Keeping the use of Xamarin.Forms.INavigation. For this reason, offers low-cost learning.  

In other words, KAMISHIBAI does not provide highly abstracted Page navigation services.  

It can be used with any MVVM framework. However, only one constraint will occur.  
Please use KAMISHIBAI for screen transition.  

# Overview  

画面遷移を実現するにあたり、KAMISHIBAIの最大の特徴は、利用者から見てモノリシックな一枚岩な構造ではなく、役割を分割して実現していることにあります。  

1. ViewModel層から画面遷移要求を発信する  
2. 画面遷移要求を受け、View層で画面遷移する  

これらを組み合わせることで画面遷移を実現しています。それぞれについて概略をみていきましょう。

## ViewModel層から画面遷移要求を発信する  

画面遷移要求を発信するには、INavigationRequestを利用します。  
実行されたCommandから、SecondPageページへの画面遷移を要求する場合、ViewModeに次のように記載します。

```cs
public ICommand NavigateCommand => new Command(() => RequestSecondPage.RaiseAsync());

public INavigationRequest RequestSecondPage { get; } = new NavigationRequest();
```

## 画面遷移要求を受け、View層で画面遷移する

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage ...>
    <ContentPage.Behaviors>
        <mvvm:PushModalAsync Request="{Binding RequestSecondPage}" x:TypeArguments="views:SecondPage"/>
    </ContentPage.Behaviors>
    <StackLayout>
        <Button Text="Navigate to Second" Command="{Binding NavigateCommand}"/>
    </StackLayout>
</ContentPage>
```
PushModalAsyncビヘイビアに注目してください。  

```xaml
<mvvm:PushModalAsync Request="{Binding RequestSecondPage}" x:TypeArguments="views:SecondPage"/>
```
RequestSecondPageの遷移要求を受けて、x:TypeArgumentsで指定されたSecondPageへモーダル遷移します。  

KAMISHIBAIではViewModelからの要求を受けて、ビヘイビアで画面遷移を行います。  
もちろんINavigationの全てのメソッドに対応するビヘイビアはあらかじめ用意されていますし、自ら作成することも可能です。  
このためKAMISHIBAIではXamarin.Formsで可能な、あらゆる画面遷移を実現することが可能です。  

さらにKAMISHIBAIでは、つぎのような魅力的な機能を提供しています。  

* 型安全性の保障された画面遷移時パラメーター  
* 画面遷移における、一貫性と再帰性を伴ったイベント通知  

つぎのようなケースで画面遷移をハンドルすることは、他のケースと比較して困難ですが、KAMISHIBAIであれば一貫性を保った通知を実現します。  

* 物理バックキーの押下やスワイプ
* TabbedPageやCarouselPageのタブ（ページ）の切り替え  
* アプリケーションのSleep、Resume  

また、MasterDetailPageやTabbedPageなどはPageをネストします。それらの多階層化した画面の末端まで再帰的にイベントを通知します。  

KAMISHIBAIに興味をもっていただけましたか？  
KAMISHIBAIでは、あなたをサポートする次のコンテンツを提供しています。  
さあ、Xamarin.Formsで最も自由な画面遷移ライブラリを使ってみましょう！  

## Contents

1. [How to install](#how-to-install)
2. Documents
    1. [KAMISHIBAIチュートリアル](Document/1-Hello-KAMISHIBAI-ja.md)  
    2. [リファレンス](Document/2-Reference-ja.md)
    2. [アーキテクチャ概要](Document/3-Architecture-Overview-ja.md)
3. [Samples](https://github.com/nuitsjp/KAMISHIBAI-Samples)

## How to install  

[NuGetから](https://www.nuget.org/packages/Kamishibai.Xamarin.Forms)インストールしてください。

```txt
> Install-Package Kamishibai.Xamarin.Forms
```
