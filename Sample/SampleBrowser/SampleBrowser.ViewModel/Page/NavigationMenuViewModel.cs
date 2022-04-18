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

    public ICommand NavigateByGenericTypeCommand => new AsyncRelayCommand(NavigateByGenericTypeAsync);

    private Task NavigateByTypeAsync()
        => _presentationService.NavigateAsync(typeof(ContentViewModel));

    private Task NavigateByGenericTypeAsync()
        => _presentationService.NavigateAsync<ContentViewModel>();

}