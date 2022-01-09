using System.Windows;
using Kamishibai.Wpf.View;
using Kamishibai.Wpf.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wpf.Extensions.Hosting;

namespace Kamishibai.Wpf.Extensions.Hosting;

public class KamishibaiApplicationBuilder<TApplication, TWindow> : IWpfApplicationBuilder<TApplication, TWindow>
    where TApplication : Application 
    where TWindow : Window
{
    private readonly IWpfApplicationBuilder<TApplication, TWindow> _builder;

    public KamishibaiApplicationBuilder(IWpfApplicationBuilder<TApplication, TWindow> builder)
    {
        _builder = builder;
    }

    public IHostEnvironment Environment => _builder.Environment;
    public IServiceCollection Services => _builder.Services;
    public ConfigurationManager Configuration => _builder.Configuration;
    public ILoggingBuilder Logging => _builder.Logging;
    public ConfigureHostBuilder Host => _builder.Host;
    public WpfApplication<TApplication, TWindow> Build()
    {
        Services.AddSingleton<INavigationService, NavigationService>();
        Services.AddSingleton<IViewProvider, ViewProvider<TApplication>>();

        return _builder.Build();
    }
}