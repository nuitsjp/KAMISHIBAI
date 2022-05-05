---
title: "Generic Hostの構成"
---

KAMISHIBAIはGeneric Host上でWPFアプリケーションを動作させます。

Generic Hostは .NETにおけるもっとも大切なコアアーキテクチャのひとつであり、多くのモダンなライブラリーがGeneric Hostを前提に提供されています。

Generic Hostをサポートすることで、KAMISHIBAIでは最新のライブラリーをサポートします。

もっとも単純なアプリケーションをホストするコードはつぎのようになります。

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
app.RunAsync();
```

ASP.NET Core 6.0と同様に記載できます。

たとえば環境別の設定ファイルをロードしたい場合はつぎのように設定できます。

```cs
var builder = KamishibaiApplication<App, MainWindow>.CreateBuilder();

builder.Configuration
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, false);
```

詳細な利用方法は、各ライブラリーのドキュメントを参照してください。