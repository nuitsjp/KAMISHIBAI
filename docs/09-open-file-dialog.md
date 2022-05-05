---
title: "ファイル選択ダイアログ"
---

KAMISHIBAIでは、つぎの3つをサポートしています。

1. 単一ファイル選択ダイアログ
2. 複数ファイル選択ダイアログ
3. フォルダー選択ダイアログ

# 単一ファイル選択ダイアログ

IPresentationServiceのOpenFileメソッドにOpenFileDialogContextを渡します。

```cs
var context = new OpenFileDialogContext
{
    Title = "画像を選択してください"
};
context.Filters.Add(new FileDialogFilter("Image", "png", "jpg"));
context.Filters.Add(new FileDialogFilter("All files", "*"));
if (_presentationService.OpenFile(context) == DialogResult.Ok)
{
    var file = context.FileName;
    ...
}
```

選択されたファイルのパスはcontextのFileNameプロパティから取得します。

詳細は下記のAPIドキュメントを参照してください。

- [OpenFileDialogContext](https://nuitsjp.github.io/KAMISHIBAI/class_open_file_dialog_context.html#aa1b7a88cbb1cc7cdda0fda274d62266a)
- [FileDialogContext(OpenFileDialogContextの基底クラス)](https://nuitsjp.github.io/KAMISHIBAI/class_file_dialog_context.html)

# 複数ファイル選択ダイアログ

OpenFileDialogContextのMultiselectにtrueを設定します。

```cs
var context = new OpenFileDialogContext
{
    Title = "画像を選択してください",
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

選択されたファイルのパスはcontextのFileNamesプロパティから取得します。


# フォルダー選択ダイアログ

OpenFileDialogContextのIsFolderPickerにtrueを設定します。

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

選択されたフォルダーのパスはcontextのFileNameプロパティから取得します。
