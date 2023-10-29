using System.ComponentModel;
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

        PreForwardEventArgs preForwardEventArgs = new(null, null, viewModel);
        await NotifyNavigating(preForwardEventArgs);
        window.Show();
        PostForwardEventArgs postForwardEventArgs = new(null, null, viewModel);
        await NotifyNavigated(postForwardEventArgs);

        SetupCloseEvents(window);
    }

    public Task<bool> OpenDialogAsync(Type viewModelType, object? owner, OpenDialogOptions options)
    {
        var viewModel = _serviceProvider.GetService(viewModelType)!;
        return OpenDialogAsync(viewModelType, owner, viewModel, options);
    }

    public Task<bool> OpenDialogAsync<TViewModel>(object? owner, OpenDialogOptions options)
        => OpenDialogAsync(typeof(TViewModel), owner, options);

    public Task<bool> OpenDialogAsync<TViewModel>(TViewModel viewModel, object? owner, OpenDialogOptions options) where TViewModel : notnull
    {
        return OpenDialogAsync(typeof(TViewModel), owner, viewModel, options);
    }

    public Task<bool> OpenDialogAsync<TViewModel>(Action<TViewModel> init, object? owner, OpenDialogOptions options)
    {
        var viewModel = (TViewModel)_serviceProvider.GetService(typeof(TViewModel))!;
        init(viewModel);
        return OpenDialogAsync(typeof(TViewModel), owner, viewModel, options);
    }

    public Task CloseWindowAsync(object? window)
    {
        Window? target = (Window?)window ?? GetActiveWindow();
        if (target is null) return Task.CompletedTask;

        target.Close();
        return Task.CompletedTask;
    }

    public async Task CloseDialogAsync(bool dialogResult, object? window)
    {
        Window? target = (Window?)window ?? GetActiveWindow();
        if(target is null) return;

        PreBackwardEventArgs preBackwardEventArgs = new(null, target.DataContext, null);
        if (await NotifyDisposing(preBackwardEventArgs) is false) return;
        target.DialogResult = dialogResult;
        PostBackwardEventArgs postBackwardEventArgs = new(null, target.DataContext, null);
        await NotifyDisposed(postBackwardEventArgs);
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

    private async Task<bool> OpenDialogAsync(Type viewModelType, object? owner, object viewModel, OpenDialogOptions options)
    {
        var window = GetWindow(viewModelType);
        window.DataContext = viewModel;
        window.WindowStartupLocation = (System.Windows.WindowStartupLocation)options.WindowStartupLocation;
        window.Owner = (Window?)owner;

        await NotifyNavigating(new PreForwardEventArgs(null, null, viewModel));
        SetupCloseEvents(window);

        Exception? exception = null;
        void WindowOnLoaded(object sender, RoutedEventArgs args)
        {
            var task = NotifyNavigated(new PostForwardEventArgs(null, null, viewModel));
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

    private static async Task NotifyNavigating(PreForwardEventArgs args)
    {
        if (args.DestinationViewModel is INavigatingAsyncAware navigatingAsyncAware) await navigatingAsyncAware.OnNavigatingAsync(args);
        if (args.DestinationViewModel is INavigatingAware navigatingAware) navigatingAware.OnNavigating(args);
    }

    private static async Task NotifyNavigated(PostForwardEventArgs args)
    {
        if (args.DestinationViewModel is INavigatedAsyncAware navigatedAsyncAware) await navigatedAsyncAware.OnNavigatedAsync(args);
        if (args.DestinationViewModel is INavigatedAware navigatedAware) navigatedAware.OnNavigated(args);
    }

    public static void SetupCloseEvents(Window window)
    {
        window.Closing += Window_Closing;
        window.Closed += Window_Closed;
    }

    private static async void Window_Closing(object? sender, CancelEventArgs e)
    {
        if (sender is not Window window) return;

        var dialogResult = window.DialogResult;

        // Cancel window closing once
        e.Cancel = true;

        PreBackwardEventArgs preBackwardEventArgs = new(null, window.DataContext, null);
        // If the return value is false, it has already been canceled and returns as is.
        if (await NotifyDisposing(preBackwardEventArgs) is false) return;

        // Detach this event handler to prevent recursive calls.
        window.Closing -= Window_Closing;

        // Close the window manually.
        window.Dispatcher.InvokeAsync(() =>
        {
            if (dialogResult != null)
            {
                window.DialogResult = dialogResult;
            }
            else
            {
                window.Close();
            }
        });
    }

    private static async void Window_Closed(object? sender, EventArgs e)
    {
        if (sender is not Window window) return;

        await NotifyDisposed(new PostBackwardEventArgs(null, window.DataContext, null));
    }

    private static async Task<bool> NotifyDisposing(PreBackwardEventArgs args)
    {
        if (args.SourceViewModel is IDisposingAsyncAware disposingAsyncAware)
        {
            await disposingAsyncAware.OnDisposingAsync(args);
            if (args.Cancel)
            {
                return false;
            }
        }

        if (args.SourceViewModel is IDisposingAware disposingAware)
        {
            disposingAware.OnDisposing(args);
            if (args.Cancel)
            {
                return false;
            }
        }
        return true;
    }

    private static async Task NotifyDisposed(PostBackwardEventArgs args)
    {
        if (args.SourceViewModel is IDisposedAsyncAware disposedAsyncAware) await disposedAsyncAware.OnDisposedAsync(args);
        if (args.SourceViewModel is IDisposable disposable) disposable.Dispose();
    }

}
