using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace GettingStarted;

public class MainViewModel
{
    private readonly IPresentationService _presentationService;

    public MainViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public ICommand NavigateCommand => new AsyncRelayCommand(Navigate);

    private Task Navigate() => _presentationService.NavigateToFirstAsync("Hello, Navigation Parameter!");
}