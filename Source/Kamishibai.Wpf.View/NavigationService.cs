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

    //public Task NavigateAsync<TViewModel>() where TViewModel : class
    //    => NavigateAsync<TViewModel>(INavigationFrame.DefaultFrameName);

    //public Task NavigateAsync<TViewModel, T1>(T1 param1) where TViewModel : class, INavigatedAsyncAware<T1>
    //    => NavigateAsync<TViewModel, T1>(INavigationFrame.DefaultFrameName, param1);

    //public Task NavigateAsync<TViewModel, T1, T2>(T1 param1, T2 param2) where TViewModel : class, INavigatedAsyncAware<T1, T2>
    //    => NavigateAsync<TViewModel, T1, T2>(INavigationFrame.DefaultFrameName, param1, param2);

    //public Task NavigateAsync<TViewModel>(TViewModel viewModel) where TViewModel : class
    //{
    //    throw new NotImplementedException();
    //}

    //public Task NavigateAsync<TViewModel>(Action<TViewModel> init) where TViewModel : class
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task NavigateAsync<TViewModel>(string frameName) where TViewModel : class
    //{
    //    var navigationFrame = NavigationFrame.GetNavigationFrame(frameName);
    //    var view = _viewProvider.ResolvePresentation<TViewModel>();

    //    navigationFrame.Navigate(view);

    //    if (view.DataContext is INavigatedAsyncAware navigationAware)
    //    {
    //        await navigationAware.OnNavigatedAsync();
    //    }
    //}

    //public async Task NavigateAsync<TViewModel, T1>(string frameName, T1 param1) where TViewModel : class, INavigatedAsyncAware<T1>
    //{
    //    var navigationFrame = NavigationFrame.GetNavigationFrame(frameName);
    //    var view = _viewProvider.ResolvePresentation<TViewModel>();

    //    navigationFrame.Navigate(view);
    //    await((INavigatedAsyncAware<T1>)view.DataContext).OnNavigatedAsync(param1);
    //}

    //public async Task NavigateAsync<TViewModel, T1, T2>(string frameName, T1 param1, T2 param2) where TViewModel : class, INavigatedAsyncAware<T1, T2>
    //{
    //    var navigationFrame = NavigationFrame.GetNavigationFrame(frameName);
    //    var view = _viewProvider.ResolvePresentation<TViewModel>();

    //    navigationFrame.Navigate(view);
    //    await((INavigatedAsyncAware<T1, T2>)view.DataContext).OnNavigatedAsync(param1, param2);
    //}

    //public void GoBack() => GoBack(INavigationService.DefaultFrameName);

    //public void GoBack(string frameName)
    //{
    //    var navigationFrame = NavigationFrame.GetNavigationFrame(frameName);
    //    navigationFrame.GoBack();
    //}

    public INavigationFrame Frame => GetFrame(INavigationFrame.DefaultFrameName);
    public INavigationFrame GetFrame(string frameName)
    {
        var frame = NavigationFrame.GetNavigationFrame(frameName);
        frame.ViewProvider = _viewProvider;
        return frame;
    }
}