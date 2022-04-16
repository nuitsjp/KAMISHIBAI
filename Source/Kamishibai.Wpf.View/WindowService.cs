using System.Runtime.InteropServices;
using System.Windows;

namespace Kamishibai.Wpf.View;

public class WindowService : IWindowService
{
    private readonly IServiceProvider _serviceProvider;

    public WindowService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task OpenWindowAsync(Type viewModelType, OpenWindowOptions options)
    {
        var viewModel = _serviceProvider.GetService(viewModelType)!;
        return OpenWindowAsync(viewModelType, viewModel, options);
    }

    public Task OpenWindowAsync<TViewModel>(TViewModel viewModel, OpenWindowOptions options) where TViewModel : notnull
    {
        return OpenWindowAsync(typeof(TViewModel), viewModel, options);
    }

    public Task OpenWindowAsync<TViewModel>(Action<TViewModel> init, OpenWindowOptions options)
    {
        var viewModel = (TViewModel)_serviceProvider.GetService(typeof(TViewModel))!;
        init(viewModel);
        return OpenWindowAsync(typeof(TViewModel), viewModel, options);
    }

    public async Task OpenWindowAsync(Type viewModelType, object viewModel, OpenWindowOptions options)
    {
        var window = GetWindow(viewModelType);
        window.DataContext = viewModel;
        window.WindowStartupLocation = (System.Windows.WindowStartupLocation)options.WindowStartupLocation;
        window.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

        await NotifyNavigating(viewModel);
        window.Show();
        await NotifyNavigated(viewModel);
    }

    public async Task OpenDialogAsync(Type viewModelType, OpenWindowOptions options)
    {
        var viewModel = _serviceProvider.GetService(viewModelType)!;
        await OpenDialogAsync(viewModelType, viewModel, options);
    }

    public Task OpenDialogAsync<TViewModel>(TViewModel viewModel, OpenWindowOptions options) where TViewModel : notnull
    {
        return OpenDialogAsync(typeof(TViewModel), viewModel, options);
    }

    public Task OpenDialogAsync<TViewModel>(Action<TViewModel> init, OpenWindowOptions options)
    {
        var viewModel = (TViewModel)_serviceProvider.GetService(typeof(TViewModel))!;
        init(viewModel);
        return OpenDialogAsync(typeof(TViewModel), viewModel, options);
    }

    private async Task OpenDialogAsync(Type viewModelType, object viewModel, OpenWindowOptions options)
    {
        var window = GetWindow(viewModelType);
        window.DataContext = viewModel;
        window.WindowStartupLocation = (System.Windows.WindowStartupLocation)options.WindowStartupLocation;
        window.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

        await NotifyNavigating(viewModel);
        window.ShowDialog();
        await NotifyNavigated(viewModel);
    }

    private Window GetWindow(Type viewModelType)
    {
        Type viewType = ViewTypeCache.GetViewType(viewModelType);
        return (Window)_serviceProvider.GetService(viewType)!;
    }

    private async Task NotifyNavigating(object destination)
    {
        if (destination is INavigatingAsyncAware navigatingAsyncAware) await navigatingAsyncAware.OnNavigatingAsync();
        if (destination is INavigatingAware navigatingAware) navigatingAware.OnNavigating();
    }

    private async Task NotifyNavigated(object destination)
    {
        if (destination is INavigatedAsyncAware navigatedAsyncAware) await navigatedAsyncAware.OnNavigatedAsync();
        if (destination is INavigatedAware navigatedAware) navigatedAware.OnNavigated();
    }

}

public class NativeMethods
{
    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    public static extern IntPtr GetActiveWindow();

}