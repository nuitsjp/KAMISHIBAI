using Microsoft.WindowsAPICodePack.Dialogs;

namespace Kamishibai;

public sealed class WindowsApiCodePackFileDialogService : IFileDialogService
{
    public DialogResult OpenFile(OpenFileDialogContext context)
    {
        var openFileDialog = new CommonOpenFileDialog
        {
            AddToMostRecentlyUsedList = context.AddToMostRecentlyUsedList,
            AllowNonFileSystemItems = context.AllowNonFileSystemItems,
            AllowPropertyEditing = context.AllowPropertyEditing,
            CookieIdentifier = context.CookieIdentifier,
            DefaultDirectory = context.DefaultDirectory,
            DefaultExtension = context.DefaultExtension,
            DefaultFileName = context.DefaultFileName,
            EnsureFileExists = context.EnsureFileExists,
            EnsurePathExists = context.EnsurePathExists,
            EnsureReadOnly = context.EnsureReadOnly,
            EnsureValidNames = context.EnsureValidNames,
            InitialDirectory = context.InitialDirectory,
            IsFolderPicker = context.IsFolderPicker,
            Multiselect = context.Multiselect,
            NavigateToShortcut = context.NavigateToShortcut,
            RestoreDirectory = context.RestoreDirectory,
            ShowHiddenItems = context.ShowHiddenItems,
            ShowPlacesList = context.ShowPlacesList,
            Title = context.Title,
        };
        foreach (var filter in context.Filters)
        {
            openFileDialog.Filters.Add(
                new CommonFileDialogFilter(
                    filter.RawDisplayName,
                    string.Join(";", filter.Extensions)));
        }

        foreach (var place in context.CustomPlaces)
        {
            openFileDialog.AddPlace(place.Path, Microsoft.WindowsAPICodePack.Shell.FileDialogAddPlaceLocation.Top);
        }
        var result = (DialogResult)openFileDialog.ShowDialog();
        if (result == DialogResult.Ok)
        {
            context.FileName = openFileDialog.Multiselect ? string.Empty : openFileDialog.FileName;
            context.FileNames = openFileDialog.FileNames;
        }
        return result;
    }

    public DialogResult SaveFile(SaveFileDialogContext context)
    {
        var openFileDialog = new CommonSaveFileDialog
        {
            AddToMostRecentlyUsedList = context.AddToMostRecentlyUsedList,
            AllowPropertyEditing = context.AllowPropertyEditing,
            AlwaysAppendDefaultExtension = context.AlwaysAppendDefaultExtension,
            CookieIdentifier = context.CookieIdentifier,
            CreatePrompt = context.CreatePrompt,
            DefaultDirectory = context.DefaultDirectory,
            DefaultExtension = context.DefaultExtension,
            DefaultFileName = context.DefaultFileName,
            EnsureFileExists = context.EnsureFileExists,
            EnsurePathExists = context.EnsurePathExists,
            EnsureReadOnly = context.EnsureReadOnly,
            EnsureValidNames = context.EnsureValidNames,
            InitialDirectory = context.InitialDirectory,
            IsExpandedMode = context.IsExpandedMode,
            NavigateToShortcut = context.NavigateToShortcut,
            OverwritePrompt = context.OverwritePrompt,
            RestoreDirectory = context.RestoreDirectory,
            ShowHiddenItems = context.ShowHiddenItems,
            ShowPlacesList = context.ShowPlacesList,
            Title = context.Title,
        };
        foreach (var filter in context.Filters)
        {
            openFileDialog.Filters.Add(
                new CommonFileDialogFilter(
                    filter.RawDisplayName,
                    string.Join(";", filter.Extensions)));
        }

        foreach (var place in context.CustomPlaces)
        {
            openFileDialog.AddPlace(place.Path, Microsoft.WindowsAPICodePack.Shell.FileDialogAddPlaceLocation.Top);
        }
        var result = (DialogResult)openFileDialog.ShowDialog();
        if (result == DialogResult.Ok)
        {
            context.FileName = openFileDialog.FileName;
        }
        return result;
    }
}
