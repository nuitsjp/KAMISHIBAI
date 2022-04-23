using System.Windows.Input;
using Kamishibai;
using Microsoft.Toolkit.Mvvm.Input;

namespace SampleBrowser.ViewModel.Page;

public class OpenDialogViewModel
{
    private readonly IPresentationService _presentationService;

    public OpenDialogViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public ICommand OpenByTypeCommand =>
        new AsyncRelayCommand(() => _presentationService.OpenDialogAsync(typeof(ChildViewModel), options: new OpenWindowOptions{WindowStartupLocation = WindowStartupLocation.CenterOwner}));

    public ICommand OpenByGenericTypeCommand =>
        new AsyncRelayCommand(() => _presentationService.OpenDialogAsync<ChildViewModel>());

    public string WindowName1 { get; set; } = "Hello, Instance!";

    public ICommand OpenByInstanceCommand =>
        new AsyncRelayCommand(() => _presentationService.OpenDialogAsync(new ChildMessageViewModel(WindowName1, _presentationService)));

    public string WindowName2 { get; set; } = "Hello, Callback!";

    public ICommand OpenWithCallbackCommand =>
        new AsyncRelayCommand(() => _presentationService.OpenDialogAsync<ChildViewModel>(viewModel => viewModel.WindowName = WindowName2));

    public string WindowName3 { get; set; } = "Hello, Safe Parameters!";

    public ICommand OpenWithSafeParameterCommand =>
        new AsyncRelayCommand(() => _presentationService.OpenChildMessageDialogAsync(WindowName3));
}