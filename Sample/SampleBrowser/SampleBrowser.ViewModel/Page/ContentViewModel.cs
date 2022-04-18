using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace SampleBrowser.ViewModel.Page;

public class ContentViewModel
{
    private readonly IPresentationService _presentationService;

    public ContentViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public ICommand GoBackCommand => new AsyncRelayCommand(() => _presentationService.GoBackAsync());
}