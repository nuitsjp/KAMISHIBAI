using Microsoft.Toolkit.Mvvm.Input;

namespace Kamishibai.Wpf.Demo.ViewModel;

public class ChildWindowViewModel
{
    private readonly IPresentationService _presentationService;

    public ChildWindowViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public AsyncRelayCommand OpenWindowCommand => new(OnOpenWindow);

    private Task OnOpenWindow()
    {
        return _presentationService.OpenWindow(typeof(ChildWindowViewModel));
    }

}