using System.Runtime.InteropServices;
using System.IO;
using System.Windows;
using System.Windows.Interop;

namespace Kamishibai;

/// <summary>
/// Displays the Windows Common Item Dialog through the public Shell COM API.
/// </summary>
public sealed class Win32FileDialogService : IFileDialogService
{
    public DialogResult OpenFile(OpenFileDialogContext context)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        IFileOpenDialog? dialog = null;
        try
        {
            dialog = (IFileOpenDialog)new FileOpenDialogComObject();
            Configure(dialog, context, GetOpenOptions(context));

            var result = Show(dialog);
            if (result != DialogResult.Ok)
            {
                return result;
            }

            var fileNames = context.Multiselect
                ? GetResults(dialog, context.AllowNonFileSystemItems)
                : new[] { GetResult(dialog, context.AllowNonFileSystemItems) };

            context.FileName = context.Multiselect ? string.Empty : fileNames[0];
            context.FileNames = fileNames;
            return DialogResult.Ok;
        }
        finally
        {
            ReleaseComObject(dialog);
        }
    }

    public DialogResult SaveFile(SaveFileDialogContext context)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        IFileSaveDialog? dialog = null;
        try
        {
            dialog = (IFileSaveDialog)new FileSaveDialogComObject();
            Configure(dialog, context, GetSaveOptions(context));

            var result = Show(dialog);
            if (result != DialogResult.Ok)
            {
                return result;
            }

            context.FileName = AppendDefaultExtension(
                GetResult(dialog, allowNonFileSystemItems: false),
                GetDefaultExtension(context));
            return DialogResult.Ok;
        }
        finally
        {
            ReleaseComObject(dialog);
        }
    }

    private static FileOpenDialogOptions GetOpenOptions(OpenFileDialogContext context)
    {
        var options = GetCommonOptions(context);
        SetOption(ref options, FileOpenDialogOptions.ForceFileSystem, !context.AllowNonFileSystemItems);
        SetOption(ref options, FileOpenDialogOptions.PickFolders, context.IsFolderPicker);
        SetOption(ref options, FileOpenDialogOptions.AllowMultiSelect, context.Multiselect);
        return options;
    }

    private static FileOpenDialogOptions GetSaveOptions(SaveFileDialogContext context)
    {
        var options = GetCommonOptions(context);
        SetOption(ref options, FileOpenDialogOptions.ForceFileSystem, enabled: true);
        SetOption(ref options, FileOpenDialogOptions.StrictFileTypes, context.AlwaysAppendDefaultExtension);
        SetOption(ref options, FileOpenDialogOptions.CreatePrompt, context.CreatePrompt);
        SetOption(ref options, FileOpenDialogOptions.DefaultNoMiniMode, context.IsExpandedMode);
        SetOption(ref options, FileOpenDialogOptions.OverwritePrompt, context.OverwritePrompt);
        return options;
    }

    private static FileOpenDialogOptions GetCommonOptions(FileDialogContext context)
    {
        var options = FileOpenDialogOptions.NoTestFileCreate;
        SetOption(ref options, FileOpenDialogOptions.DontAddToRecent, !context.AddToMostRecentlyUsedList);
        SetOption(ref options, FileOpenDialogOptions.FileMustExist, context.EnsureFileExists);
        SetOption(ref options, FileOpenDialogOptions.PathMustExist, context.EnsurePathExists);
        SetOption(ref options, FileOpenDialogOptions.NoReadOnlyReturn, !context.EnsureReadOnly);
        SetOption(ref options, FileOpenDialogOptions.NoValidate, !context.EnsureValidNames);
        SetOption(ref options, FileOpenDialogOptions.NoDereferenceLinks, !context.NavigateToShortcut);
        SetOption(ref options, FileOpenDialogOptions.NoChangeDirectory, context.RestoreDirectory);
        SetOption(ref options, FileOpenDialogOptions.ForceShowHidden, context.ShowHiddenItems);
        SetOption(ref options, FileOpenDialogOptions.HidePinnedPlaces, !context.ShowPlacesList);
        return options;
    }

    private static void SetOption(
        ref FileOpenDialogOptions options,
        FileOpenDialogOptions option,
        bool enabled)
    {
        if (enabled)
        {
            options |= option;
        }
        else
        {
            options &= ~option;
        }
    }

    private static void Configure(
        IFileDialog dialog,
        FileDialogContext context,
        FileOpenDialogOptions requestedOptions)
    {
        if (context.CookieIdentifier != Guid.Empty)
        {
            var clientGuid = context.CookieIdentifier;
            ThrowIfFailed(dialog.SetClientGuid(ref clientGuid));
        }

        ThrowIfFailed(dialog.GetOptions(out var defaultOptions));
        const FileOpenDialogOptions controlledOptions =
            FileOpenDialogOptions.OverwritePrompt |
            FileOpenDialogOptions.StrictFileTypes |
            FileOpenDialogOptions.NoChangeDirectory |
            FileOpenDialogOptions.PickFolders |
            FileOpenDialogOptions.ForceFileSystem |
            FileOpenDialogOptions.NoValidate |
            FileOpenDialogOptions.AllowMultiSelect |
            FileOpenDialogOptions.PathMustExist |
            FileOpenDialogOptions.FileMustExist |
            FileOpenDialogOptions.CreatePrompt |
            FileOpenDialogOptions.NoReadOnlyReturn |
            FileOpenDialogOptions.NoTestFileCreate |
            FileOpenDialogOptions.HidePinnedPlaces |
            FileOpenDialogOptions.NoDereferenceLinks |
            FileOpenDialogOptions.DontAddToRecent |
            FileOpenDialogOptions.ForceShowHidden |
            FileOpenDialogOptions.DefaultNoMiniMode;
        ThrowIfFailed(dialog.SetOptions((defaultOptions & ~controlledOptions) | requestedOptions));

        if (context.Filters.Count > 0)
        {
            var filters = context.Filters.Select(filter => new FileDialogFilterSpec
            {
                Name = filter.DisplayName,
                Spec = string.Join(";", filter.Extensions.Select(extension => $"*.{extension}")),
            }).ToArray();
            ThrowIfFailed(dialog.SetFileTypes((uint)filters.Length, filters));
        }

        if (!string.IsNullOrEmpty(context.DefaultFileName))
        {
            ThrowIfFailed(dialog.SetFileName(context.DefaultFileName));
        }

        var defaultExtension = GetDefaultExtension(context);

        if (!string.IsNullOrEmpty(defaultExtension))
        {
            ThrowIfFailed(dialog.SetDefaultExtension(defaultExtension.TrimStart('*', '.')));
        }

        if (!string.IsNullOrEmpty(context.Title))
        {
            ThrowIfFailed(dialog.SetTitle(context.Title));
        }

        SetFolder(dialog, context.DefaultDirectory, useAsDefault: true);
        SetFolder(dialog, context.InitialDirectory, useAsDefault: false);

        foreach (var place in context.CustomPlaces)
        {
            IShellItem? item = null;
            try
            {
                item = CreateShellItem(place.Path);
                ThrowIfFailed(dialog.AddPlace(item, (FileDialogAddPlace)place.Location));
            }
            finally
            {
                ReleaseComObject(item);
            }
        }
    }

    private static string GetDefaultExtension(FileDialogContext context)
    {
        if (!string.IsNullOrEmpty(context.DefaultExtension))
        {
            return context.DefaultExtension.TrimStart('*', '.');
        }

        return context is SaveFileDialogContext && context.Filters.Count > 0
            ? context.Filters[0].Extensions.FirstOrDefault(extension => extension != "*") ?? string.Empty
            : string.Empty;
    }

    private static string AppendDefaultExtension(string fileName, string defaultExtension)
    {
        return string.IsNullOrEmpty(defaultExtension) || Path.HasExtension(fileName)
            ? fileName
            : $"{fileName}.{defaultExtension}";
    }

    private static void SetFolder(IFileDialog dialog, string path, bool useAsDefault)
    {
        if (string.IsNullOrEmpty(path))
        {
            return;
        }

        IShellItem? item = null;
        try
        {
            item = CreateShellItem(path);
            ThrowIfFailed(useAsDefault ? dialog.SetDefaultFolder(item) : dialog.SetFolder(item));
        }
        finally
        {
            ReleaseComObject(item);
        }
    }

    private static DialogResult Show(IFileDialog dialog)
    {
        var result = dialog.Show(GetOwnerHandle());
        if (result == NativeMethods.ErrorCancelled)
        {
            return DialogResult.Cancel;
        }

        ThrowIfFailed(result);
        return DialogResult.Ok;
    }

    private static nint GetOwnerHandle()
    {
        var activeWindow = Application.Current?.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive);
        return activeWindow is null ? IntPtr.Zero : new WindowInteropHelper(activeWindow).Handle;
    }

    private static string[] GetResults(IFileOpenDialog dialog, bool allowNonFileSystemItems)
    {
        IShellItemArray? results = null;
        try
        {
            ThrowIfFailed(dialog.GetResults(out results));
            ThrowIfFailed(results.GetCount(out var count));

            var paths = new string[count];
            for (uint index = 0; index < count; index++)
            {
                IShellItem? item = null;
                try
                {
                    ThrowIfFailed(results.GetItemAt(index, out item));
                    paths[index] = GetDisplayName(item, allowNonFileSystemItems);
                }
                finally
                {
                    ReleaseComObject(item);
                }
            }

            return paths;
        }
        finally
        {
            ReleaseComObject(results);
        }
    }

    private static string GetResult(IFileDialog dialog, bool allowNonFileSystemItems)
    {
        IShellItem? item = null;
        try
        {
            ThrowIfFailed(dialog.GetResult(out item));
            return GetDisplayName(item, allowNonFileSystemItems);
        }
        finally
        {
            ReleaseComObject(item);
        }
    }

    private static string GetDisplayName(IShellItem item, bool allowNonFileSystemItems)
    {
        var result = item.GetDisplayName(ShellItemDisplayName.FileSystemPath, out var displayName);
        if (result < 0 && allowNonFileSystemItems)
        {
            result = item.GetDisplayName(ShellItemDisplayName.DesktopAbsoluteParsing, out displayName);
        }

        ThrowIfFailed(result);
        try
        {
            return Marshal.PtrToStringUni(displayName) ?? string.Empty;
        }
        finally
        {
            Marshal.FreeCoTaskMem(displayName);
        }
    }

    private static IShellItem CreateShellItem(string path)
    {
        var interfaceId = typeof(IShellItem).GUID;
        ThrowIfFailed(NativeMethods.SHCreateItemFromParsingName(path, IntPtr.Zero, ref interfaceId, out var item));
        return item;
    }

    private static void ThrowIfFailed(int result)
    {
        if (result < 0)
        {
            Marshal.ThrowExceptionForHR(result);
        }
    }

    private static void ReleaseComObject(object? value)
    {
        if (value is not null && Marshal.IsComObject(value))
        {
            Marshal.FinalReleaseComObject(value);
        }
    }

    private static class NativeMethods
    {
        internal const int ErrorCancelled = unchecked((int)0x800704C7);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = true)]
        internal static extern int SHCreateItemFromParsingName(
            string path,
            nint bindContext,
            ref Guid interfaceId,
            [MarshalAs(UnmanagedType.Interface)] out IShellItem shellItem);
    }
}

[Flags]
internal enum FileOpenDialogOptions : uint
{
    None = 0,
    OverwritePrompt = 0x00000002,
    StrictFileTypes = 0x00000004,
    NoChangeDirectory = 0x00000008,
    PickFolders = 0x00000020,
    ForceFileSystem = 0x00000040,
    NoValidate = 0x00000100,
    AllowMultiSelect = 0x00000200,
    PathMustExist = 0x00000800,
    FileMustExist = 0x00001000,
    CreatePrompt = 0x00002000,
    NoReadOnlyReturn = 0x00008000,
    NoTestFileCreate = 0x00010000,
    HidePinnedPlaces = 0x00040000,
    NoDereferenceLinks = 0x00100000,
    DontAddToRecent = 0x02000000,
    ForceShowHidden = 0x10000000,
    DefaultNoMiniMode = 0x20000000,
}

internal enum FileDialogAddPlace
{
    Bottom = 0,
    Top = 1,
}

internal enum ShellItemDisplayName : uint
{
    DesktopAbsoluteParsing = 0x80028000,
    FileSystemPath = 0x80058000,
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct FileDialogFilterSpec
{
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Name;

    [MarshalAs(UnmanagedType.LPWStr)]
    public string Spec;
}

[ComImport]
[Guid("DC1C5A9C-E88A-4DDE-A5A1-60F82A20AEF7")]
internal class FileOpenDialogComObject
{
}

[ComImport]
[Guid("C0B4E2F3-BA21-4773-8DBA-335EC946EB8B")]
internal class FileSaveDialogComObject
{
}

[ComImport]
[Guid("42F85136-DB7E-439C-85F1-E4075D135FC8")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IFileDialog
{
    [PreserveSig]
    int Show(nint owner);

    [PreserveSig]
    int SetFileTypes(
        uint fileTypeCount,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] FileDialogFilterSpec[] filterSpec);

    [PreserveSig]
    int SetFileTypeIndex(uint fileTypeIndex);

    [PreserveSig]
    int GetFileTypeIndex(out uint fileTypeIndex);

    [PreserveSig]
    int Advise(nint events, out uint cookie);

    [PreserveSig]
    int Unadvise(uint cookie);

    [PreserveSig]
    int SetOptions(FileOpenDialogOptions options);

    [PreserveSig]
    int GetOptions(out FileOpenDialogOptions options);

    [PreserveSig]
    int SetDefaultFolder(IShellItem shellItem);

    [PreserveSig]
    int SetFolder(IShellItem shellItem);

    [PreserveSig]
    int GetFolder(out IShellItem shellItem);

    [PreserveSig]
    int GetCurrentSelection(out IShellItem shellItem);

    [PreserveSig]
    int SetFileName([MarshalAs(UnmanagedType.LPWStr)] string name);

    [PreserveSig]
    int GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string name);

    [PreserveSig]
    int SetTitle([MarshalAs(UnmanagedType.LPWStr)] string title);

    [PreserveSig]
    int SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] string text);

    [PreserveSig]
    int SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] string label);

    [PreserveSig]
    int GetResult(out IShellItem shellItem);

    [PreserveSig]
    int AddPlace(IShellItem shellItem, FileDialogAddPlace location);

    [PreserveSig]
    int SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] string defaultExtension);

    [PreserveSig]
    int Close(int result);

    [PreserveSig]
    int SetClientGuid(ref Guid clientGuid);

    [PreserveSig]
    int ClearClientData();

    [PreserveSig]
    int SetFilter(nint filter);
}

[ComImport]
[Guid("D57C7288-D4AD-4768-BE02-9D969532D960")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IFileOpenDialog : IFileDialog
{
    [PreserveSig]
    new int Show(nint owner);

    [PreserveSig]
    new int SetFileTypes(
        uint fileTypeCount,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] FileDialogFilterSpec[] filterSpec);

    [PreserveSig]
    new int SetFileTypeIndex(uint fileTypeIndex);

    [PreserveSig]
    new int GetFileTypeIndex(out uint fileTypeIndex);

    [PreserveSig]
    new int Advise(nint events, out uint cookie);

    [PreserveSig]
    new int Unadvise(uint cookie);

    [PreserveSig]
    new int SetOptions(FileOpenDialogOptions options);

    [PreserveSig]
    new int GetOptions(out FileOpenDialogOptions options);

    [PreserveSig]
    new int SetDefaultFolder(IShellItem shellItem);

    [PreserveSig]
    new int SetFolder(IShellItem shellItem);

    [PreserveSig]
    new int GetFolder(out IShellItem shellItem);

    [PreserveSig]
    new int GetCurrentSelection(out IShellItem shellItem);

    [PreserveSig]
    new int SetFileName([MarshalAs(UnmanagedType.LPWStr)] string name);

    [PreserveSig]
    new int GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string name);

    [PreserveSig]
    new int SetTitle([MarshalAs(UnmanagedType.LPWStr)] string title);

    [PreserveSig]
    new int SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] string text);

    [PreserveSig]
    new int SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] string label);

    [PreserveSig]
    new int GetResult(out IShellItem shellItem);

    [PreserveSig]
    new int AddPlace(IShellItem shellItem, FileDialogAddPlace location);

    [PreserveSig]
    new int SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] string defaultExtension);

    [PreserveSig]
    new int Close(int result);

    [PreserveSig]
    new int SetClientGuid(ref Guid clientGuid);

    [PreserveSig]
    new int ClearClientData();

    [PreserveSig]
    new int SetFilter(nint filter);


    [PreserveSig]
    int GetResults(out IShellItemArray shellItemArray);

    [PreserveSig]
    int GetSelectedItems(out IShellItemArray shellItemArray);
}

[ComImport]
[Guid("84BCCd23-5FDE-4CDB-AEA4-AF64B83D78AB")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IFileSaveDialog : IFileDialog
{
    [PreserveSig]
    new int Show(nint owner);

    [PreserveSig]
    new int SetFileTypes(
        uint fileTypeCount,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] FileDialogFilterSpec[] filterSpec);

    [PreserveSig]
    new int SetFileTypeIndex(uint fileTypeIndex);

    [PreserveSig]
    new int GetFileTypeIndex(out uint fileTypeIndex);

    [PreserveSig]
    new int Advise(nint events, out uint cookie);

    [PreserveSig]
    new int Unadvise(uint cookie);

    [PreserveSig]
    new int SetOptions(FileOpenDialogOptions options);

    [PreserveSig]
    new int GetOptions(out FileOpenDialogOptions options);

    [PreserveSig]
    new int SetDefaultFolder(IShellItem shellItem);

    [PreserveSig]
    new int SetFolder(IShellItem shellItem);

    [PreserveSig]
    new int GetFolder(out IShellItem shellItem);

    [PreserveSig]
    new int GetCurrentSelection(out IShellItem shellItem);

    [PreserveSig]
    new int SetFileName([MarshalAs(UnmanagedType.LPWStr)] string name);

    [PreserveSig]
    new int GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string name);

    [PreserveSig]
    new int SetTitle([MarshalAs(UnmanagedType.LPWStr)] string title);

    [PreserveSig]
    new int SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] string text);

    [PreserveSig]
    new int SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] string label);

    [PreserveSig]
    new int GetResult(out IShellItem shellItem);

    [PreserveSig]
    new int AddPlace(IShellItem shellItem, FileDialogAddPlace location);

    [PreserveSig]
    new int SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] string defaultExtension);

    [PreserveSig]
    new int Close(int result);

    [PreserveSig]
    new int SetClientGuid(ref Guid clientGuid);

    [PreserveSig]
    new int ClearClientData();

    [PreserveSig]
    new int SetFilter(nint filter);


    [PreserveSig]
    int SetSaveAsItem(IShellItem shellItem);

    [PreserveSig]
    int SetProperties(nint propertyStore);

    [PreserveSig]
    int SetCollectedProperties(nint propertyDescriptionList, [MarshalAs(UnmanagedType.Bool)] bool appendDefault);

    [PreserveSig]
    int GetProperties(out nint propertyStore);

    [PreserveSig]
    int ApplyProperties(IShellItem shellItem, nint propertyStore, nint owner, nint progressSink);
}

[ComImport]
[Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IShellItem
{
    [PreserveSig]
    int BindToHandler(nint bindContext, ref Guid handlerId, ref Guid interfaceId, out nint result);

    [PreserveSig]
    int GetParent(out IShellItem parent);

    [PreserveSig]
    int GetDisplayName(ShellItemDisplayName displayName, out nint name);

    [PreserveSig]
    int GetAttributes(uint requestedAttributes, out uint attributes);

    [PreserveSig]
    int Compare(IShellItem shellItem, uint hint, out int order);
}

[ComImport]
[Guid("B63EA76D-1F85-456F-A19C-48159EFA858B")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IShellItemArray
{
    [PreserveSig]
    int BindToHandler(nint bindContext, ref Guid handlerId, ref Guid interfaceId, out nint result);

    [PreserveSig]
    int GetPropertyStore(int flags, ref Guid interfaceId, out nint propertyStore);

    [PreserveSig]
    int GetPropertyDescriptionList(ref Guid propertyKey, ref Guid interfaceId, out nint propertyDescriptionList);

    [PreserveSig]
    int GetAttributes(uint attributeFlags, uint requestedAttributes, out uint attributes);

    [PreserveSig]
    int GetCount(out uint count);

    [PreserveSig]
    int GetItemAt(uint index, out IShellItem shellItem);

    [PreserveSig]
    int EnumItems(out nint enumShellItems);
}
