namespace Kamishibai;

public interface IBackwardEventArgs
{
    string? FrameName { get; }
    object SourceViewModel { get; }
    object? DestinationViewModel { get; }
}