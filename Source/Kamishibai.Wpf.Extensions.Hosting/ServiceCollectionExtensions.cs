using System.Windows;
using Kamishibai.Wpf.View;

namespace Kamishibai.Wpf.Extensions.Hosting;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation<TView, TViewModel>(this IServiceCollection services)
        where TView : FrameworkElement
        where TViewModel : class
    {
        ViewTypeCache.SetViewType<TView, TViewModel>();
        services.AddTransient<TView>();
        services.AddTransient<TViewModel>();
        return services;
    }
}