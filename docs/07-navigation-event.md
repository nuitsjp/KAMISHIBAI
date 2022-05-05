---
title: "ナヴィゲーションイベント"
---

一貫性あるナヴィゲーションイベントは、KAMISHIBAIの強みの1つです。

ナヴィゲーションイベントは2つ要素によって成り立っています。

1. イベントの通知パターン
2. イベントの通知タイミング

# イベントの通知パターン

KAMISHIBAIではつぎの2つの通知パターンがあります。

1. ViewModelのインターフェイスへの通知
2. NavigationFrameのイベントハンドラー

## ViewModelのインターフェイスへの通知

ViewModelでイベントに対応した各種インターフェイスを実装することでイベント通知を受け取れます。

たとえばナヴィゲーション後のViewModelでナヴィゲーション完了の通知を受け取りたい場合は、つぎのように実装します。

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

イベント通知インターフェイスを利用した場合、基本的にナヴィゲーションにかかわるViewModelで受け取る必要があります。

ナヴィゲーションに関与しない箇所でイベントを受け取りたい場合などは、INavigationFrameのイベントハンドラーを利用してください。

```cs
INavigationFrame frame = _presentationService.GetNavigationFrame();
frame.Navigated += FrameOnNavigated;
```

IPresentationServiceから対象のNavigationFrameを取得してイベントを受信します。

# イベントの通知タイミング

KAMISHIBAIではナヴィゲーション前後の任意のタイミングで通知を受け取ることができます。

![](/images/books/kamishibai/navigation-event.png)

大まかに上図のタイミングでイベントを受信できます。

Page1からPage2にナヴィゲーションする場合、次の順で処理が行われます。

1. ナヴィゲーション元のOnPausing系の通知
2. ナヴィゲーション先のOnNavigating系の通知
3. ナヴィゲーション
4. ナヴィゲーション先のOnNavigated系の通知
5. ナヴィゲーション元のOnPaused系の通知

それぞれの系統にはViewModelへの同期・非同期通知とINavigationServiceのイベントハンドラーの3種類（OnDisposedだけ例外。詳細は後述）が存在します。

戻る場合においても同様です。

# イベントの詳細：前進

|順序|通知先|インターフェイス|メンバー|キャンセル|
|--:|:--|--|--|:-:|
|1|ナヴィゲーション元ViewModel|IPausingAsyncAware|OnPausingAsync|✅|
|2|ナヴィゲーション元ViewModel|IPausingAware|OnPausing|✅|
|3|任意|INavigationFrame|Pausing|✅|
|4|ナヴィゲーション先ViewModel|INavigatingAsyncAware|OnNavigatingAsync|✅|
|5|ナヴィゲーション先ViewModel|INavigatingAware|OnNavigating|✅|
|6|任意|INavigationFrame|Navigating|✅|
|7|-|ナヴィゲーション|-|-|
|8|ナヴィゲーション先ViewModel|INavigatedAsyncAware|OnNavigatedAsync|-|
|9|ナヴィゲーション先ViewModel|INavigatedAware|OnNavigated|-|
|10|任意|INavigationFrame|Navigated|-|
|11|ナヴィゲーション元ViewModel|IPausedAsyncAware|OnPausedAsync|-|
|12|ナヴィゲーション元ViewModel|IPausedAware|OnPaused|-|
|13|任意|INavigationFrame|Paused|-|

No.1～6ではナヴィゲーションをキャンセルすることが可能です。

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
|1|ナヴィゲーション元ViewModel|IDisposingAsyncAware|OnDisposingAsync|✅|
|2|ナヴィゲーション元ViewModel|IDisposingAware|OnDisposing|✅|
|3|任意|INavigationFrame|Disposing|✅|
|4|ナヴィゲーション先ViewModel|IResumingAsyncAware|OnResumingAsync|✅|
|5|ナヴィゲーション先ViewModel|IResumingAware|OnResuming|✅|
|6|任意|INavigationFrame|INavigationFrame|✅|
|7|-|画面戻し|-|-|
|8|ナヴィゲーション先ViewModel|IResumedAsyncAware|OnResumedAsync|-|
|9|ナヴィゲーション先ViewModel|IResumedAware|OnResumed|-|
|10|任意|INavigationFrame|Resumed|-|
|11|ナヴィゲーション元ViewModel|IDisposedAsyncAware|OnDisposedAsync|-|
|12|ナヴィゲーション元ViewModel|IDisposedAware|OnDisposed|-|
|13|ナヴィゲーション元ViewModel|IDisposable|Dispose|-|
|14|任意|INavigationFrame|Disposed|-|

No.1～6ではナヴィゲーションをキャンセルすることが可能です。

一般的な .NETの実装との相互運用を考慮して、IDisposableインターフェイスでも通知を受け取ることが可能です。