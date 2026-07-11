namespace Kamishibai;

public interface IFileDialogService
{
    DialogResult OpenFile(OpenFileDialogContext context);
    DialogResult SaveFile(SaveFileDialogContext context);
}
