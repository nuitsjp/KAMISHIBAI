[for Japanese](https://zenn.dev/nuits_jp/books/introduction-to-kamishibai)

[for Xamarin](Xamarin/README.md)

[![KAMISHIBAI](https://raw.githubusercontent.com/nuitsjp/KAMISHIBAI/master/Images/KAMISHIBAI.png)](https://github.com/nuitsjp/KAMISHIBAI/blob/master/README.md)

# KAMISHIBAI

KAMISHIBAI is a navigation library for WPF that supports MVVM pattern on Generic Host.

By declaring arguments in the ViewModel constructor, a dedicated navigation method is automatically generated.

To pass a string during a navigation, define a ViewModel as follows

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

A dedicated navigation method is then automatically generated and can be called as follows

```cs
await _presentationService.NavigateToFirstAsync("Hello, KAMISHIBAI!");
```

And you can use DI together.

```cs
public FirstViewModel(
    string message, 
    [Inject] ILogger<FirstViewModel> logger)
```

Declare an InjectAttribute for the argument you wish to inject a dependency on.

The logger is injected from the DI container. Exactly the same as in the navigation example above, only the message can be passed to the navigation.


KAMISHIBAI guarantees type safety during navigation, allowing for safe implementations that take full advantage of nullable.

KAMISHIBAI does not restrict any of the WPF functions and provides the following

- Generic Host support
- ViewModel-driven navigation using the MVVM pattern
- Type-safe navigation parameters
- Consistent event notifications for navigation
- Support for maximizing the use of nullable

Generic Host support enables WPF to take advantage of most of the latest .NET technologies.

KAMISHIBAI provides the following features

- Navigation
- Display of new screens and user dialogs
- Display of message dialogs, file selection and save dialogs

With KAMISHIBAI, the MVVM pattern can be most easily realized in WPF.

And it can be used with any existing MVVM framework or library. There is one restriction, however.

Please use KAMISHIBAI for navigation.

# Table of Contents

- [Getting Started](/docs/02-getting-started.md)
- [NuGet Package Structure and Overview](/docs/03-overview.md)
- [Configuration of Generic Host](/docs/04-hosting.md)
- [Navigation Details](/docs/05-navigation.md)
- [OpenWindow and OpenDialog](/docs/06-open-window-and-dialog.md)
- [Navigation Event Details](/docs/07-navigation-event.md)
- [Message Dialog](/docs/08-message-dialog.md)
- [Open File Dialog](/docs/09-open-file-dialog.md)
- [Save File Dialog](/docs/10-save-file-dialog.md)