# Open File Dialog

KAMISHIBAI supports the following three types of dialogs

1. single file selection dialog
2. multiple file selection dialog
3. folder selection dialog

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