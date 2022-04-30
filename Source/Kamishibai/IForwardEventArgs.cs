namespace Kamishibai;

public interface IForwardEventArgs
{
    string? FrameName { get; }
    object? SourceViewModel { get; }
    object DestinationViewModel { get; }
}