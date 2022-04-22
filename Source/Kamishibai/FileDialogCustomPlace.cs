namespace Kamishibai;

public class FileDialogCustomPlace
{
    public FileDialogCustomPlace(string path, FileDialogAddPlaceLocation location)
    {
        Path = path;
        Location = location;
    }
    public FileDialogCustomPlace(Environment.SpecialFolder specialFolder, FileDialogAddPlaceLocation location)
    {
        Location = location;
        Path = Environment.GetFolderPath(specialFolder);
    }

    public string Path { get; }
    public FileDialogAddPlaceLocation Location { get; }
}