using System.Windows;
using Kamishibai.Wpf.View;
using Kamishibai.Wpf.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nuits.Extensions.Hosting.Wpf;

namespace Kamishibai.Wpf.Extensions.Hosting;

public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureKamishibai<TApplication>(this IHostBuilder hostBuilder)
        where TApplication : Application
    {
        return hostBuilder.ConfigureKamishibai<TApplication>((_, _, _) => { });
    }

    public static IHostBuilder ConfigureKamishibai<TApplication>(this IHostBuilder hostBuilder, Action<TApplication, Window, IServiceProvider> onLoaded)
        where TApplication : Application
    {
        Initialize<TApplication>(hostBuilder);
        return hostBuilder.ConfigureWpf(onLoaded);
    }

    public static IHostBuilder ConfigureKamishibai<TApplication, TWindow>(this IHostBuilder hostBuilder)
        where TApplication : Application
        where TWindow : Window
    {
        return hostBuilder.ConfigureKamishibai<TApplication, TWindow>((_, _, _) => { });
    }

    public static IHostBuilder ConfigureKamishibai<TApplication, TWindow>(this IHostBuilder hostBuilder, Action<TApplication, TWindow, IServiceProvider> onLoaded)
        where TApplication : Application
        where TWindow : Window
    {
        Initialize<TApplication>(hostBuilder);
        return hostBuilder.ConfigureWpf<TApplication, TWindow>((application, window, serviceProvider) =>
        {
            var navigationService = (NavigationService) serviceProvider.GetRequiredService<INavigationService>();
            navigationService.InitializeAsync(application, window).Wait();
            onLoaded(application, window, serviceProvider);
        });
    }

    private static void Initialize<TApplication>(IHostBuilder hostBuilder)
        where TApplication : Application
    {
        hostBuilder.ConfigureServices((_, services) =>
        {
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IViewProvider, ViewProvider<TApplication>>();
        });
    }
}