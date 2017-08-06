[![KAMISHIBAI for Xamarin.Forms](https://raw.githubusercontent.com/nuitsjp/KAMISHIBAI/master/logo_wide.png)](https://github.com/nuitsjp/KAMISHIBAI/blob/master/README-ja.md)

# KAMISHIBAI for Xamarin.Forms

KAMISHIBAI for Xamarin.Formsは、MVVMパターンをサポートする画面遷移ライブラリです。  
KAMISHIBAIはXamarin.Formsの機能を一切制限せず、つぎの機能拡張を行います。  

* 型安全性の保障された画面遷移時パラメーター  
* 画面遷移にともなう適切なイベント通知  
* MVVMのサポート  

Xamarin.Formsで実現可能なあらゆる画面遷移は、KAMISHIBAIを利用することで、より簡単に実現する事ができます。  

# Overview  

KAMISHIBAIの最大の特徴は、構造的にモノリシックな一枚岩はなく、つぎの二つに分けて画面遷移を実現していることです。

1. ViewModel層から画面遷移要求を発信する  
2. 画面遷移要求を受け、View層で画面遷移する  

それぞれについて概略を順にみていきましょう。

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
PushModalAsyncビヘイビアを注目してください。  
RequestSecondPageの遷移要求を受けて、x:TypeArgumentsで指定されたSecondPageへモーダル遷移します。  

KAMISHIBAIではViewModelからの要求を受けて、ビヘイビアで画面処理を行います。  
もちろんINavigationのメソッドは全て用意されていますし、ビヘイビアを自ら作成することも可能です。  
このためKAMISHIBAIではXamarin.Formsで可能な、あらゆる画面遷移を実現することが可能です。  

さらにKAMISHIBAIでは、つぎのような魅力的な機能を提供しています。  

* 型安全性の保障された画面遷移時パラメーター  
* 画面遷移にともなう適切なイベント通知

イベント通知には以下のものも含まれます。  

* アプリケーションのSleep、Resume  
* TabbedPageやCarouselPageのタブ（ページ）の切り替え  
* 物理バックキーの押下やスワイプ

KAMISHIBAIに興味をもっていただけましたか？  
KAMISHIBAIでは、あなたをサポートする次のコンテンツを提供しています。  
さあ、Xamarin.Formsで最も自由な画面遷移ライブラリを使ってみましょう！  

## Contents

1. [How to install](#how-to-install)
2. Documents
    1. [KAMISHIBAI入門](Document/1-Hello-KAMISHIBAI-ja.md)  
    2. [KAMISHIBAI仕様](Document/2-Reference-ja.md)
    2. [KAMISHIBAIアーキテクチャ概要](Document/3-Architecture-Overview-ja.md)
3. [Samples](https://github.com/nuitsjp/KAMISHIBAI-Samples)

## How to install  

[NuGetから](https://www.nuget.org/packages/Kamishibai.Xamarin.Forms)インストールしてください。

```txt
> Install-Package Kamishibai.Xamarin.Forms
```
