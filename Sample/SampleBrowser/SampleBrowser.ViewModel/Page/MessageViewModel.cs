using System.Windows.Input;
using Kamishibai;
using Microsoft.Toolkit.Mvvm.Input;

namespace SampleBrowser.ViewModel.Page;

[Navigatable]
public class MessageViewModel
{
    private readonly IPresentationService _presentationService;

    public MessageViewModel(
        string message, 
        [Inject] IPresentationService presentationService)
    {
        Message = message;
        _presentationService = presentationService;
    }

    public string Message { get; }
    public ICommand GoBackCommand => new AsyncRelayCommand(() => _presentationService.GoBackAsync());
}