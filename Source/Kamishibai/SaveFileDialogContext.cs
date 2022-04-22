namespace Kamishibai;

public class SaveFileDialogContext : FileDialogContext
{
    /// <summary>
    /// Gets or sets a value that controls whether the returned file name has a file extension that matches the currently selected file
    /// type. If necessary, the dialog appends the correct file extension.
    /// </summary>
    /// <permission cref="System.InvalidOperationException">This property cannot be changed when the dialog is showing.</permission>
    public bool AlwaysAppendDefaultExtension { get; set; }

    /// <summary>
    /// Gets or sets a value that controls whether to prompt for creation if the item returned in the save dialog does not exist.
    /// </summary>
    /// <remarks>Note that this does not actually create the item.</remarks>
    /// <permission cref="System.InvalidOperationException">This property cannot be changed when the dialog is showing.</permission>
    public bool CreatePrompt { get; set; }

    /// <summary>Gets or sets a value that controls whether to the save dialog displays in expanded mode.</summary>
    /// <remarks>Expanded mode controls whether the dialog shows folders for browsing or hides them.</remarks>
    /// <permission cref="System.InvalidOperationException">This property cannot be changed when the dialog is showing.</permission>
    public bool IsExpandedMode { get; set; }

    /// <summary>
    /// Gets or sets a value that controls whether to prompt before overwriting an existing file of the same name. Default value is true.
    /// </summary>
    /// <permission cref="System.InvalidOperationException">This property cannot be changed when the dialog is showing.</permission>
    public bool OverwritePrompt { get; set; }

}