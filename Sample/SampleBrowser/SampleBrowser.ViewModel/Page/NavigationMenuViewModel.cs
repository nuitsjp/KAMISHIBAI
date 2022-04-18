using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace SampleBrowser.ViewModel.Page;

public class NavigationMenuViewModel
{
    private readonly IPresentationService _presentationService;

    public NavigationMenuViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public ICommand NavigateByTypeCommand => 
        new AsyncRelayCommand(() => _presentationService.NavigateAsync(typeof(ContentViewModel)));

    public ICommand NavigateByGenericTypeCommand => 
        new AsyncRelayCommand(() => _presentationService.NavigateAsync<ContentViewModel>());

    public string Message1 { get; set; } = "Hello, Instance!";

    public ICommand NavigateByInstanceCommand =>
        new AsyncRelayCommand(() => _presentationService.NavigateAsync(new MessageViewModel(Message1, _presentationService)));

    public string Message2 { get; set; } = "Hello, Callback!";

    public ICommand NavigateWithCallbackCommand =>
        new AsyncRelayCommand(() => _presentationService.NavigateAsync<ContentViewModel>(viewModel => viewModel.Message = Message2));

    public string Message3 { get; set; } = "Hello, Parameter!";

    public ICommand NavigateWithSafeParameterCommand =>
        new AsyncRelayCommand(() => _presentationService.NavigateToMessageAsync(Message3));
}