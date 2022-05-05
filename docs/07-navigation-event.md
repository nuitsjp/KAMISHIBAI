---
title: "画面遷移イベント"
---

一貫性ある画面遷移イベントは、KAMISHIBAIの強みの1つです。

画面遷移イベントは2つ要素によって成り立っています。

1. イベントの通知パターン
2. イベントの通知タイミング

# イベントの通知パターン

KAMISHIBAIではつぎの2つの通知パターンがあります。

1. ViewModelのインターフェイスへの通知
2. NavigationFrameのイベントハンドラー

## ViewModelのインターフェイスへの通知

ViewModelでイベントに対応した各種インターフェイスを実装することでイベント通知を受け取れます。

たとえば遷移後のViewModelで遷移完了の通知を受け取りたい場合は、つぎのように実装します。

```cs
public class MainViewModel : INavigatedAsyncAware
{
    public Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        ...
    }
```

各種インターフェイスについては後述します。

## NavigationFrameのイベントハンドラー

イベント通知インターフェイスを利用した場合、基本的に遷移にかかわるViewModelで受け取る必要があります。

遷移に関与しない箇所でイベントを受け取りたい場合などは、INavigationFrameのイベントハンドラーを利用してください。

```cs
INavigationFrame frame = _presentationService.GetNavigationFrame();
frame.Navigated += FrameOnNavigated;
```

IPresentationServiceから対象のNavigationFrameを取得してイベントを受信します。

# イベントの通知タイミング

KAMISHIBAIでは画面遷移前後の任意のタイミングで通知を受け取ることができます。

![](/images/books/kamishibai/navigation-event.png)

大まかに上図のタイミングでイベントを受信できます。

Page1からPage2に画面遷移する場合、次の順で処理が行われます。

1. 遷移元のOnPausing系の通知
2. 遷移先のOnNavigating系の通知
3. 画面遷移
4. 遷移先のOnNavigated系の通知
5. 遷移元のOnPaused系の通知

それぞれの系統にはViewModelへの同期・非同期通知とINavigationServiceのイベントハンドラーの3種類（OnDisposedだけ例外。詳細は後述）が存在します。

戻る場合においても同様です。

# イベントの詳細：前進

|順序|通知先|インターフェイス|メンバー|キャンセル|
|--:|:--|--|--|:-:|
|1|遷移元ViewModel|IPausingAsyncAware|OnPausingAsync|✅|
|2|遷移元ViewModel|IPausingAware|OnPausing|✅|
|3|任意|INavigationFrame|Pausing|✅|
|4|遷移先ViewModel|INavigatingAsyncAware|OnNavigatingAsync|✅|
|5|遷移先ViewModel|INavigatingAware|OnNavigating|✅|
|6|任意|INavigationFrame|Navigating|✅|
|7|-|画面遷移|-|-|
|8|遷移先ViewModel|INavigatedAsyncAware|OnNavigatedAsync|-|
|9|遷移先ViewModel|INavigatedAware|OnNavigated|-|
|10|任意|INavigationFrame|Navigated|-|
|11|遷移元ViewModel|IPausedAsyncAware|OnPausedAsync|-|
|12|遷移元ViewModel|IPausedAware|OnPaused|-|
|13|任意|INavigationFrame|Paused|-|

No.1～6では遷移をキャンセルすることが可能です。

```cs
public class MainViewModel : INavigatedAsyncAware
{
    public Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        args.Cancel = true
        ...
    }
```

キャンセルされた以降のイベントは通知されません。

# イベント詳細：後進

|順序|通知先|インターフェイス|メンバー|キャンセル|
|--:|:--|--|--|:-:|
|1|遷移元ViewModel|IDisposingAsyncAware|OnDisposingAsync|✅|
|2|遷移元ViewModel|IDisposingAware|OnDisposing|✅|
|3|任意|INavigationFrame|Disposing|✅|
|4|遷移先ViewModel|IResumingAsyncAware|OnResumingAsync|✅|
|5|遷移先ViewModel|IResumingAware|OnResuming|✅|
|6|任意|INavigationFrame|INavigationFrame|✅|
|7|-|画面戻し|-|-|
|8|遷移先ViewModel|IResumedAsyncAware|OnResumedAsync|-|
|9|遷移先ViewModel|IResumedAware|OnResumed|-|
|10|任意|INavigationFrame|Resumed|-|
|11|遷移元ViewModel|IDisposedAsyncAware|OnDisposedAsync|-|
|12|遷移元ViewModel|IDisposedAware|OnDisposed|-|
|13|遷移元ViewModel|IDisposable|Dispose|-|
|14|任意|INavigationFrame|Disposed|-|

No.1～6では遷移をキャンセルすることが可能です。

一般的な .NETの実装との相互運用を考慮して、IDisposableインターフェイスでも通知を受け取ることが可能です。