using System.Windows;
using Kamishibai.Wpf.View;
using Kamishibai.Wpf.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Extensions.Hosting;

namespace Kamishibai.Wpf.Extensions.Hosting;

public class KamishibaiApplication<TApplication, TWindow>
    where TApplication : Application
    where TWindow : Window
{
    public static IWpfApplicationBuilder<TApplication, TWindow> CreateBuilder()
    {
        WpfApplicationOptions<TApplication, TWindow> options = new()
        {
            OnLoaded = (application, window, serviceProvider) =>
            {
                var navigationService = (NavigationService)serviceProvider.GetRequiredService<INavigationService>();
                navigationService.InitializeAsync(application, window).Wait();
            }
        };
        return new KamishibaiApplicationBuilder<TApplication, TWindow>(WpfApplication<TApplication, TWindow>
            .CreateBuilder(options));
    }
}