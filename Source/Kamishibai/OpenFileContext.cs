namespace Kamishibai;

public class OpenFileContext
{
    public bool AddExtension { get; set; } = true;
    public bool CheckFileExists { get; set; } = true;
    public bool CheckPathExists { get; set; } = true;
    public string DefaultExt { get; set; } = string.Empty;
    public bool DereferenceLinks { get; set; } = true;
    public string Filter { get; set; } = string.Empty;
    public int FilterIndex { get; set; } = 1;
    public string InitialDirectory { get; set; } = string.Empty;
    public bool ReadOnlyChecked { get; set; } = false;
    public bool ShowReadOnly { get; set; } = false;
    public string SafeFileName { get; set; } = string.Empty;
    public string[] SafeFileNames { get; set; } = Array.Empty<string>();
    public string Title { get; set; } = string.Empty;
    public bool ValidateNames { get; set; } = true;
}