using System.Windows.Input;
using Kamishibai;
using Microsoft.Toolkit.Mvvm.Input;
using PropertyChanged;

namespace SampleBrowser.ViewModel.Page;

[AddINotifyPropertyChangedInterface]
public class NavigationMenuViewModel : IPausingAware
{
    private readonly IPresentationService _presentationService;

    public NavigationMenuViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public bool BlockNavigation { get; set; }

    public string Message { get; set; } = string.Empty;

    public ICommand NavigateByTypeCommand => 
        new AsyncRelayCommand(() => _presentationService.NavigateAsync(typeof(WithoutArgumentsViewModel)));

    public ICommand NavigateByGenericTypeCommand => 
        new AsyncRelayCommand(() => _presentationService.NavigateAsync<WithoutArgumentsViewModel>());

    public string Message1 { get; set; } = "Hello, Instance!";

    public ICommand NavigateByInstanceCommand =>
        new AsyncRelayCommand(() => _presentationService.NavigateAsync(new WithArgumentsViewModel(Message1, _presentationService)));

    public string Message2 { get; set; } = "Hello, Callback!";

    public ICommand NavigateWithCallbackCommand =>
        new AsyncRelayCommand(() => _presentationService.NavigateAsync<WithoutArgumentsViewModel>(viewModel => viewModel.Message = Message2));

    public string Message3 { get; set; } = "Hello, Parameter!";

    public ICommand NavigateWithSafeParameterCommand =>
        new AsyncRelayCommand(() => _presentationService.NavigateToWithArgumentsAsync(Message3));

    public bool OnPausing()
    {
        if (BlockNavigation)
        {
            Message = "Navigation blocked.";
            return false;
        }

        Message = string.Empty;
        return true;
    }
}