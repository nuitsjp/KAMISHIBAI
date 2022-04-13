namespace Kamishibai.Wpf;

public class DisposedEventArgs : EventArgs
{
    public DisposedEventArgs(object sourceViewModel, object destinationViewModel)
    {
        SourceViewModel = sourceViewModel;
        DestinationViewModel = destinationViewModel;
    }

    public object SourceViewModel { get; }
    public object DestinationViewModel { get; }
}