namespace Kamishibai.Wpf;

public class DisposingEventArgs : EventArgs
{
    public DisposingEventArgs(object sourceViewModel, object destinationViewModel)
    {
        SourceViewModel = sourceViewModel;
        DestinationViewModel = destinationViewModel;
    }

    public object SourceViewModel { get; }
    public object DestinationViewModel { get; }
}