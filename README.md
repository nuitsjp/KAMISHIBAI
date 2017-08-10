[Japanese](README-ja.md)

[![KAMISHIBAI for Xamarin.Forms](https://raw.githubusercontent.com/nuitsjp/KAMISHIBAI/master/logo_wide.png)](https://github.com/nuitsjp/KAMISHIBAI/blob/master/README-ja.md)

# KAMISHIBAI for Xamarin.Forms

KAMISHIBAI for Xamarin.Forms is Page navigation library that supports MVVM patterns.  
KAMISHIBAI will grant the following features to Xamarin.Forms.

* Consistent event notification on Page navigation  
* Generic Type Parameters on Page navigation  
* Compatibility of MVVM Pattern and direct manipulation feeling against INavigation  

And KAMISHIBAI does not limit Xamarin.Forms. All possible Navigation in Xamarin.Forms is also possible in KAMISHIBAI.

# Overview  

The feature of KAMISHIBAI's Navigation Service is that it is not a monolithic structure. Navigation is separated into two roles.

1. Send Page navigation request from View Model to View  
2. Based on Page navigation request, navigation Page on the View layer  

Please see specific examples.  

## Send Page navigation request from View Model to View  

Use INavigationRequest to make Page navigation request.  
When executing Command, it is sending RequestSecondPage request.  

```cs
public ICommand NavigateCommand => new Command(() => RequestSecondPage.RaiseAsync());

public INavigationRequest RequestSecondPage { get; } = new NavigationRequest();
```



## Based on Page navigation request, navigation Page on the View layer  

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
Notice the PushModalAsync Behavior.  

```xaml
<mvvm:PushModalAsync Request="{Binding RequestSecondPage}" x:TypeArguments="views:SecondPage"/>
```
Based on RequestSecondPage Navigation request, it is defined to make a Modal Navigation to SecondPage.  
PushModalAsync navigation Page with [Kamisibai.Xamarin.Forms.Navigator](https://github.com/nuitsjp/KAMISHIBAI/blob/master/Source/Kamishibai.Xamarin.Forms/Navigator.cs).  
Navigator wraps Xamarin.Forms.INavigation and adds navigation parameter and navigation event to INavigation.  

## Consistent event notification on Page navigation  




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
