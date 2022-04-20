using System.Windows.Input;
using Kamishibai;
using Microsoft.Toolkit.Mvvm.Input;

namespace SampleBrowser.ViewModel.Page;

public class OpenFileViewModel
{
    private readonly IPresentationService _presentationService;

    public OpenFileViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public bool? OpenFileResult { get; set; }
    public string FilePath { get; set; }

    public ICommand OpenFileCommand => new RelayCommand(() =>
    {
        var context = new OpenFileContext();
        OpenFileResult = _presentationService.TryOpenFile(context, out var file);
        FilePath = file;
    });
}