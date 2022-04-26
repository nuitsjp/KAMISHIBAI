using Kamishibai;
using Microsoft.Toolkit.Mvvm.Input;

namespace SampleBrowser.ViewModel.Page;

public class ShowMessageViewModel
{
    private readonly IPresentationService _presentationService;

    public ShowMessageViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public string Message { get; set; } = "Hello, Show Message!";
    public string Caption { get; set; } = "Caption";

    public List<MessageBoxButton> MessageBoxButtons => new()
    {
        MessageBoxButton.OK,
        MessageBoxButton.OKCancel,
        MessageBoxButton.YesNo,
        MessageBoxButton.YesNoCancel,
    };

    public MessageBoxButton SelectedMessageBoxButton { get; set; } = MessageBoxButton.OK;

    public List<MessageBoxImage> MessageBoxImages => new()
    {
        MessageBoxImage.Asterisk,
        MessageBoxImage.Error,
        MessageBoxImage.Exclamation,
        MessageBoxImage.Hand,
        MessageBoxImage.Information,
        MessageBoxImage.None,
        MessageBoxImage.Question,
        MessageBoxImage.Stop,
        MessageBoxImage.Warning
    };

    public MessageBoxImage SelectedMessageBoxImage { get; set; } = MessageBoxImage.Asterisk;

    public List<MessageBoxResult> MessageBoxResults => new()
    {
        MessageBoxResult.Cancel,
        MessageBoxResult.No,
        MessageBoxResult.None,
        MessageBoxResult.OK,
        MessageBoxResult.Yes
    };

    public MessageBoxResult SelectedMessageBoxResult { get; set; } = MessageBoxResult.Cancel;

    public List<MessageBoxOptions> MessageBoxOptions => new()
    {
        Kamishibai.MessageBoxOptions.DefaultDesktopOnly,
        Kamishibai.MessageBoxOptions.None,
        Kamishibai.MessageBoxOptions.RightAlign,
        Kamishibai.MessageBoxOptions.RtlReading,
        Kamishibai.MessageBoxOptions.ServiceNotification
    };

    public MessageBoxOptions SelectedMessageBoxOptions { get; set; } = Kamishibai.MessageBoxOptions.None;

    public RelayCommand<object> ShowMessageCommand => new(owner
        =>
    {
        var ownerWindow = 
            SelectedMessageBoxOptions is 
                Kamishibai.MessageBoxOptions.DefaultDesktopOnly
                or Kamishibai.MessageBoxOptions.ServiceNotification
            ? null
            : owner;
        _presentationService.ShowMessage(
            Message,
            Caption,
            SelectedMessageBoxButton,
            SelectedMessageBoxImage,
            SelectedMessageBoxResult,
            SelectedMessageBoxOptions,
            ownerWindow);
    });
}