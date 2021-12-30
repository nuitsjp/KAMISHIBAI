using Kamishibai.Wpf.Demo.App;
using Kamishibai.Wpf.Demo.View;
using Kamishibai.Wpf.Demo.ViewModel;
using Kamishibai.Wpf.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

new HostBuilder()
    .ConfigureServices(ConfigureServices)
    .UseWpf<App, MainWindow>()
    .Build()
    .RunAsync();

void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    services.AddTransient<MainWindowViewModel>();
}