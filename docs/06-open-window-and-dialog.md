# OpenWindow and OpenDialog

OpenWindow and OpenDialog are very similar, but OpenWindow opens a modeless window, while OpenDialog opens a modal window.

Here we will discuss OpenWindow, but if you use OpenDialog, please read it differently.

There are also two methods for opening a new window. 1.

1. a safe method that is automatically generated from the ViewModel constructor
2. a method that navigates by passing a ViewModel Type or instance.

Please also refer to the sample application.

- [SampleBrowser](https://github.com/nuitsjp/KAMISHIBAI/tree/master/Sample/SampleBrowser)

# auto-generated method

By declaring an OpenWindow or OpenDialog attribute on the ViewModel, a dedicated method is created in the IPresentationService.

For example, suppose the following ViewModel exists

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

In this case, the following code is generated

```cs
Task OpenChildWindowAsync(string windowName, object? owner = null, OpenWindowOptions? options = null);
```

Usage examples

```cs
await _presentationService.OpenChildWindowAsync("New Window!");
```

A method named "Open+ScreenName+WindowAsync (or DialogAsync)" is created, and the arguments are defined by adding owner and options to the arguments of the ViewModel constructor.

ScreenName is the ViewModel class name with the following strings removed in order.

1. ViewModel
2. Page
3. Window
4. Dialog

For example, if the ViewModel was ChildWindowViewModel, it would be OpenChildWindowAsync.

# Close Window

Use the following methods of IPresentationService.

```cs
Task CloseWindowAsync(object? window = null);
```

Usage examples

```cs
await _presentationService.CloseWindowAsync();
```

# Injecting Dependencies into ViewModel

If you want to inject some object from a DI container into the ViewModel instead of navigation parameters, declare the Inject attribute as a constructor argument.

For example, if you want to inject a logger, you would write the following

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

The methods generated in this case are as follows

```cs
Task OpenChildWindowAsync(string windowName, object? owner = null, OpenWindowOptions? options = null);
```

The signature will be the same as if the logger were not injected.

# OpenWindowOptions

By specifying OpenWindowOptions or OpenDialogOptions, you can control the position at which a new screen is opened.

In this case, the parent Window must be passed.

For example, the following Window is passed to the CommandParameter of the button that opens a child screen

```xml
<UserControl ...
             xmlns:kamishibai="clr-namespace:Kamishibai;assembly=Kamishibai.View"
             ...
    <Button 
        Content="Open" 
        Command="{Binding OpenWindowCommand}" 
        CommandParameter="{kamishibai:Window}"/>
```

Declare the kamishibai namespace and pass "kamishibai:Window" as CommandParameter.

Now an instance of Window containing UserControl is passed as CommandParameter.

On the ViewModel side, implement as follows.

```cs
public AsyncRelayCommand<object> OpenWindowCommand =>
    new(owner => _presentationService.OpenChildWindowAsync(
        "New window", 
        owner, 
        new OpenWindowOptions { WindowStartupLocation = WindowStartupLocation.CenterOwner }));
```

If the ViewModel project does not refer to a WPF class, we want to be able to specify a Window class, so we specify an object for the CommandParameter type.

It receives owner in CommandParameter and opens a child screen in the center of the parent screen with OpenWindowOptions.

# Type specification

As with navigation, OpenWindow and OpenDialog can also be operated by specifying the Type, etc.

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

[<< Navigation Details](05-navigation.md) | [Navigation Event Details >>](07-navigation-event.md)