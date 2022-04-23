using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using Application = System.Windows.Application;

namespace Kamishibai;

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

    public Task OpenWindowAsync<TViewModel>(object? owner, OpenWindowOptions options)
        => OpenWindowAsync(typeof(TViewModel), owner, options);

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

    public Task<bool> OpenDialogAsync<TViewModel>(object? owner, OpenWindowOptions options)
        => OpenDialogAsync(typeof(TViewModel), owner, options);

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

        if(await NotifyDisposing(target.DataContext) is false) return;
        target.Close();
        await NotifyDisposed(target.DataContext);
    }

    public async Task CloseDialogAsync(bool dialogResult, object? window)
    {
        Window? target = (Window?)window ?? GetActiveWindow();
        if(target is null) return;

        if (await NotifyDisposing(target.DataContext) is false) return;
        target.DialogResult = dialogResult;
        await NotifyDisposed(target.DataContext);
    }

    public MessageBoxResult ShowMessage(
        string messageBoxText, 
        string caption, 
        MessageBoxButton button, 
        MessageBoxImage icon, 
        MessageBoxResult defaultResult, 
        MessageBoxOptions options, 
        object? owner)
    {
        var messageDialog = owner is null ? 
            new MessageDialog() : 
            new MessageDialog((Window)owner);
        messageDialog.Text = messageBoxText;
        messageDialog.Caption = caption;
        messageDialog.Buttons = button;
        messageDialog.Icon = icon;
        messageDialog.DefaultResult = defaultResult;
        messageDialog.Options = options;

        return (MessageBoxResult)messageDialog.Show();
    }

    public DialogResult OpenFile(OpenFileDialogContext context)
    {
        var openFileDialog = new CommonOpenFileDialog
        {
            AddToMostRecentlyUsedList = context.AddToMostRecentlyUsedList,
            AllowNonFileSystemItems = context.AllowNonFileSystemItems,
            AllowPropertyEditing = context.AllowPropertyEditing,
            CookieIdentifier = context.CookieIdentifier,
            DefaultDirectory = context.DefaultDirectory,
            DefaultExtension = context.DefaultExtension,
            DefaultFileName = context.DefaultFileName,
            EnsureFileExists = context.EnsureFileExists,
            EnsurePathExists = context.EnsurePathExists,
            EnsureReadOnly = context.EnsureReadOnly,
            EnsureValidNames = context.EnsureValidNames,
            InitialDirectory = context.InitialDirectory,
            IsFolderPicker = context.IsFolderPicker,
            Multiselect = context.Multiselect,
            NavigateToShortcut = context.NavigateToShortcut,
            RestoreDirectory = context.RestoreDirectory,
            ShowHiddenItems = context.ShowHiddenItems,
            ShowPlacesList = context.ShowPlacesList,
            Title = context.Title,
        };
        foreach (var filter in context.Filters)
        {
            openFileDialog.Filters.Add(
                new CommonFileDialogFilter(
                    filter.RawDisplayName,
                    string.Join(";", filter.Extensions)));
        }

        foreach (var place in context.CustomPlaces)
        {
            openFileDialog.AddPlace(place.Path, Microsoft.WindowsAPICodePack.Shell.FileDialogAddPlaceLocation.Top);
        }
        var result = (DialogResult)openFileDialog.ShowDialog();
        if (result == DialogResult.Ok)
        {
            context.FileName = openFileDialog.Multiselect ? string.Empty : openFileDialog.FileName;
            context.FileNames = openFileDialog.FileNames;
        }
        return result;
    }

    public DialogResult SaveFile(SaveFileDialogContext context)
    {
        var openFileDialog = new CommonSaveFileDialog
        {
            AddToMostRecentlyUsedList = context.AddToMostRecentlyUsedList,
            AllowPropertyEditing = context.AllowPropertyEditing,
            AlwaysAppendDefaultExtension = context.AlwaysAppendDefaultExtension,
            CookieIdentifier = context.CookieIdentifier,
            CreatePrompt = context.CreatePrompt,
            DefaultDirectory = context.DefaultDirectory,
            DefaultExtension = context.DefaultExtension,
            DefaultFileName = context.DefaultFileName,
            EnsureFileExists = context.EnsureFileExists,
            EnsurePathExists = context.EnsurePathExists,
            EnsureReadOnly = context.EnsureReadOnly,
            EnsureValidNames = context.EnsureValidNames,
            InitialDirectory = context.InitialDirectory,
            IsExpandedMode = context.IsExpandedMode,
            NavigateToShortcut = context.NavigateToShortcut,
            OverwritePrompt = context.OverwritePrompt,
            RestoreDirectory = context.RestoreDirectory,
            ShowHiddenItems = context.ShowHiddenItems,
            ShowPlacesList = context.ShowPlacesList,
            Title = context.Title,
        };
        foreach (var filter in context.Filters)
        {
            openFileDialog.Filters.Add(
                new CommonFileDialogFilter(
                    filter.RawDisplayName,
                    string.Join(";", filter.Extensions)));
        }

        foreach (var place in context.CustomPlaces)
        {
            openFileDialog.AddPlace(place.Path, Microsoft.WindowsAPICodePack.Shell.FileDialogAddPlaceLocation.Top);
        }
        var result = (DialogResult)openFileDialog.ShowDialog();
        if (result == DialogResult.Ok)
        {
            context.FileName = openFileDialog.FileName;
        }
        return result;
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
