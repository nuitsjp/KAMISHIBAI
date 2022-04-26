using Kamishibai;
using Microsoft.Toolkit.Mvvm.Input;

namespace SampleBrowser.ViewModel.Page;

public class OpenWindowViewModel
{
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
        new(owner => _presentationService.OpenWindowAsync(typeof(ChildViewModel), owner, new OpenWindowOptions {WindowStartupLocation = SelectedWindowStartupLocation}));

    public AsyncRelayCommand<object> OpenByGenericTypeCommand =>
        new (owner => _presentationService.OpenWindowAsync<ChildViewModel>(owner, new OpenWindowOptions{WindowStartupLocation = SelectedWindowStartupLocation}));

    public string WindowName1 { get; set; } = "Hello, Instance!";

    public AsyncRelayCommand<object> OpenByInstanceCommand =>
        new(owner => _presentationService.OpenWindowAsync(new ChildMessageViewModel(WindowName1, _presentationService), owner, new OpenWindowOptions { WindowStartupLocation = SelectedWindowStartupLocation }));

    public string WindowName2 { get; set; } = "Hello, Callback!";

    public AsyncRelayCommand<object> OpenWithCallbackCommand =>
        new(owner => _presentationService.OpenWindowAsync<ChildViewModel>(viewModel => viewModel.WindowName = WindowName2, owner, new OpenWindowOptions { WindowStartupLocation = SelectedWindowStartupLocation }));

    public string WindowName3 { get; set; } = "Hello, Safe Parameters!";

    public AsyncRelayCommand<object> OpenWithSafeParameterCommand =>
        new(owner => _presentationService.OpenChildMessageWindowAsync(WindowName3, owner, new OpenWindowOptions { WindowStartupLocation = SelectedWindowStartupLocation }));
}