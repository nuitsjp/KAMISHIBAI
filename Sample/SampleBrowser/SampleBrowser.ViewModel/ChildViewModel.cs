using System.Windows.Input;
using Kamishibai;
using Microsoft.Toolkit.Mvvm.Input;
using PropertyChanged;

namespace SampleBrowser.ViewModel;

[AddINotifyPropertyChangedInterface]
public class ChildViewModel : IDisposingAware
{
    private readonly IPresentationService _presentationService;

    public ChildViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public bool BlockClosing { get; set; }
    public string Message { get; set; } = string.Empty;
    public string WindowName { get; set; } = "Child Window";

    public ICommand CloseCommand => new AsyncRelayCommand(() => _presentationService.CloseWindowAsync());

    public AsyncRelayCommand<object> CloseSpecifiedWindowCommand => new (window => _presentationService.CloseWindowAsync(window));
    public bool OnDisposing()
    {
        if (BlockClosing)
        {
            Message = "Closing blocked.";
            return false;
        }

        Message = string.Empty;
        return true;

    }
}