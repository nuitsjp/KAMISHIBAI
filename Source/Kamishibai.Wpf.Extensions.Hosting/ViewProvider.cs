using System;
using System.Windows;
using Kamishibai.Wpf.View;
using Kamishibai.Wpf.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace Kamishibai.Wpf.Extensions.Hosting;

public class ViewProvider<TApplication> : IViewProvider
    where TApplication : Application
{
    private readonly IServiceProvider _serviceProvider;

    public ViewProvider(IServiceProvider serviceProvider, TApplication application)
    {
        _serviceProvider = serviceProvider;
        Application = application;
    }

    public Application Application { get; }

    private TViewModel Resolve<TViewModel>() where TViewModel : class
    {
        return _serviceProvider!.GetRequiredService<TViewModel>();
    }

    public FrameworkElement ResolvePresentation<TViewModel>() where TViewModel: class
    {
        Type? viewType = ViewTypeCatch<TViewModel>.ViewType;
        if (viewType is null)
        {
            throw new InvalidOperationException($"View matching the {typeof(TViewModel)} has not been registered.");
        }

        var frameworkElement = (FrameworkElement)_serviceProvider!.GetRequiredService(viewType);
        frameworkElement.DataContext ??= Resolve<TViewModel>();

        return frameworkElement;
    }

    public Window ResolveWindow<TViewModel>() where TViewModel : class, IWindowViewModel
    {
        return (Window)ResolvePresentation<TViewModel>();
    }
}