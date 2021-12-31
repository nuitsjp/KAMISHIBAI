using System.Windows;
using System.Windows.Threading;
using Kamishibai.Wpf.Demo.App;
using Kamishibai.Wpf.Demo.View;
using Kamishibai.Wpf.Demo.ViewModel;
using Kamishibai.Wpf.Extensions.Hosting;
using Kamishibai.Wpf.View;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


new HostBuilder()
    .ConfigureServices((context, services) =>
    {
        //services.AddShellWindow<MainWindow>();
        services.AddTransient<MainWindowViewModel>();
    })
    .UseKamishibai<App, MainWindow>()
    .Build()
    .RunAsync();
