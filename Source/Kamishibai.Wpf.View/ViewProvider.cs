using System.Windows;
using Kamishibai.Wpf.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace Kamishibai.Wpf.View;

public class ViewProvider : IViewProvider
{
    private readonly ServiceCollection _services = new();
    private IServiceProvider? _serviceProvider;

    private readonly IDictionary<Type, Type> _viewToViewModel = new Dictionary<Type, Type>();

    public ViewProvider(Application application)
    {
        Application = application;
        _services.AddSingleton<INavigationService, NavigationService>();
        _services.AddSingleton<IViewProvider>(this);
    }

    public Application Application { get; }

    public void AddTransient<TService>() where TService : class
    {
        _services.AddTransient<TService>();
    }

    public void AddTransient<TService, TImplementation>() 
        where TService : class 
        where TImplementation : class, TService
    {
        _services.AddTransient<TService, TImplementation>();
    }

    public void AddTransient<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
    {
        _services.AddTransient(implementationFactory);
    }

    public void AddPresentation<TView, TViewModel>() 
        where TView : FrameworkElement
        where TViewModel : class
    {
        AddTransient<TView>();
        AddTransient<TViewModel>();
        _viewToViewModel[typeof(TViewModel)] = typeof(TView);
    }

    public bool IsAlreadyBuild => _serviceProvider is not null;

    public INavigationService BuildNavigationService()
    {
        _serviceProvider = _services.BuildServiceProvider();
        return _serviceProvider!.GetRequiredService<INavigationService>();
    }

    public TViewModel Resolve<TViewModel>() where TViewModel : class
    {
        return _serviceProvider!.GetRequiredService<TViewModel>();
    }

    public FrameworkElement ResolvePresentation<TViewModel>() where TViewModel: class
    {
        var frameworkElement = (FrameworkElement)_serviceProvider!.GetRequiredService(_viewToViewModel[typeof(TViewModel)]);
        frameworkElement.DataContext ??= Resolve<TViewModel>();

        return frameworkElement;
    }

    public Window ResolveWindow<TViewModel>() where TViewModel : class, IWindowViewModel
    {
        return (Window)ResolvePresentation<TViewModel>();
    }
}