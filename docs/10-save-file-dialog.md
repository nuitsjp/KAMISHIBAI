---
title: "ファイル保存ダイアログ"
---

IPresentationServiceのSaveFileメソッドにSaveFileDialogContextを渡します。

```cs
var context = new SaveFileDialogContext()
{
    Title = "画像を選択してください",
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

選択されたファイルのパスはcontextのFileNameプロパティから取得します。

詳細は下記のAPIドキュメントを参照してください。

- [SaveFileDialogContext](https://nuitsjp.github.io/KAMISHIBAI/class_save_file_dialog_context.html#af09f75b40487c857b4c2dd81e26f9f9a)
- [FileDialogContext(SaveFileDialogContextの基底クラス)](https://nuitsjp.github.io/KAMISHIBAI/class_file_dialog_context.html)

