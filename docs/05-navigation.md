---
title: "ナヴィゲーション"
---

KAMISHIBAIのナヴィゲーションメソッドには、2種類あります。

1. ViewModelのコンストラクターから自動生成される安全なメソッド
2. ViewModelのTypeやインスタンスを渡してナヴィゲーションするメソッド

こちらに説明は記載しますが、サンプルアプリケーションも参考にしてください。

- [KamishibaiApp](https://github.com/nuitsjp/KAMISHIBAI/tree/master/Sample/KamishibaiApp)
- [SampleBrowser](https://github.com/nuitsjp/KAMISHIBAI/tree/master/Sample/SampleBrowser)

# 自動生成メソッド

ViewModelにNavigate属性を宣言することで、IPresentationServiceに専用のナヴィゲーションメソッドが生成されます。

たとえばつぎのようなViewModelが存在したとします。

```cs
[Navigate]
public class FirstViewModel
{
    public FirstViewModel(string message)
    {
        Message = message;
    }

    public string Message { get; }
}
```

この場合、つぎのようなコードが生成されます。

```cs
Task<bool> NavigateToFirstAsync(string message, string frameName = "");
```

使用例

```cs
await _presentationService.NavigateToFirstAsync("Hello, Navigation Parameter!");
```

NavigateTo+ナヴィゲーション名+Asyncという名称のメソッドが作成され、ViewModelのコンストラクターの引数にframeNameを追加した引数が設定されます。

ナヴィゲーション名はViewModelのクラス名の末尾からつぎの文字列を順にすべて削除したものになります。

1. ViewModel
2. Page
3. Window
4. Dialog

たとえばViewModelがFirstPageViewModelだった場合、NavigateToFirstAsyncになります。

# 戻る

IPresentationServiceのつぎのメソッドを利用します。

```cs
Task<bool> GoBackAsync(string frameName = "");
```

使用例

```cs
await _presentationService.GoBackAsync();
```

# ViewModelへ依存性の注入

ViewModelにナヴィゲーションのパラメーターではなく、DIコンテナーから何らかのオブジェクトを注入したい場合、コンストラクター引数にInject属性を宣言します。

たとえばロガーを注入したい場合、つぎのように記述します。

```cs
[Navigate]
public class FirstViewModel
{
    private readonly ILogger<FirstViewModel> _logger;

    public FirstViewModel(
        string message, 
        [Inject] ILogger<FirstViewModel> logger)
    {
        Message = message;
        _logger = logger;
    }
```

この場合に生成されるナヴィゲーションメソッドはつぎのとおりです。

```cs
Task<bool> NavigateToFirstAsync(string message, string frameName = "");
```

ロガーの注入がない場合と完全に同一のシグニチャーとなります。

ナヴィゲーション先のViewModelだけが必要とするサービスなどを注入することで、ViewModel間の依存関係を疎に保つことができます。

# コンストラクターにframeNameを渡す

ViewModelへ、ナヴィゲーションに用いるフレーム名を渡したい場合があります。

```cs
[Navigate]
public class FirstViewModel
{
    public FirstViewModel(string frameName, string message)
    {
        ...
```

この場合、つぎのようなコードが生成されます。

```cs
Task<bool> NavigateToFirstAsync(string frameName, string message);
```

通常オプショナルで追加される引数が削除され、コンストラクターのシグニチャーに従います。

# 複数コンストラクター

KAMISHIBAIではViewModelで複数のコンストラクターを宣言することが可能です。

```cs
[Navigate]
public class FirstViewModel
{
    public FirstViewModel() : this("Default message")
    {
    }

    public FirstViewModel(string message)
    {
        Message = message;
    }
```

この場合、コンストラクターごとにナヴィゲーションメソッドが生成されます。

```cs
Task<bool> NavigateToFirstAsync(string frameName = "");
Task<bool> NavigateToFirstAsync(string message, string frameName = "");
```

# Type指定ナヴィゲーション

たとえばつぎのようなサイドメニューがある場合。

![](/Images/side-menu.png)

メニューをリストで作成しておき、動的にサイドメニューをコントロールしたいということが良くあります。

この場合、ナヴィゲーションにパラメーターは伴いませんし、コード生成されたメソッドではなく、ViewModelのTypeを指定した方がシンプルに実装できることがあります。

そのためKAMISHIBAIではつぎのナヴィゲーションメソッドを用意しています。

```cs
Task<bool> NavigateAsync(Type viewModelType, string frameName = "");
Task<bool> NavigateAsync<TViewModel>(string frameName = "");
Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, string frameName = "");
```

Typeを引数や型引数で指定してナヴィゲーションすることが可能です。

先の例では先頭を利用することになるでしょう。

また少し異なりますが、ViewModelのインスタンスを指定して渡すことも可能です。

```cs
Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, string frameName = "") where TViewModel : notnull;
```

サブ画面などで、表示内容を保持しておきたい場合などに利用できます。

