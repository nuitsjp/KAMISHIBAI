using System.Windows;
using Kamishibai.Wpf.View;
using Microsoft.Extensions.DependencyInjection;

namespace Kamishibai.Wpf.Extensions.Hosting
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication<TApplication>(this IServiceCollection services) where TApplication : Application
        {
            services.AddSingleton<TApplication>();
            return services;
        }

        public static IServiceCollection AddShellWindow<TWindow>(this IServiceCollection services) where TWindow : Window, IShell
        {
            services.AddTransient<TWindow>();
            return services;
        }

    }
}