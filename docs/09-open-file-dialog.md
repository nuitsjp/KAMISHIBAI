# Open File Dialog

KAMISHIBAI supports the following three types of dialogs

1. single file selection dialog
2. multiple file selection dialog
3. folder selection dialog

KAMISHIBAI 4 uses a direct Win32 Common Item Dialog implementation by default. To preserve the file dialog implementation and behavior from KAMISHIBAI 3.x, install `Kamishibai.FileDialogs.WindowsApiCodePack` and register it before building the application.

```cs
builder.Services.AddWindowsApiCodePackFileDialogs();
var app = builder.Build();
```

The `IPresentationService`, `IWindowService`, and dialog context APIs are unchanged. Applications can also replace `IFileDialogService` with their own implementation through dependency injection.

The direct Win32 implementation does not apply `AllowPropertyEditing` because the public Common Item Dialog API has no corresponding switch. `IsExpandedMode` is mapped to `FOS_DEFAULTNOMINIMODE`, which Windows 7 and later do not support. Use the compatibility package when the KAMISHIBAI 3.x behavior of these options is required.

See [Migrating from KAMISHIBAI 3.x to 4.0](12-migrating-to-4.0.md) for all compatibility considerations.

# Single File Selection Dialog

Pass OpenFileDialogContext to the OpenFile method of IPresentationService.

```cs
var context = new OpenFileDialogContext
{
    Title = "Please select an image"
};
context.Filters.Add(new FileDialogFilter("Image", "png", "jpg"));
context.Filters.Add(new FileDialogFilter("All files", "*"));
if (_presentationService.OpenFile(context) == DialogResult.Ok)
{
    var file = context.FileName;
    ...
}
```

The path to the selected file is obtained from the context's FileName property.

See the API document below for details.

- [OpenFileDialogContext](https://nuitsjp.github.io/KAMISHIBAI/Api/class_open_file_dialog_context.html#aa1b7a88cbb1cc7cdda0fda274d62266a)
- [FileDialogContext(Base class for OpenFileDialogContext)](https://nuitsjp.github.io/KAMISHIBAI/Api/class_file_dialog_context.html)

# Multiple File Selection Dialog

Set Multiselect to true in OpenFileDialogContext.

```cs
var context = new OpenFileDialogContext
{
    Title = "Please select an image",
    Multiselect = true
};
context.Filters.Add(new FileDialogFilter("Image", "png", "jpg"));
context.Filters.Add(new FileDialogFilter("All files", "*"));
if (_presentationService.OpenFile(context) == DialogResult.Ok)
{
    var files = context.FileNames;
    ...
}
```

The path of the selected files is obtained from the context's FileNames property.


# Folder Selection Dialog

Set true to IsFolderPicker in OpenFileDialogContext.

```cs
var context = new OpenFileDialogContext
{
    IsFolderPicker = true
};
if (_presentationService.OpenFile(context) == DialogResult.Ok)
{
    var file = context.FileName;
    ...
}
```

The path to the selected folder is obtained from the context's FileName property.

[<< Message Dialog](08-message-dialog.md) | [Menu](01-table-of-contents.md) | [Save File Dialog >>](10-save-file-dialog.md)
