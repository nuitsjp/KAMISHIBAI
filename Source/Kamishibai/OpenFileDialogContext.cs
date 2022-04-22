namespace Kamishibai;

public class OpenFileDialogContext : FileDialogContext
{
    /// <summary>
    /// Gets or sets a value that determines whether the user can select non-filesystem items, such as <b>Library</b>, <b>Search
    /// Connectors</b>, or <b>Known Folders</b>.
    /// </summary>
    public bool AllowNonFileSystemItems { get; set; } = false;

    /// <summary>Gets a collection of the selected file names.</summary>
    /// <remarks>This property should only be used when the <see cref="OpenFileDialogContext.Multiselect"/> property is <b>true</b>.</remarks>
    public IEnumerable<string> FileNames { get; set; } = Array.Empty<string>();

    /// <summary>Gets or sets a value that determines whether the user can select folders or files. Default value is false.</summary>
    public bool IsFolderPicker { get; set; }

    /// <summary>Gets or sets a value that determines whether the user can select more than one file.</summary>
    public bool Multiselect { get; set; } = false;
}