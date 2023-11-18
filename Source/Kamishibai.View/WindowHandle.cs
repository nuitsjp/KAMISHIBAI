using System.Windows;

namespace Kamishibai;

/// <summary>
/// WindowHandle is a wrapper class for <see cref="Window"/> to implement <see cref="IWindow"/>.
/// </summary>
public class WindowHandle : IWindow
{
    /// <summary>
    /// Window instance.
    /// </summary>
    private readonly Window _window;

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
    private void OnClosed(object? sender, EventArgs e)
    {
        IsClosed = true;
    }


    /// <summary>
    /// Dispose.
    /// </summary>
    public void Dispose()
    {
        Close();
    }

    /// <summary>
    /// Window is closed.
    /// </summary>
    public bool IsClosed { get; private set; }

    /// <summary>
    /// Close window.
    /// </summary>
    public void Close()
    {
        if (IsClosed)
        {
            return;
        }

        _window.Close();
    }
}