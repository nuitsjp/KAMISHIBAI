using System.Windows.Input;
using Kamishibai;
using Microsoft.Toolkit.Mvvm.Input;
using PropertyChanged;

namespace SampleBrowser.ViewModel.Page;

[AddINotifyPropertyChangedInterface]
public class OpenFilesViewModel
{
    private readonly IPresentationService _presentationService;

    public OpenFilesViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
        string[] filters = {
            "Image Files(*.png;*.jpg;*.jpeg;*.tif;*.tiff;*.bmp;*.gif)|*.png;*.jpg;*.jpeg;*.tif;*.tiff;*.bmp;*.gif",
            "All files(*.*)|*.*"
        };
        Filter = string.Join("|", filters);
        FilterIndexes = Enumerable.Range(1, filters.Length).ToArray();
    }

    public bool AddExtension { get; set; } = true;
    public bool CheckFileExists { get; set; } = true;
    public bool CheckPathExists { get; set; } = true;
    public string DefaultExt { get; set; } = string.Empty;
    public bool DereferenceLinks { get; set; } = true;
    public string Filter { get; set; }
    public int[] FilterIndexes { get; }
    public int SelectedFilterIndex { get; set; } = 2;
    public string InitialDirectory { get; set; } = string.Empty;
    public bool ReadOnlyChecked { get; set; } = false;
    public bool ShowReadOnly { get; set; } = false;
    public string Title { get; set; } = string.Empty;
    public bool ValidateNames { get; set; } = true;

    public bool? OpenFileResult { get; set; }
    public string[] FilePaths { get; set; } = Array.Empty<string>();
    public string? SelectedFilePath { get; set; }

    public ICommand OpenFileCommand => new RelayCommand(() =>
    {
        var context = new OpenFileContext
        {
            AddExtension = AddExtension,
            CheckFileExists = CheckFileExists,
            CheckPathExists = CheckPathExists,
            DefaultExt = DefaultExt,
            DereferenceLinks = DereferenceLinks,
            Filter = Filter,
            FilterIndex = SelectedFilterIndex,
            InitialDirectory = InitialDirectory,
            ReadOnlyChecked = ReadOnlyChecked,
            ShowReadOnly = ShowReadOnly,
            Title = Title,
            ValidateNames = ValidateNames
        };
        OpenFileResult = _presentationService.TryOpenFiles(context, out var files);
        FilePaths = files;
        SelectedFilePath = FilePaths.FirstOrDefault();
    });
}