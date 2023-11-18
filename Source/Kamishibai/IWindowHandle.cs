namespace Kamishibai;

/// <summary>
/// WindowHandle is a wrapper class for Window.
/// </summary>
public interface IWindowHandle : IDisposable
{
    /// <summary>
    /// Windows is closed.
    /// </summary>
    bool IsClosed { get; }

    /// <summary>
    /// Close window.
    /// </summary>
    void Close();
}
