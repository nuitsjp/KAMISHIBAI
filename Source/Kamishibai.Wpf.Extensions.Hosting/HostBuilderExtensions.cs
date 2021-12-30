using System.Windows;
using Kamishibai.Wpf.View;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kamishibai.Wpf.Extensions.Hosting;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseWpf<TApplication, TShell>(this IHostBuilder hostBuilder) 
        where TApplication : Application
        where TShell : Window, IShell
    {
        Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
        Thread.CurrentThread.SetApartmentState(ApartmentState.STA);


        return hostBuilder.ConfigureServices((context, services) =>
        {
            services.AddHostedService<WpfHostedService>();
            services.AddSingleton<Application, TApplication>();
            services.AddSingleton<IShell, TShell>();
        });
    }
}