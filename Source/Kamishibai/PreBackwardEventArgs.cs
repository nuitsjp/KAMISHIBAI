namespace Kamishibai;

public class PreBackwardEventArgs : EventArgs, IBackwardEventArgs
{
    public PreBackwardEventArgs(string? frameName, object sourceViewModel, object? destinationViewModel)
    {
        FrameName = frameName;
        SourceViewModel = sourceViewModel;
        DestinationViewModel = destinationViewModel;
    }

    public string? FrameName { get; }
    public object SourceViewModel { get; }
    public object? DestinationViewModel { get; }
    public bool Cancel { get; set; }
}