using System.Windows;

namespace Kamishibai;

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
        Services.AddSingleton<INavigationFrameProvider, NavigationFrameProvider>();
        Services.AddTransient<IWindowService, WindowService>();

        var app = _builder.Build();
        app.Startup += async (_, args) =>
        {
            if (args.Window.DataContext is INavigatingAsyncAware navigatingAsyncAware) await navigatingAsyncAware.OnNavigatingAsync();
            if (args.Window.DataContext is INavigatingAware navigatingAware) navigatingAware.OnNavigating();
        };
        app.Loaded += async (_, args) =>
        {
            if (args.Window.DataContext is INavigatedAsyncAware navigationAware) await navigationAware.OnNavigatedAsync();
            if (args.Window.DataContext is INavigatedAware navigatedAware) navigatedAware.OnNavigated();
        };
        return app;
    }
}