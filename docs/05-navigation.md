# Navigation Details

There are two types of KAMISHIBAI navigation methods.

1. a safe method that is automatically generated from the ViewModel constructor
2. a method that navigates by passing a ViewModel Type or instance.

Please also refer to the sample application.

- [KamishibaiApp](https://github.com/nuitsjp/KAMISHIBAI/tree/master/Sample/KamishibaiApp)
- [SampleBrowser](https://github.com/nuitsjp/KAMISHIBAI/tree/master/Sample/SampleBrowser)

# auto-generated method

By declaring the Navigate attribute on the ViewModel, a dedicated navigation method is created for the IPresentationService.

For example, suppose the following ViewModel exists

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

In this case, the following code is generated

```cs
Task<bool> NavigateToFirstAsync(string message, string frameName = "");
```

Usage examples

```cs
await _presentationService.NavigateToFirstAsync("Hello, Navigation Parameter!");
```

The method "NavigateTo+NavigationName+Async" is created, and the arguments are set to the ViewModel constructor and frameName.

NavigationName is the ViewModel class name with the following strings removed in order.

1. ViewModel
2. Page
3. Window
4. Dialog

For example, if the ViewModel was FirstPageViewModel, it would be NavigateToFirstAsync.

# Go Back

Use the following methods of IPresentationService.

```cs
Task<bool> GoBackAsync(string frameName = "");
```

Usage examples

```cs
await _presentationService.GoBackAsync();
```

# Injecting Dependencies into ViewModel

If you want to inject some object from a DI container into the ViewModel instead of navigation parameters, declare the Inject attribute as a constructor argument.

For example, if you want to inject a logger, you would write the following

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

The navigation method generated in this case is as follows

```cs
Task<bool> NavigateToFirstAsync(string message, string frameName = "");
```

The signature will be the same as if there were no logger injection.

# Pass frameName to constructor

You may want to pass the frame name used for navigation to the ViewModel.

```cs
[Navigate]
public class FirstViewModel
{
    public FirstViewModel(string frameName, string message)
    {
        ...
```

In this case, the following code is generated

```cs
Task<bool> NavigateToFirstAsync(string frameName, string message);
```

Arguments that are normally added as optional are removed and follow the constructor's signature.

# multiple constructor

KAMISHIBAI allows multiple constructors to be declared in the ViewModel.

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

In this case, a navigation method is generated for each constructor.

```cs
Task<bool> NavigateToFirstAsync(string frameName = "");
Task<bool> NavigateToFirstAsync(string message, string frameName = "");
```

# Type-specific navigation

For example, if you have the following side menu

![](/Images/side-menu.png)

It is often the case that a menu is created as a list and you want to dynamically control the side menu.

In this case, navigation does not require any parameters, and it is sometimes simpler to specify the ViewModel Type than to use a code-generated method.

For this reason, KAMISHIBAI provides the following navigation methods.


```cs
Task<bool> NavigateAsync(Type viewModelType, string frameName = "");
Task<bool> NavigateAsync<TViewModel>(string frameName = "");
Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init, string frameName = "");
```

It is possible to navigate by specifying the Type as an argument.

It is also possible, although slightly different, to pass a ViewModel instance by specifying it.

```cs
Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel, string frameName = "") where TViewModel : notnull;
```

This can be used when you want to keep the status on a sub-view, for example.

[<< Configuration of Generic Host](04-hosting.md) | [Menu](01-table-of-contents.md) | [OpenWindow and OpenDialog >>](06-open-window-and-dialog.md)