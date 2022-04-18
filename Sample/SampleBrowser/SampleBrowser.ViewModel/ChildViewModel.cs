using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace SampleBrowser.ViewModel;

public class ChildViewModel
{
    private readonly IPresentationService _presentationService;

    public ChildViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public string WindowName { get; set; } = "Child Window";

    public ICommand CloseCommand => new AsyncRelayCommand(() => _presentationService.CloseWindowAsync());

    public AsyncRelayCommand<object> CloseSpecifiedWindowCommand => new (window => _presentationService.CloseWindowAsync(window));
}