using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace GettingStarted.ViewModel;

public class MainViewModel
{
    private readonly IPresentationService _presentationService;

    public MainViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public ICommand NavigateCommand =>
        new AsyncRelayCommand(() => _presentationService.NavigateToFirstAsync("Hello, KAMISHIBAI!"));
}