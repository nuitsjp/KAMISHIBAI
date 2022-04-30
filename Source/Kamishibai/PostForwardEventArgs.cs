namespace Kamishibai;

public class PostForwardEventArgs : EventArgs, IForwardEventArgs
{
    public PostForwardEventArgs(string? frameName, object? sourceViewModel, object destinationViewModel)
    {
        FrameName = frameName;
        SourceViewModel = sourceViewModel;
        DestinationViewModel = destinationViewModel;
    }

    public string? FrameName { get; }
    public object? SourceViewModel { get; }
    public object DestinationViewModel { get; }
}