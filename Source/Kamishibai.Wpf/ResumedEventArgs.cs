namespace Kamishibai.Wpf;

public class ResumedEventArgs : EventArgs
{
    public ResumedEventArgs(object sourceViewModel, object destinationViewModel)
    {
        SourceViewModel = sourceViewModel;
        DestinationViewModel = destinationViewModel;
    }

    public object SourceViewModel { get; }
    public object DestinationViewModel { get; }
}