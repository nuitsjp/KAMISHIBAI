//using System;
//using System.Windows;
//using Kamishibai.Wpf.View;
//using Kamishibai.Wpf.ViewModel;
//using Microsoft.Extensions.DependencyInjection;
//using IServiceProvider = Kamishibai.Wpf.View.IServiceProvider;

//namespace Kamishibai.Wpf.Extensions.Hosting;

//public class ServiceProvider<TApplication> : IServiceProvider
//    where TApplication : Application
//{
//    private readonly System.IServiceProvider _serviceProvider;

//    public ServiceProvider(System.IServiceProvider serviceProvider, TApplication application)
//    {
//        _serviceProvider = serviceProvider;
//        Application = application;
//    }

//    public Application Application { get; }

//    public TService GetRequiredService<TService>() where TService : notnull
//    {
//        return _serviceProvider!.GetRequiredService<TService>();
//    }

//    public FrameworkElement GetPresentation<TViewModel>() where TViewModel: class
//    {
//        ViewType? viewType = ViewTypeCache<TViewModel>.ViewType;
//        if (viewType is null)
//        {
//            throw new InvalidOperationException($"View matching the {typeof(TViewModel)} has not been registered.");
//        }

//        var frameworkElement = (FrameworkElement)_serviceProvider!.GetRequiredService(viewType.Type);
//        if (viewType.AssignViewModel)
//        {
//            frameworkElement.DataContext ??= GetRequiredService<TViewModel>();
//        }

//        return frameworkElement;
//    }
//}