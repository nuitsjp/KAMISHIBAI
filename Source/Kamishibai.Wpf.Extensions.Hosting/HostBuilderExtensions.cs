using System.Windows;
using Kamishibai.Wpf.View;
using Kamishibai.Wpf.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kamishibai.Wpf.Extensions.Hosting;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseWpf<TApplication, TShell>(this IHostBuilder hostBuilder) 
        where TApplication : Application
        where TShell : Window, IShell
    {
        return hostBuilder.UseWpf<TApplication, TShell>((_, _) => { });
    }

    public static IHostBuilder UseWpf<TApplication, TShell>(this IHostBuilder hostBuilder, Action<TApplication, TShell> configureApplication)
        where TApplication : Application
        where TShell : Window, IShell
    {
        Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
        Thread.CurrentThread.SetApartmentState(ApartmentState.STA);


        return hostBuilder.ConfigureServices((context, services) =>
        {
            services.AddHostedService<WpfHostedService<TApplication, TShell>>();
            services.AddTransient<TApplication>();
            services.AddTransient<TShell>();
            services.AddTransient<IApplicationConfigurator<TApplication, TShell>>(_ => new ApplicationConfigurator<TApplication, TShell>(configureApplication));
        });
    }

    public static IHostBuilder UseKamishibai<TApplication, TShell>(this IHostBuilder hostBuilder)
        where TApplication : Application
        where TShell : Window, IShell
    {
        return hostBuilder.UseWpf<TApplication, TShell>((application, shell) =>
        {
            if (shell.DataContext is INavigationAware navigationAware)
            {
                shell.Loaded += (_, _) => { navigationAware.OnEntryAsync(); };
            }


        });
    }
}