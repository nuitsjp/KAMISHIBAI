using System.Windows;

namespace Kamishibai;

/// <summary>
/// WindowHandle is a wrapper class for <see cref="Window"/> to implement <see cref="IWindowHandle"/>.
/// </summary>
public class WindowHandle : IWindowHandle
{
    /// <summary>
    /// Window instance.
    /// </summary>
    private readonly Window _window;

    /// <summary>
    /// Window is closed.
    /// </summary>
    private bool _closed;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="window"></param>
    public WindowHandle(Window window)
    {
        _window = window;
        _window.Closed += OnClosed;
    }

    /// <summary>
    /// Window is closed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnClosed(object sender, EventArgs e)
    {
        _closed = true;
    }


    /// <summary>
    /// Dispose.
    /// </summary>
    public void Dispose()
    {
        Close();
    }

    /// <summary>
    /// Close window.
    /// </summary>
    public void Close()
    {
        if (_closed)
        {
            return;
        }

        _window.Close();
    }
}