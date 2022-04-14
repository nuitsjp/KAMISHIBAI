﻿namespace Kamishibai.Wpf;

public class ResumingEventArgs : EventArgs
{
    public ResumingEventArgs(object sourceViewModel, object destinationViewModel)
    {
        SourceViewModel = sourceViewModel;
        DestinationViewModel = destinationViewModel;
    }

    public object SourceViewModel { get; }
    public object DestinationViewModel { get; }
}