using Microsoft.Toolkit.Mvvm.Input;

namespace Kamishibai.Wpf.Demo.ViewModel;

public class ChildWindowViewModel
{
    private readonly IPresentationService _presentationService;

    public ChildWindowViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public string Message { get; set; } = string.Empty;
    public AsyncRelayCommand OpenWindowCommand => new(OnOpenWindowAsync);
    public AsyncRelayCommand OpenDialogCommand => new(OnOpenDialogAsync);

    private Task OnOpenWindowAsync()
    {
        return _presentationService.OpenWindowAsync(typeof(ChildWindowViewModel));
    }

    private Task OnOpenDialogAsync()
    {
        return _presentationService.OpenDialogAsync(typeof(ChildWindowViewModel));
    }
}