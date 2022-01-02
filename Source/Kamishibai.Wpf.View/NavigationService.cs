using System.Windows;
using Kamishibai.Wpf.ViewModel;

namespace Kamishibai.Wpf.View;

public class NavigationService : INavigationService
{
    private static readonly string DefaultFrameName = string.Empty;
    private readonly Dictionary<string, NavigationFrame> _frames = new();
    private readonly IViewProvider _viewProvider;

    public NavigationService(IViewProvider viewProvider)
    {
        _viewProvider = viewProvider;
    }

    public async Task InitializeAsync(Application application, Window window)
    {
        var navigationFrame = window.GetChildOfType<NavigationFrame>()!;
        _frames[navigationFrame.Name] = navigationFrame;
 
        if (window.DataContext is INavigationAware navigationAware)
        {
            await navigationAware.OnEntryAsync();
        }
    }

    public async Task NavigateAsync<TViewModel>() where TViewModel : class
    {
        var navigationFrame = _frames[DefaultFrameName];
        var view = _viewProvider.ResolvePresentation<TViewModel>();

        navigationFrame.Push(view);

        if (view.DataContext is INavigationAware navigationAware)
        {
            await navigationAware.OnEntryAsync();
        }
    }

    public async Task NavigateAsync<TViewModel, T>(T obj) where TViewModel : class, INavigationAware<T>
    {
        var navigationFrame = _frames[DefaultFrameName];
        var view = _viewProvider.ResolvePresentation<TViewModel>();

        navigationFrame.Push(view);
        await ((INavigationAware<T>) view.DataContext).OnEntryAsync(obj);
    }
}