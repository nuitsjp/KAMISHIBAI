using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Automation;

namespace Kamishibai.Wpf.View;

public class WindowService : IWindowService
{
    private readonly IServiceProvider _serviceProvider;

    public WindowService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task OpenWindowAsync(Type viewModelType, object? owner, OpenWindowOptions options)
    {
        var viewModel = _serviceProvider.GetService(viewModelType)!;
        return OpenWindowAsync(viewModelType, owner, viewModel, options);
    }

    public Task OpenWindowAsync<TViewModel>(TViewModel viewModel, object? owner, OpenWindowOptions options) where TViewModel : notnull
    {
        return OpenWindowAsync(typeof(TViewModel), owner, viewModel, options);
    }

    public Task OpenWindowAsync<TViewModel>(Action<TViewModel> init, object? owner, OpenWindowOptions options)
    {
        var viewModel = (TViewModel)_serviceProvider.GetService(typeof(TViewModel))!;
        init(viewModel);
        return OpenWindowAsync(typeof(TViewModel), owner, viewModel, options);
    }

    public async Task OpenWindowAsync(Type viewModelType, object? owner, object viewModel, OpenWindowOptions options)
    {
        var window = GetWindow(viewModelType);
        window.DataContext = viewModel;
        window.WindowStartupLocation = (System.Windows.WindowStartupLocation)options.WindowStartupLocation;
        window.Owner = (Window?)owner;

        await NotifyNavigating(viewModel);
        window.Show();
        await NotifyNavigated(viewModel);
    }

    public Task<bool> OpenDialogAsync(Type viewModelType, object? owner, OpenWindowOptions options)
    {
        var viewModel = _serviceProvider.GetService(viewModelType)!;
        return OpenDialogAsync(viewModelType, owner, viewModel, options);
    }

    public Task<bool> OpenDialogAsync<TViewModel>(TViewModel viewModel, object? owner, OpenWindowOptions options) where TViewModel : notnull
    {
        return OpenDialogAsync(typeof(TViewModel), owner, viewModel, options);
    }

    public Task<bool> OpenDialogAsync<TViewModel>(Action<TViewModel> init, object? owner, OpenWindowOptions options)
    {
        var viewModel = (TViewModel)_serviceProvider.GetService(typeof(TViewModel))!;
        init(viewModel);
        return OpenDialogAsync(typeof(TViewModel), owner, viewModel, options);
    }

    public async Task CloseWindowAsync(object? window)
    {
        Window? target = (Window?)window ?? GetActiveWindow();
        if (target is null) return;

        await NotifyDisposing(target.DataContext);
        target.Close();
        await NotifyDisposed(target.DataContext);
    }

    public async Task CloseDialogAsync(bool dialogResult, object? window)
    {
        Window? target = (Window?)window ?? GetActiveWindow();
        if(target is null) return;

        await NotifyDisposing(target.DataContext);
        target.DialogResult = dialogResult;
        await NotifyDisposed(target.DataContext);
    }

    private Window? GetActiveWindow()
        => Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

    private async Task<bool> OpenDialogAsync(Type viewModelType, object? owner, object viewModel, OpenWindowOptions options)
    {
        var window = GetWindow(viewModelType);
        window.DataContext = viewModel;
        window.WindowStartupLocation = (System.Windows.WindowStartupLocation)options.WindowStartupLocation;
        window.Owner = (Window?)owner;

        await NotifyNavigating(viewModel);

        Exception? exception = null;
        void WindowOnLoaded(object sender, RoutedEventArgs args)
        {
            var task = NotifyNavigated(viewModel);
            exception = task.Exception;
        }

        window.Loaded += WindowOnLoaded;
        try
        {
            var result = window.ShowDialog() == true;
            if (exception is not null)
            {
                throw new AggregateException(exception);
            }
            return result;
        }
        finally
        {
            window.Loaded -= WindowOnLoaded;
        }
    }

    private Window GetWindow(Type viewModelType)
    {
        Type viewType = ViewTypeCache.GetViewType(viewModelType);
        return (Window)_serviceProvider.GetService(viewType)!;
    }

    private static async Task NotifyNavigating(object destination)
    {
        if (destination is INavigatingAsyncAware navigatingAsyncAware) await navigatingAsyncAware.OnNavigatingAsync();
        if (destination is INavigatingAware navigatingAware) navigatingAware.OnNavigating();
    }

    private static async Task NotifyNavigated(object destination)
    {
        if (destination is INavigatedAsyncAware navigatedAsyncAware) await navigatedAsyncAware.OnNavigatedAsync();
        if (destination is INavigatedAware navigatedAware) navigatedAware.OnNavigated();
    }

    private async Task<bool> NotifyDisposing(object sourceViewModel)
    {
        if (sourceViewModel is IDisposingAsyncAware disposingAsyncAware)
        {
            if (await disposingAsyncAware.OnDisposingAsync() is false)
            {
                return false;
            }
        }

        if (sourceViewModel is IDisposingAware disposingAware)
        {
            if (disposingAware.OnDisposing() is false)
            {
                return false;
            }
        }
        return true;
    }

    private async Task NotifyDisposed(object sourceViewModel)
    {
        if (sourceViewModel is IDisposedAsyncAware disposedAsyncAware) await disposedAsyncAware.OnDisposedAsync();
        if (sourceViewModel is IDisposable disposable) disposable.Dispose();
    }

}
