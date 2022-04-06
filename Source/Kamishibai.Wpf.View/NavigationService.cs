using System.Windows;
using Kamishibai.Wpf.ViewModel;

namespace Kamishibai.Wpf.View;

public class NavigationService : INavigationService
{
    private readonly IViewProvider _viewProvider;

    public NavigationService(IViewProvider viewProvider)
    {
        _viewProvider = viewProvider;
    }

    public async Task InitializingAsync(Application application, Window window)
    {
        if (window.DataContext is INavigatingAsyncAware navigatingAsyncAware) await navigatingAsyncAware.OnNavigatingAsync();
        if (window.DataContext is INavigatingAware navigatingAware) navigatingAware.OnNavigating();
    }

    public async Task InitializeAsync(Application application, Window window)
    {
        if (window.DataContext is INavigatedAsyncAware navigationAware) await navigationAware.OnNavigatedAsync();
        if (window.DataContext is INavigatedAware navigatedAware) navigatedAware.OnNavigated();
    }

    public INavigationFrame Frame => GetFrame(INavigationFrame.DefaultFrameName);
    public INavigationFrame GetFrame(string frameName)
    {
        var frame = NavigationFrame.GetNavigationFrame(frameName);
        frame.ViewProvider = _viewProvider;
        return frame;
    }
}