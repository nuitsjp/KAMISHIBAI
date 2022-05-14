# Save File Dialog

Pass the SaveFileDialogContext to the SaveFile method of the IPresentationService.

```cs
var context = new SaveFileDialogContext()
{
    Title = "Please select an image",
    DefaultExtension = "*.png",
    DefaultFileName = "default.png"
};
context.Filters.Add(new FileDialogFilter("Image", "png", "jpg"));
context.Filters.Add(new FileDialogFilter("All files", "*"));
if (_presentationService.SaveFile(context) == DialogResult.Ok)
{
    var file = context.FileName;
    ...
}
```

The path to the selected file is obtained from the context's FileName property.

See the API document below for details.

- [SaveFileDialogContext](https://nuitsjp.github.io/KAMISHIBAI/Api/class_save_file_dialog_context.html#af09f75b40487c857b4c2dd81e26f9f9a)
- [FileDialogContext(Base class for SaveFileDialogContext)](https://nuitsjp.github.io/KAMISHIBAI/Api/class_file_dialog_context.html)

[<< Open File Dialog](09-open-file-dialog.md) | [Menu](01-table-of-contents.md) | [Animation of navigation >>](11-animation-of-navigation.md)