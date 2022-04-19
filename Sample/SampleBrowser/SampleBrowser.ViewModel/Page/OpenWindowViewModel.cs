using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace SampleBrowser.ViewModel.Page;

public class OpenWindowViewModel
{
    private readonly IPresentationService _presentationService;

    public OpenWindowViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public ICommand OpenByTypeCommand =>
        new AsyncRelayCommand(() => _presentationService.OpenWindowAsync(typeof(ChildViewModel)));

    public ICommand OpenByGenericTypeCommand =>
        new AsyncRelayCommand(() => _presentationService.OpenWindowAsync<ChildViewModel>());

    public string WindowName1 { get; set; } = "Hello, Instance!";

    public ICommand OpenByInstanceCommand =>
        new AsyncRelayCommand(() => _presentationService.OpenWindowAsync(new ChildMessageViewModel(WindowName1, _presentationService)));

    public string WindowName2 { get; set; } = "Hello, Callback!";

    public ICommand OpenWithCallbackCommand =>
        new AsyncRelayCommand(() => _presentationService.OpenWindowAsync<ChildViewModel>(viewModel => viewModel.WindowName = WindowName2));

    public string WindowName3 { get; set; } = "Hello, Safe Parameters!";

    public ICommand OpenWithSafeParameterCommand =>
        new AsyncRelayCommand(() => _presentationService.OpenChildMessageWindowAsync(WindowName3));
}