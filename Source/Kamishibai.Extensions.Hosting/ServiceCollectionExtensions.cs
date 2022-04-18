using System.Windows;

namespace Kamishibai;

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