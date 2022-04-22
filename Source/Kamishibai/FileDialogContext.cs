namespace Kamishibai;

public class FileDialogContext
{
    /// <summary>
    /// Gets or sets a value that controls whether to show or hide the list of places where the user has recently opened or saved items.
    /// </summary>
    /// <value>A <see cref="System.Boolean"/> value.</value>
    /// <exception cref="System.InvalidOperationException">This property cannot be set when the dialog is visible.</exception>
    public bool AddToMostRecentlyUsedList { get; set; } = true;

    /// <summary>Gets or sets a value that controls whether properties can be edited.</summary>
    /// <value>A <see cref="System.Boolean"/> value.</value>
    public bool AllowPropertyEditing { get; set; } = false;

    /// <summary>Gets or sets a value that enables a calling application to associate a GUID with a dialog's persisted state.</summary>
    public Guid CookieIdentifier { get; set; } = Guid.Empty;

    /// <summary>Sets the folder and path used as a default if there is not a recently used folder value available.</summary>
    public string DefaultDirectory { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the default file extension to be added to file names. If the value is string.Empty, the extension is not
    /// added to the file names.
    /// </summary>
    public string DefaultExtension { get; set; } = string.Empty;

    /// <summary>Default file name.</summary>
    public string DefaultFileName { get; set; } = string.Empty;

    /// <summary>Gets or sets a value that determines whether the file must exist beforehand.</summary>
    /// <value>A <see cref="System.Boolean"/> value. <b>true</b> if the file must exist.</value>
    /// <exception cref="System.InvalidOperationException">This property cannot be set when the dialog is visible.</exception>
    public virtual bool EnsureFileExists { get; set; } = true;

    /// <summary>Gets or sets a value that specifies whether the returned file must be in an existing folder.</summary>
    /// <value>A <see cref="System.Boolean"/> value. <b>true</b> if the file must exist.</value>
    /// <exception cref="System.InvalidOperationException">This property cannot be set when the dialog is visible.</exception>
    public bool EnsurePathExists { get; set; } = true;

    /// <summary>
    /// Gets or sets a value that determines whether read-only items are returned. Default value for CommonOpenFileDialog is true (allow
    /// read-only files) and CommonSaveFileDialog is false (don't allow read-only files).
    /// </summary>
    /// <value>A <see cref="System.Boolean"/> value. <b>true</b> includes read-only items.</value>
    /// <exception cref="System.InvalidOperationException">This property cannot be set when the dialog is visible.</exception>
    public bool EnsureReadOnly { get; set; } = true;

    /// <summary>Gets or sets a value that determines whether to validate file names.
    /// </summary>
    ///<value>A <see cref="System.Boolean"/> value. <b>true </b>to check for situations that would prevent an application from opening the selected file, such as sharing violations or access denied errors.</value>
    /// <exception cref="System.InvalidOperationException">This property cannot be set when the dialog is visible.</exception>
    public bool EnsureValidNames { get; set; } = false;

    /// <summary>Gets the selected filename.</summary>
    /// <value>A <see cref="System.String"/> object.</value>
    public string FileName { get; set; } = string.Empty;

    /// <summary>Gets the filters used by the dialog.</summary>
    public IList<FileDialogFilter> Filters { get; set; } = new List<FileDialogFilter>();

    /// <summary>
    /// Gets or sets the initial directory displayed when the dialog is shown. A null or empty string indicates that the dialog is using
    /// the default directory.
    /// </summary>
    /// <value>A <see cref="System.String"/> object.</value>
    public string InitialDirectory { get; set; } = string.Empty;

    ///<summary>
    /// Gets or sets a value that controls whether shortcuts should be treated as their target items, allowing an application to open a .lnk file.
    /// </summary>
    /// <value>A <see cref="System.Boolean"/> value. <b>true</b> indicates that shortcuts should be treated as their targets. </value>
    /// <exception cref="System.InvalidOperationException">This property cannot be set when the dialog is visible.</exception>
    public bool NavigateToShortcut { get; set; } = true;

    /// <summary>Gets or sets a value that determines the restore directory.</summary>
    /// <remarks></remarks>
    /// <exception cref="System.InvalidOperationException">This property cannot be set when the dialog is visible.</exception>
    public bool RestoreDirectory { get; set; }

    ///<summary>
    /// Gets or sets a value that controls whether to show hidden items.
    /// </summary>
    /// <value>A <see cref="System.Boolean"/> value.<b>true</b> to show the items; otherwise <b>false</b>.</value>
    /// <exception cref="System.InvalidOperationException">This property cannot be set when the dialog is visible.</exception>
    public bool ShowHiddenItems { get; set; }

    /// <summary>Gets or sets a value that controls whether to show or hide the list of pinned places that the user can choose.</summary>
    /// <value>A <see cref="System.Boolean"/> value. <b>true</b> if the list is visible; otherwise <b>false</b>.</value>
    /// <exception cref="System.InvalidOperationException">This property cannot be set when the dialog is visible.</exception>
    public bool ShowPlacesList { get; set; } = true;

    /// <summary>Gets or sets the dialog title.</summary>
    /// <value>A <see cref="System.String"/> object.</value>
    public string Title { get; set; } = string.Empty;

    /// <summary>An item in the "Place" section to add to the list of locations where users can open or save items. </summary>
    public IList<FileDialogCustomPlace> CustomPlaces { get; set; } = new List<FileDialogCustomPlace>();
}