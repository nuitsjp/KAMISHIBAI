# Getting Started

First, create a WPF project named "GettingStarted".

KAMISHIBAI supports the following platforms

- .NET 6.0+
- .NET Framework 4.6.2+

Next, install the package from NuGet.

NuGet :[Kamishibai.Hosting](https://www.nuget.org/packages/Kamishibai.Hosting)

```powershell
Install-Package Kamishibai.Hosting
```

In addition, install "Microsoft.Toolkit.Mvvm" to use AsyncRelayCommand, an asynchronous implementation of ICommand.

```powershell
Install-Package Microsoft.Toolkit.Mvvm
```

It does not necessarily have to be "Microsoft.Toolkit.Mvvm", but in this case we will use it to avoid the ICommand implementation.

Stop the generation of the Main method of the application. Open the .csproj file and set EnableDefaultApplicationDefinition to false.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net462</TargetFramework>
    <Nullable>enable</Nullable>
    <LangVersion>10</LangVersion>
    <UseWPF>true</UseWPF>
    <EnableDefaultApplicationDefinition>false</EnableDefaultApplicationDefinition>
  </PropertyGroup>
```

Remove the StartupUri description from App.xaml.

```xml
<Application x:Class="GettingStarted.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Application.Resources>
         
    </Application.Resources>
</Application>
```

Add a constructor to App.xaml.cs.

```csharp
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }
    }
```

Create a Program.cs file and implement the process of launching the WPF application on the Generic Host.

```csharp
using GettingStarted;
using Kamishibai;
using Microsoft.Extensions.Hosting;

// Create a builder by specifying the application and main window.
var builder = KamishibaiApplication<App, MainWindow>.CreateBuilder();

// Build and run the application.
var app = builder.Build();
app.RunAsync();
```

Now run the application and confirm that the MainWindow is displayed.

Once confirmed, create the following class.

|Name|Type|
|--|--|
|FirstPage|UserControl|
|MainViewModel|Class|
|FirstViewModel|Class|

Let's implement navigation to FirstPage in MainWindow.

First, we make navigation possible by declaring the Navigate attribute on FirstViewModel, which is the ViewModel of FirstPage.

```cs
using Kamishibai;

namespace GettingStarted;

[Navigate]
public class FirstViewModel
{
    
}
```

Since it is not possible to check whether the FirstPage has been navigated or not with the FirstPage as created, modify FirstPage.xaml to display the text.

```xml
<UserControl x:Class="GettingStarted.FirstPage"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             mc:Ignorable="d" 
             d:DesignHeight="450" d:DesignWidth="800">
    <Grid HorizontalAlignment="Center" VerticalAlignment="Center">
        <TextBlock Text="Hello, KAMISHIBAI!" FontSize="30"/>
    </Grid>
</UserControl>
```

Next, create an ICommand in the MainViewModel that navigates to the FirstPage.

```cs
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace GettingStarted;

public class MainViewModel
{
    private readonly IPresentationService _presentationService;

    public MainViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public ICommand NavigateCommand => new AsyncRelayCommand(Navigate);

    private Task Navigate() => _presentationService.NavigateToFirstAsync();
}
```

To use IPresentationService for navigation, inject it in the constructor.

Open MainWindow, define a Button for navigation and a NavigationFrame for displaying the FirstPage, and bind NavigationCommand to the Button.

```xml
<Window x:Class="GettingStarted.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:kamishibai="clr-namespace:Kamishibai;assembly=Kamishibai.View"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>

        <Button Content="Navigate" Command="{Binding NavigateCommand}"/>
        <kamishibai:NavigationFrame Grid.Row="1"/>
    </Grid>
</Window>
```

Open Program.cs and pair View and ViewModel.

```cs
var builder = KamishibaiApplication<App, MainWindow>.CreateBuilder();

builder.Services.AddPresentation<MainWindow, MainViewModel>();
builder.Services.AddPresentation<FirstPage, FirstViewModel>();
```

Let's run the program. After launching, press the Navigate button to navigate and display the following

![](/images/books/kamishibai/hello-kamishibai.png)

Finally, let's implement parameter passing.

We want the text displayed in FirstPage to be what is passed from MainViewModel.

First, add a Message property to FirstViewModel and receive the initial value in the constructor.

```cs
using Kamishibai;

namespace GettingStarted;

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

Next, bind the Message property in FirstPage.

```xml
<TextBlock Text="{Binding Message}" FontSize="30"/>
```

Then, pass messages from MainViewModel to navigate.

```cs
private Task Navigate() => _presentationService.NavigateToFirstAsync("Hello, Navigation Parameter!");
```

A string message should be added to the Navigate method. This is because the Navigate attribute constructor is parsed to generate code.

Now run the code to confirm that it does indeed navigate.

![](/images/books/kamishibai/hello-navigation-parameter.png)

What do you think?

I hope you understand that you can realize the MVVM Pattern easily and safely.