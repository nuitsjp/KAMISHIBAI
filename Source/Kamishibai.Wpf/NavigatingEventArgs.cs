namespace Kamishibai.Wpf;

public class NavigatingEventArgs : EventArgs
{
    public NavigatingEventArgs(object? sourceViewModel, object destinationViewModel)
    {
        SourceViewModel = sourceViewModel;
        DestinationViewModel = destinationViewModel;
    }

    public object? SourceViewModel { get; }
    public object DestinationViewModel { get; }
}