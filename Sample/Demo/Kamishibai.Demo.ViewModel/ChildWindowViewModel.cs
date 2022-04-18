using Microsoft.Toolkit.Mvvm.Input;

namespace Kamishibai.Demo.ViewModel;

public class ChildWindowViewModel
{
    private readonly IPresentationService _presentationService;

    public ChildWindowViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public string Message { get; set; } = string.Empty;
    public AsyncRelayCommand<object> OpenWindowCommand => new(OnOpenWindowAsync);
    public AsyncRelayCommand OpenDialogCommand => new(OnOpenDialogAsync);
    public AsyncRelayCommand CloseWindowCommand => new(OnCloseWindowAsync);
    public AsyncRelayCommand CloseDialogCommand => new(OnCloseDialogAsync);

#nullable enable
    private Task OnOpenWindowAsync(object? window)
    {
#nullable disable
        return _presentationService.OpenWindowAsync(typeof(ChildWindowViewModel), window);
    }

    private Task OnOpenDialogAsync()
    {
        return _presentationService.OpenDialogAsync(typeof(ChildWindowViewModel));
    }
    private Task OnCloseWindowAsync()
    {
        _presentationService.CloseWindowAsync();
        return Task.CompletedTask;
    }

    private Task OnCloseDialogAsync()
    {
        _presentationService.CloseDialogAsync(true);
        return Task.CompletedTask;
    }

}