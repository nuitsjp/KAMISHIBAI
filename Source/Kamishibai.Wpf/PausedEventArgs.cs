namespace Kamishibai.Wpf;

public class PausedEventArgs : EventArgs
{
    public PausedEventArgs(object sourceViewModel, object destinationViewModel)
    {
        SourceViewModel = sourceViewModel;
        DestinationViewModel = destinationViewModel;
    }

    public object SourceViewModel { get; }
    public object DestinationViewModel { get; }
}