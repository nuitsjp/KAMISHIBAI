# Message Dialog

KAMISHIBAI makes it easy to display alert and confirmation dialogs from the ViewModel.

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

The only required argument is the first messageBoxText.

```cs
_presentationService.ShowMessage("Hello, Message!");
```

# Dialog Selection Result

You can get the result selected in the dialog.

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

# Displayed in the center of the parent window

By passing owner in CommandParameter, you can easily display the dialog in the center of the Window.

```xml
<UserControl ...
             xmlns:kamishibai="clr-namespace:Kamishibai;assembly=Kamishibai.View"
             ...
    <Button 
        Content="Show Message" 
        Command="{Binding ShowMessageCommand}" 
        CommandParameter="{kamishibai:Window}"/>
```

Add the namespace "kamishibai" and pass "kamishibai:Window" to CommandParameter.

This is received as a Command parameter and passed to the ShowMessage method.

```cs
public RelayCommand<object> ShowMessageCommand => new(owner
=> _presentationService.ShowMessage("Hello, Message!", owner: owner);
```

[<< Navigation Event Details](07-navigation-event.md) | [Open File Dialog >>](09-open-file-dialog.md)