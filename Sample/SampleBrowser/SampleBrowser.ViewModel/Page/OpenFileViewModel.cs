using System.Windows.Input;
using Kamishibai;
using Microsoft.Toolkit.Mvvm.Input;
using PropertyChanged;

namespace SampleBrowser.ViewModel.Page;

[AddINotifyPropertyChangedInterface]
public class OpenFileViewModel
{
    private readonly IPresentationService _presentationService;

    public OpenFileViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public bool AddExtension { get; set; } = true;

    public bool? OpenFileResult { get; set; }
    public string FilePath { get; set; } = string.Empty;

    public ICommand OpenFileCommand => new RelayCommand(() =>
    {
        var context = new OpenFileContext();
        OpenFileResult = _presentationService.TryOpenFile(context, out var file);
        FilePath = file;
    });
}