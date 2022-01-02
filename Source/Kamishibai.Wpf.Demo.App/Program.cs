using Kamishibai.Wpf.Demo.App;
using Kamishibai.Wpf.Demo.View;
using Kamishibai.Wpf.Demo.ViewModel;
using Kamishibai.Wpf.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


new HostBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<MainWindowViewModel>();
        services.AddPresentation<ContentPage, ContentPageViewModel>();
    })
    .ConfigureKamishibai<App, MainWindow>()
    .Build()
    .RunAsync();
