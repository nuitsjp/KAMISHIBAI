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

    public async Task InitializeAsync(Application application, Window window)
    {
        if (window.DataContext is INavigatedAsyncAware navigationAware)
        {
            await navigationAware.OnNavigatedAsync();
        }
    }

    public INavigationFrame Frame => GetFrame(INavigationFrame.DefaultFrameName);
    public INavigationFrame GetFrame(string frameName)
    {
        var frame = NavigationFrame.GetNavigationFrame(frameName);
        frame.ViewProvider = _viewProvider;
        return frame;
    }
}