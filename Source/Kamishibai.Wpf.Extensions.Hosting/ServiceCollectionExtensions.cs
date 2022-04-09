using System.Windows;
using Kamishibai.Wpf.View;
using Microsoft.Extensions.DependencyInjection;

namespace Kamishibai.Wpf.Extensions.Hosting;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation<TView, TViewModel>(this IServiceCollection services, bool assignViewModel = false)
        where TView : FrameworkElement
        where TViewModel : class
    {
        ViewTypeCache<TViewModel>.ViewType = new ViewType(typeof(TView), assignViewModel);
        services.AddTransient<TView>();
        services.AddTransient<TViewModel>();
        return services;
    }
}