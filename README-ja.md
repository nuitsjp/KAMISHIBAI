![KAMISHIBAI for Xamarin.Forms](https://raw.githubusercontent.com/nuitsjp/KAMISHIBAI/master/logo_wide.png)

KAMISHIBAI for Xamarin.Formsは、もっとも自由な画面遷移を提供するXamarin.Forms用の画面遷移フレームワークです。  
KAMISHIBAIは、あなたに次のものを提供します。  

* Xamarin.Formsの可能性を一切阻害しない画面遷移アーキテクチャ  
* 型安全性の保障された画面遷移時パラメーター  
* 画面遷移にともなう、適切なイベントの通知  
* 柔軟な拡張性  
* MVVMのサポート  

# Getting Started with KAMISHIBAI

さて、まずはKAMISHIBAIの最も基本的な画面遷移の実装を見ていただきましょう。  

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
