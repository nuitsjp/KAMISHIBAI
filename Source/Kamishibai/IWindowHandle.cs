namespace Kamishibai;

/// <summary>
/// WindowHandle is a wrapper class for Window.
/// </summary>
public interface IWindowHandle : IDisposable
{
    /// <summary>
    /// Close window.
    /// </summary>
    void Close();
}
