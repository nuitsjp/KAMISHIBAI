using System.Windows.Input;
using Kamishibai;
using Microsoft.Toolkit.Mvvm.Input;
using PropertyChanged;

namespace SampleBrowser.ViewModel.Page;

[AddINotifyPropertyChangedInterface]
public class SaveFileViewModel
{
    private readonly IPresentationService _presentationService;

    public SaveFileViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
        string[] filters = {
            "Image FileNames(*.png;*.jpg;*.jpeg;*.tif;*.tiff;*.bmp;*.gif)|*.png;*.jpg;*.jpeg;*.tif;*.tiff;*.bmp;*.gif",
            "All files(*.*)|*.*"
        };
        Filter = string.Join(",", filters);
    }

    public bool AddToMostRecentlyUsedList { get; set; } = true;
    public bool AllowPropertyEditing { get; set; }
    public bool AlwaysAppendDefaultExtension { get; set; }
    public bool CreatePrompt { get; set; }
    public string DefaultDirectory { get; set; } = string.Empty;
    public string DefaultExtension { get; set; } = string.Empty;
    public string DefaultFileName { get; set; } = string.Empty;
    public bool EnsureFileExists { get; set; } = true;
    public bool EnsurePathExists { get; set; } = true;
    public bool EnsureReadOnly { get; set; } = true;
    public bool EnsureValidNames { get; set; }
    public string Filter { get; set; }
    public string InitialDirectory { get; set; } = string.Empty;
    public bool IsExpandedMode { get; set; }
    public bool NavigateToShortcut { get; set; } = true;
    public bool OverwritePrompt { get; set; }
    public bool RestoreDirectory { get; set; }
    public bool ShowHiddenItems { get; set; }
    public bool ShowPlacesList { get; set; } = true;
    public string Place { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DialogResult? OpenFileResult { get; set; }
    public string FilePath { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public ICommand SaveFileCommand => new RelayCommand(() =>
    {
        ErrorMessage = string.Empty;
        List<FileDialogFilter> filters =
            string.IsNullOrWhiteSpace(Filter)
                ? new List<FileDialogFilter>()
                : Filter.Split(',')
                    .Select(x => x.Split('|'))
                    .Select(x => new FileDialogFilter(x[0], x[1].Split(';')))
                    .ToList();

        try
        {
            var context = new SaveFileDialogContext
            {
                AddToMostRecentlyUsedList = AddToMostRecentlyUsedList,
                AllowPropertyEditing = AllowPropertyEditing,
                AlwaysAppendDefaultExtension = AlwaysAppendDefaultExtension,
                CreatePrompt = CreatePrompt,
                DefaultDirectory = DefaultDirectory,
                DefaultExtension = DefaultExtension,
                DefaultFileName = DefaultFileName,
                EnsureFileExists = EnsureFileExists,
                EnsurePathExists = EnsurePathExists,
                EnsureReadOnly = EnsureReadOnly,
                EnsureValidNames = EnsureValidNames,
                Filters = filters,
                InitialDirectory = InitialDirectory,
                IsExpandedMode = IsExpandedMode,
                NavigateToShortcut = NavigateToShortcut,
                OverwritePrompt = OverwritePrompt,
                RestoreDirectory = RestoreDirectory,
                ShowHiddenItems = ShowHiddenItems,
                ShowPlacesList = ShowPlacesList,
                Title = Title,
            };
            if (string.IsNullOrWhiteSpace(Place) is false)
            {
                context.CustomPlaces.Add(new FileDialogCustomPlace(Place, FileDialogAddPlaceLocation.Top));
            }
            OpenFileResult = _presentationService.SaveFile(context);
            FilePath = context.FileName;
        }
        catch (Exception e)
        {
            ErrorMessage = $"Unavailable combinations (e.g., Folder Selection and Filter). {e.Message}";
        }
    });
}