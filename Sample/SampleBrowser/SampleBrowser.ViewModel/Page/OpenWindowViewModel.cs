using System.Reactive.Disposables;
using Kamishibai;
using Microsoft.Toolkit.Mvvm.Input;

namespace SampleBrowser.ViewModel.Page;

public class OpenWindowViewModel : IDisposingAware, IPausingAware
{
    private readonly CompositeDisposable _disposable = new();
    private readonly IPresentationService _presentationService;

    public OpenWindowViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public List<WindowStartupLocation> WindowStartupLocations => new()
    {
        WindowStartupLocation.CenterOwner,
        WindowStartupLocation.CenterScreen,
        WindowStartupLocation.Manual
    };

    public WindowStartupLocation SelectedWindowStartupLocation { get; set; } = WindowStartupLocation.CenterOwner;

    public AsyncRelayCommand<object> OpenByTypeCommand =>
        new(owner => _presentationService.OpenWindowAsync(typeof(WindowWithoutArgumentsViewModel), owner, new OpenWindowOptions { WindowStartupLocation = SelectedWindowStartupLocation })
            .AddTo(_disposable));

    public AsyncRelayCommand<object> OpenByGenericTypeCommand =>
        new (owner => _presentationService.OpenWindowAsync<WindowWithoutArgumentsViewModel>(owner, new OpenWindowOptions{WindowStartupLocation = SelectedWindowStartupLocation})
            .AddTo(_disposable));

    public string WindowName1 { get; set; } = "Hello, Instance!";

    public AsyncRelayCommand<object> OpenByInstanceCommand =>
        new(owner => _presentationService.OpenWindowAsync(new WindowWithArgumentsViewModel(WindowName1, _presentationService), owner, new OpenWindowOptions { WindowStartupLocation = SelectedWindowStartupLocation })
            .AddTo(_disposable));

    public string WindowName2 { get; set; } = "Hello, Callback!";

    public AsyncRelayCommand<object> OpenWithCallbackCommand =>
        new(owner => _presentationService.OpenWindowAsync<WindowWithoutArgumentsViewModel>(viewModel => viewModel.WindowName = WindowName2, owner, new OpenWindowOptions { WindowStartupLocation = SelectedWindowStartupLocation })
            .AddTo(_disposable));

    public string WindowName3 { get; set; } = "Hello, Safe Parameters!";

    public AsyncRelayCommand<object> OpenWithSafeParameterCommand =>
        new(owner => _presentationService.OpenWindowWithArgumentsWindowAsync(WindowName3, owner, new OpenWindowOptions { WindowStartupLocation = SelectedWindowStartupLocation })
            .AddTo(_disposable));

    public string WindowName4 { get; set; } = "Hello, Singleton (App-specific)";

    public AsyncRelayCommand<object> OpenSingletonCommand =>
        new(owner => _presentationService.OpenSingletonWindowWithArgumentsWindowAsync(WindowName4, owner, new OpenWindowOptions { WindowStartupLocation = SelectedWindowStartupLocation })
            .AddTo(_disposable));

    public RelayCommand ActivateCommand => new(() => InvokeWindowAction(x => x.Activate()));
    public RelayCommand HideCommand => new(() => InvokeWindowAction(x => x.Hide()));
    public RelayCommand ShowCommand => new(() => InvokeWindowAction(x => x.Show()));
    public RelayCommand MinimizeCommand => new(() => InvokeWindowAction(x => x.Minimize()));
    public RelayCommand MaximizeCommand => new(() => InvokeWindowAction(x => x.Maximize()));
    public RelayCommand RestoreCommand => new(() => InvokeWindowAction(x => x.Restore()));
    public RelayCommand CloseCommand => new(() => InvokeWindowAction(x => x.Close()));

    private void InvokeWindowAction(Action<IWindow> action)
    {
        foreach (var disposable in _disposable)
        {
            if (disposable is IWindow { IsClosed: false } window)
            {
                action(window);
            }
        }
    }

    public void OnDisposing(PreBackwardEventArgs args)
    {
        _disposable.Dispose();
    }

    public void OnPausing(PreForwardEventArgs args)
    {
        _disposable.Dispose();
    }
}