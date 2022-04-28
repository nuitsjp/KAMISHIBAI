using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using PropertyChanged;

namespace SampleBrowser.ViewModel.Page;

[AddINotifyPropertyChangedInterface]
public class WithoutArgumentsViewModel
{
    private readonly IPresentationService _presentationService;

    public WithoutArgumentsViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public string Message { get; set; } = "Default WindowName";
    public ICommand GoBackCommand => new AsyncRelayCommand(() => _presentationService.GoBackAsync());
}