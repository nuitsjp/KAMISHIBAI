using System.ComponentModel;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wpf.Extensions.Hosting;

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

        // Register IPresentationService to DI Container.
        var presentationServiceBase = typeof(IPresentationServiceBase);
        var presentationServiceInterfaces = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => p != presentationServiceBase && presentationServiceBase.IsAssignableFrom(p) && p.IsInterface);
        foreach (var presentationServiceInterface in presentationServiceInterfaces)
        {
            var implementation = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Single(p => p != presentationServiceInterface && presentationServiceInterface.IsAssignableFrom(p));
            Services.AddTransient(presentationServiceInterface, implementation);
        }

        var app = _builder.Build();
        app.Startup += async (_, args) =>
        {
            WindowService.SetupCloseEvents(args.Window);

            if (args.Window.DataContext is null)
            {
                if (ViewTypeCache.TryGetViewModelType(typeof(TWindow), out var viewModelType))
                {
                    args.Window.DataContext = app.Services.GetService(viewModelType);
                }
            }

            if (args.Window.DataContext is null) return;

            PreForwardEventArgs preForwardEventArgs = new(null, null, args.Window.DataContext);
            if (args.Window.DataContext is INavigatingAsyncAware navigatingAsyncAware) await navigatingAsyncAware.OnNavigatingAsync(preForwardEventArgs);
            if (args.Window.DataContext is INavigatingAware navigatingAware) navigatingAware.OnNavigating(preForwardEventArgs);
        };
        app.Loaded += async (_, args) =>
        {
            if (args.Window.DataContext is null) return;

            PostForwardEventArgs postForwardEventArgs = new(null, null, args.Window.DataContext);
            if (args.Window.DataContext is INavigatedAsyncAware navigationAware) await navigationAware.OnNavigatedAsync(postForwardEventArgs);
            if (args.Window.DataContext is INavigatedAware navigatedAware) navigatedAware.OnNavigated(postForwardEventArgs);
        };
        return app;
    }
}