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

    public void OnDisposing(PreBackwardEventArgs args)
    {
        _disposable.Dispose();
    }

    public void OnPausing(PreForwardEventArgs args)
    {
        _disposable.Dispose();
    }
}