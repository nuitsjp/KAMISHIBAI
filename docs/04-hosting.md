
# Generic Host Configuration

KAMISHIBAI runs WPF applications on Generic Host.

Generic Host is the most important architecture in .NET, and many modern libraries are provided on the premise of Generic Host.

By supporting Generic Host, KAMISHIBAI supports modern libraries.

The code to host the simplest application is as follows

```cs
using GettingStarted;
using Kamishibai;
using Microsoft.Extensions.Hosting;

// Create a builder by specifying the application and main window.
var builder = KamishibaiApplication<App, MainWindow>.CreateBuilder();

// Register presentation(View and ViewModel).
builder.Services.AddPresentation<MainWindow, MainViewModel>();
builder.Services.AddPresentation<FirstPage, FirstViewModel>();

// Build and run the application.
var app = builder.Build();
await app.RunAsync();
```

It can be described in the same way as ASP.NET Core 6.0.

For example, if you want to load environment-specific configuration files, you can do so as follows

```cs
var builder = KamishibaiApplication<App, MainWindow>.CreateBuilder();

builder.Configuration
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, false);
```

For detailed usage, please refer to the documentation of each library.

[<< NuGet Package Structure and Overview](03-overview.md) | [Menu](01-table-of-contents.md) | [Navigation Details >>](05-navigation.md)