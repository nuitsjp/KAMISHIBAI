---
title: "メッセージダイアログ"
---

KAMISHIBAIを利用することでViewModelから簡単にアラートダイアログや確認ダイアログを表示できます。

```cs
MessageBoxResult ShowMessage(
    string messageBoxText,
    string caption = "",
    MessageBoxButton button = MessageBoxButton.OK,
    MessageBoxImage icon = MessageBoxImage.None, 
    MessageBoxResult defaultResult = MessageBoxResult.None,
    MessageBoxOptions options = MessageBoxOptions.None,
    object? owner = null);
```

必須引数は1つ目のmessageBoxTextだけです。

```cs
_presentationService.ShowMessage("Hello, Message!");
```

# ダイアログ選択結果の取得

ダイアログで選択された結果を取得できます。

```cs
MessageBoxResult result = 
    _presentationService.ShowMessage(
        "Hello, Message!", 
        button: MessageBoxButton.OKCancel);
if (result == MessageBoxResult.OK)
{
    ...
}
```

# 親ウィンドウの中央に表示

CommandParameterでownerを渡すことで簡単に、Windowの中央にダイアログを表示できます。

```xml
<UserControl ...
             xmlns:kamishibai="clr-namespace:Kamishibai;assembly=Kamishibai.View"
             ...
    <Button 
        Content="Show Message" 
        Command="{Binding ShowMessageCommand}" 
        CommandParameter="{kamishibai:Window}"/>
```

名前空間「kamishibai」を追加し、CommandParameterに「kamishibai:Window」を渡します。

これをCommandのパラメーターとして受け取り、ShowMessageメソッドに渡します。

```cs
public RelayCommand<object> ShowMessageCommand => new(owner
=> _presentationService.ShowMessage("Hello, Message!", owner: owner);
```

