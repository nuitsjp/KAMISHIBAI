---
title: "OpenWindowとOpenDialog"
---

OpenWindowとOpenDialogはよく似ていますが、OpenWindowではモードレスウィンドウが開き、OpenDialogではモーダルウィンドウが開きます。

ここではOpenWindowの説明をしますが、基本的な相違はないためOpenDialogを使う場合は読み替えてください。

新しくウィンドウを開くメソッドも、2種類の方法があります。

1. ViewModelのコンストラクターから自動生成される安全なメソッド
2. ViewModelのTypeやインスタンスを渡して遷移するメソッド

こちらに説明は記載しますが、サンプルアプリケーションも参考にしてください。

- [SampleBrowser](https://github.com/nuitsjp/KAMISHIBAI/tree/master/Sample/SampleBrowser)

# 自動生成メソッド

ViewModelにOpenWindowもしくOpenDialog属性を宣言することで、IPresentationServiceに専用のメソッドが生成されます。

たとえばつぎのようなViewModelが存在したとします。

```cs
[OpenWindow]
public class ChildViewModel
{
    public ChildViewModel(string windowName)
    {
        WindowName = windowName;
    }

    public string WindowName { get; }
}
```

この場合、つぎのようなコードが生成されます。

```cs
Task OpenChildWindowAsync(string windowName, object? owner = null, OpenWindowOptions? options = null);
```

使用例

```cs
await _presentationService.OpenChildWindowAsync("New Window!");
```

Open+画面名+WindowAsync（またはDialogAsync）という名称のメソッドが作成され、ViewModelのコンストラクターの引数にownerとoptionsを追加した引数が設定されます。

遷移名はViewModelのクラス名の末尾からつぎの文字列を順にすべて削除したものになります。

1. ViewModel
2. Page
3. Window
4. Dialog

たとえばViewModelがChildWindowViewModelだった場合、OpenChildWindowAsyncになります。

# 閉じる

IPresentationServiceのつぎのメソッドを利用します。

```cs
Task CloseWindowAsync(object? window = null);
```

使用例

```cs
await _presentationService.CloseWindowAsync();
```

# ViewModelへ依存性の注入

ViewModelに画面遷移のパラメーターではなく、DIコンテナーから何らかのオブジェクトを注入したい場合、コンストラクター引数にInject属性を宣言します。

たとえばロガーを注入したい場合、つぎのように記述します。

```cs
[OpenWindow]
public class ChildViewModel
{
    private readonly ILogger<FirstViewModel> _logger;

    public ChildViewModel(
        string windowName, 
        [Inject] ILogger<FirstViewModel> logger)
    {
        WindowName = windowName;
        _logger = logger;
    }
```

この場合に生成される画面遷移メソッドはつぎのとおりです。

```cs
Task OpenChildWindowAsync(string windowName, object? owner = null, OpenWindowOptions? options = null);
```

ロガーの注入がない場合と完全に同一のシグニチャーとなります。

遷移先のViewModelだけが必要とするサービスなどを注入することで、ViewModel間の依存関係を疎に保つことができます。

# OpenWindowOptions

OpenWindowOptionsまたはOpenDialogOptionsを指定することで、新しく画面を開く位置を制御できます。

その場合、親となるWindowを渡す必要があります。

たとえば子画面を開くボタンのCommandParameterに、つぎのようにWindowを渡します。

```xml
<UserControl ...
             xmlns:kamishibai="clr-namespace:Kamishibai;assembly=Kamishibai.View"
             ...
    <Button 
        Content="Open" 
        Command="{Binding OpenWindowCommand}" 
        CommandParameter="{kamishibai:Window}"/>
```

kamishibai名前空間を宣言し、CommandParameterに「kamishibai:Window」を渡します。

これでUserControlを含んでいるWindowのインスタンスをCommandParameterとして渡します。

ViewModel側ではつぎのように実装します。

```cs
public AsyncRelayCommand<object> OpenWindowCommand =>
    new(owner => _presentationService.OpenChildWindowAsync(
        "New window", 
        owner, 
        new OpenWindowOptions { WindowStartupLocation = WindowStartupLocation.CenterOwner }));
```

ViewModelのプロジェクトではWPFクラスを参照していない場合、Windowクラスを指定できたいため、CommandParameterの型にはobjectを指定します。

CommandParameterでownerを受け取り、OpenWindowOptionsで親画面の中央に子画面を開いています。

# Type指定遷移

画面遷移と同様に、OpenWindow・OpenDialogでもTypeなどを指定した操作も可能です。

```cs
Task OpenWindowAsync(Type viewModelType, object? owner = null, OpenWindowOptions? options = null);
Task OpenWindowAsync<TViewModel>(object? owner = null, OpenWindowOptions? options = null);
Task OpenWindowAsync<TViewModel>(TViewModel viewModel, object? owner = null, OpenWindowOptions? options = null) where TViewModel : notnull;
Task OpenWindowAsync<TViewModel>(Action<TViewModel> init, object? owner = null, OpenWindowOptions? options = null);

Task<bool> OpenDialogAsync(Type viewModelType, object? owner = null, OpenDialogOptions? options = null);
Task<bool> OpenDialogAsync<TViewModel>(object? owner = null, OpenDialogOptions? options = null);
Task<bool> OpenDialogAsync<TViewModel>(TViewModel viewModel, object? owner = null, OpenDialogOptions? options = null) where TViewModel : notnull;
Task<bool> OpenDialogAsync<TViewModel>(Action<TViewModel> init, object? owner = null, OpenDialogOptions? options = null);
```
