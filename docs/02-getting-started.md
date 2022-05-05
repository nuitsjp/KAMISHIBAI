---
title: "Getting Started"
---

まずはWPFプロジェクトを作成します。ここでは「GettingStarted」という名称で作成することとします。

KAMISHIBAIは以下のいずれかのプラットフォームをサポートします。

- .NET 6.0+
- .NET Framework 4.6.2+

つづいてNuGetからパッケージをインストールします。

NuGet :[Kamishibai.Hosting](https://www.nuget.org/packages/Kamishibai.Hosting)

```powershell
Install-Package Kamishibai.Hosting
```

あわせてICommandの非同期実装であるAsyncRelayCommandを利用するため、Microsoft.Toolkit.Mvvmをインストールします。

```powershell
Install-Package Microsoft.Toolkit.Mvvm
```

必ずしもMicrosoft.Toolkit.Mvvmである必要はありませんが、今回はICommandの実装を避けるために利用します。

アプリケーションのエントリポイント（Mainメソッド）の自動生成を停止します。.csprojファイルを開き、EnableDefaultApplicationDefinitionをfalseに設定します。

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

App.xamlからStartupUriの記述を削除します。

```xml
<Application x:Class="GettingStarted.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Application.Resources>
         
    </Application.Resources>
</Application>
```

App.xaml.csにコンストラクターを追加します。

```csharp
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }
    }
```

Program.csファイルを作成し、Generic Host上でWPFアプリケーションを起動する処理を実装します。

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

ここでいったんアプリケーションを実行し、MainWindowが表示されることを確認してください。

確認できたら、つぎのクラスを作成してください。

|名前|タイプ|
|--|--|
|FirstPage|UserControl|
|MainViewModel|Class|
|FirstViewModel|Class|

それではMainWindowの中で、FirstPageに画面遷移するように実装を追加していきましょう。

まずFirstPageのViewModelであるFirstViewModelに、Navigate属性を宣言することで遷移可能にします。

```cs
using Kamishibai;

namespace GettingStarted;

[Navigate]
public class FirstViewModel
{
    
}
```

作成したままのFirstPageだと、遷移したかどうか確認できないため、テキストを表示するようFirstPage.xamlを修正します。

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

つづいてMainViewModelに、FirstPageへ遷移するICommandを作成します。

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

画面遷移にIPresentationServiceを利用するため、コンストラクターで注入します。

MainWindowを開いて、画面遷移を事項するためのButtonと、FirstPageを表示する領域であるNavigationFrameを定義し、ボタンにNavigationCommandをバインドします。

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

Program.csを開き、ViewとViewModelをペアリングします。

```cs
var builder = KamishibaiApplication<App, MainWindow>.CreateBuilder();

builder.Services.AddPresentation<MainWindow, MainViewModel>();
builder.Services.AddPresentation<FirstPage, FirstViewModel>();
```

それでは実行しましょう。起動後、Navigateボタンを押すと画面遷移し、つぎのように表示されます。

![](/images/books/kamishibai/hello-kamishibai.png)

では最後にパラメーター渡しを実装しましょう。

FirstPageに表示しているテキストをMainViewModelから渡されたものを表示するようにします。

まずはFirstViewModelにMessageプロパティを追加し、コンストラクターで受け取るように実装します。

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

つづいてFirstPageでMessageプロパティをバインドします。

```xml
<TextBlock Text="{Binding Message}" FontSize="30"/>
```

そしてMainViewModelからメッセージを渡して画面遷移させます。

```cs
private Task Navigate() => _presentationService.NavigateToFirstAsync("Hello, Navigation Parameter!");
```

画面遷移メソッドにstring messageが追加されているはずです。これはNavigate属性のコンストラクターを解析してコード生成をしているためです。

では実行して確かに画面遷移することを確認してください。

![](/images/books/kamishibai/hello-navigation-parameter.png)

どうですか？
「簡単で」「安全に」「MVVMパターンを」実現できることをご理解いただけたのではないでしょうか？