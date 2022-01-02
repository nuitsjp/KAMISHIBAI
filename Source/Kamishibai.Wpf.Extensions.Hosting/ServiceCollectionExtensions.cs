using System.Windows;
using Kamishibai.Wpf.View;
using Microsoft.Extensions.DependencyInjection;

namespace Kamishibai.Wpf.Extensions.Hosting;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation<TView, TViewModel>(this IServiceCollection services)
        where TView : FrameworkElement
        where TViewModel : class
    {
        ViewTypeCatch<TViewModel>.ViewType = typeof(TView);
        services.AddTransient<TView>();
        services.AddTransient<TViewModel>();
        return services;
    }
}