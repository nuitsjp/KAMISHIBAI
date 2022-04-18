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

    public ICommand NavigateByTypeCommand => new AsyncRelayCommand(NavigateByTypeAsync);

    private Task NavigateByTypeAsync()
        => _presentationService.NavigateAsync(typeof(ContentViewModel));
}