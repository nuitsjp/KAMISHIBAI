namespace Kamishibai;

public class OpenFileContext
{
    public bool AddExtension { get; set; } = true;
    public bool CheckFileExists { get; set; }
    public bool CheckPathExists { get; set; } = true;
}