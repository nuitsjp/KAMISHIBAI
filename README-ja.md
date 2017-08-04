![KAMISHIBAI for Xamarin.Forms](https://raw.githubusercontent.com/nuitsjp/KAMISHIBAI/master/logo_wide.png)

# KAMISHIBAI for Xamarin.Forms

KAMISHIBAI for Xamarin.Formsは、MVVMパターンをサポートする画面遷移ライブラリです。  
KAMISHIBAIはXamarin.Formsの機能を一切制限せず、つぎの機能拡張を行います。  

* 型安全性の保障された画面遷移時パラメーター  
* 画面遷移にともなう適切なイベント通知  
* MVVMのサポート  

Xamarin.Formsで実現可能なあらゆる画面遷移は、KAMISHIBAIを利用することで、より簡単に実現する事ができます。  

## Hello, KAMISHIBAI  

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
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:views="clr-namespace:SimplestSample.Views;assembly=SimplestSample"
             xmlns:mvvm="clr-namespace:Kamishibai.Xamarin.Forms.Mvvm;assembly=Kamishibai.Xamarin.Forms"
             x:Class="SimplestSample.Views.FirstPage">
    <ContentPage.Behaviors>
        <mvvm:PushAsync Request="{Binding RequestSecondPage}" x:TypeArguments="views:SecondPage"/>
    </ContentPage.Behaviors>
    <StackLayout>
        <Button Text="Navigate to Second" Command="{Binding NavigateCommand}"/>
    </StackLayout>
</ContentPage>
```
「mvvm:PushAsync」ビヘイビアを注目してください。  
RequestSecondPageの遷移要求を受けて、x:TypeArgumentsで指定されたSecondPageへ、INavigation#PushAsync()を利用して画面遷移します。  

KAMISHIBAIではこの他に、つぎのような魅力的な機能を提供しています。  

* 型安全性の保障された画面遷移時パラメーター  
* 画面遷移にともなう適切なイベント通知  

KAMISHIBAIに興味をもっていただけましたか？  
KAMISHIBAIでは、あなたをサポートする次のコンテンツを提供しています。  
さあ、Xamarin.Formsで最も自由な画面遷移ライブラリを使ってみましょう！  

## Documents

1. [How to install](#how-to-install)
2. [Getting Started with KAMISHIBAI](#getting-started-with-kamishibai)

## How to install  

Install from NuGet.

```txt
> Install-Package Kamishibai.Xamarin.Forms
```

## Getting Started with KAMISHIBAI

さて、まずはKAMISHIBAIの基本的な画面遷移の実装を見ていただきましょう。  

KAMISHIBAIの最大の特徴は、画面遷移の要求と画面遷移処理の二つが、完全に分離されている点にあります。  
KAMISHIBAIにおける画面遷移は、つぎの二つのステップにて実現します。  

1. ViewModelからViewへ画面遷移を要求する  
2. 画面遷移要求をViewは受け取り、「どこに」「どのように」遷移するか決定する  

そして必要であれば遷移イベントや遷移パラメーターを受け取ります。  

ここではFirstPageからSecondPageへ画面遷移し、その際にDateTimeを受け渡す例を示します。  
その際にNavigationPageを利用します。  

この例の完全なコードは次のリポジトリのSimplestSampleを参照してください。  
[https://github.com/nuitsjp/KAMISHIBAI-Samples](https://github.com/nuitsjp/KAMISHIBAI-Samples)

## ViewModelから画面遷移を要求する  

画面遷移要求にはINavigationRequestを利用します。  
FirstPageのViewModelに、つぎのようなプロパティを記述します。  

```cs
public INavigationRequest<DateTime> RequestSecondPage { get; } = new NavigationRequest<DateTime>();
```

そしてINavigationRequestのRaiseAsyncメソッドを呼び出すことで、Viewへ画面遷移を要求します。  

```cs
RequestSecondPage.RaiseAsync(SelectedDate)
```

## 要求に基づきViewで画面遷移する

ViewではFirstPageのBehaviorにつぎのように記述します。

```xaml
<ContentPage.Behaviors>
    <mvvm:PushAsync Request="{Binding RequestSecondPage}"
                    x:TypeArguments="views:SecondPage" />
</ContentPage.Behaviors>
```

この記述は、つぎを表しています。  

1. INavigationのPushAsyncメソッドを利用して画面遷移する  
2. RequestSecondPageから遷移要求を受ける
3. SecondPageへ遷移する  

KAMISHIBAIでは、Xamarin.Formsの画面遷移オブジェクトであるINavigationのメソッドと同等のビヘイビアが全て用意されています。  
もちろん、独自の拡張遷移を実装する事でXamarin.Formsに可能なあらゆる画面遷移を実現することが可能です。  

## SecondPageでパラメーターを受け取る  

KAMISHIBAIでは必要な画面遷移イベントを受け取ることができます。  
パラメーターの受け取りは、ページの初期化イベントを受けることで取得できます。  
IPageInilializeAwareインターフェースを実装しましょう。  

```cs
public class SecondPageViewModel : IPageInilializeAware<DateTime>, INotifyPropertyChanged
{
    public void OnInitialize(DateTime selectedDate)
    {
        SelectedDate = selectedDate;
    }
    ...
```

以上でDateTime型のパラメーターを伴う、画面遷移を実現できました。  
