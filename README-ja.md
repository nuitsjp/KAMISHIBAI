![KAMISHIBAI for Xamarin.Forms](https://raw.githubusercontent.com/nuitsjp/KAMISHIBAI/master/logo_wide.png)

# KAMISHIBAI for Xamarin.Forms

KAMISHIBAI for Xamarin.Formsは、MVVMパターンをサポートする画面遷移ライブラリです。  
KAMISHIBAIはXamarin.Formsの機能を一切制限せず、つぎの機能拡張を行います。  

* 型安全性の保障された画面遷移時パラメーター  
* 画面遷移にともなう適切なイベント通知  
* MVVMのサポート  

Xamarin.Formsで実現可能なあらゆる画面遷移は、KAMISHIBAIを利用することで、より簡単に実現する事ができます。  

## Overview  

KAMISHIBAIの最も簡単な画面遷移は、二つのステップで実現できます。  

1. ViewModelから画面遷移を要求する  
2. 要求に従い、Viewで画面遷移を実行する  

### ViewModelから画面遷移を要求する  
ViewModelに、つぎのコードを記載します。  

```cs
public INavigationRequest RequestSecondPage { get; } = new NavigationRequest();

public ICommand NavigateCommand => new Command(() => RequestSecondPage.RaiseAsync();
```

Commandが実行されたら、SecondPageへの遷移を要求します。  

### 要求に従い、Viewで画面遷移を実行する

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

KAMISHIBAIではViewModelからの要求を受けて、Behaviorで該当するINavigationのメソッドを呼び出します。  
標準で、INavigationのメソッドは全て用意されていますし、Behaviorを自ら作成することも可能です。  
このためKAMISHIBAIではXamarin.Formsで可能な、あらゆる画面遷移を実現することが可能です。  

さらにKAMISHIBAIでは、つぎのような魅力的な機能を提供しています。  

* 型安全性の保障された画面遷移時パラメーター  
* 画面遷移にともなう適切なイベント通知  

KAMISHIBAIに興味をもっていただけましたか？  
KAMISHIBAIでは、あなたをサポートする次のコンテンツを提供しています。  
さあ、Xamarin.Formsで最も自由な画面遷移ライブラリを使ってみましょう！  

## Contents

1. [How to install](#how-to-install)
2. Documents
    1. [Hello, KAMISHIBAI](Document/1-Hello-KAMISHIBAI-ja.md)
3. [Samples](https://github.com/nuitsjp/KAMISHIBAI-Samples)

## How to install  

Install from NuGet.

```txt
> Install-Package Kamishibai.Xamarin.Forms
```
