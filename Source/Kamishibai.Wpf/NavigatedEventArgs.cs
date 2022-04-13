namespace Kamishibai.Wpf;

public class NavigatedEventArgs : EventArgs
{
    public NavigatedEventArgs(object sourceViewModel, object destinationViewModel)
    {
        SourceViewModel = sourceViewModel;
        DestinationViewModel = destinationViewModel;
    }

    public object SourceViewModel { get; }
    public object DestinationViewModel { get; }
}