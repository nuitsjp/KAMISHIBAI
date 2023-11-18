using System.ComponentModel;
using System.Windows;

namespace Kamishibai;

/// <summary>
/// WindowHandle is a wrapper class for <see cref="Window"/> to implement <see cref="IWindow"/>.
/// </summary>
public class WindowHandle : IWindow
{
    /// <summary>
    /// Activated event.
    /// </summary>
    public event EventHandler? Activated;

    /// <summary>
    /// Closed event.
    /// </summary>
    public event EventHandler? Closed;

    /// <summary>
    /// Closing event.
    /// </summary>
    public event EventHandler<CancelEventArgs>? Closing;

    /// <summary>
    /// WindowState changed event.
    /// </summary>
    public event EventHandler? StateChanged;

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
        _window.Closing += OnClosing;
        _window.Closed += OnClosed;
        _window.Activated += OnActivated;
        _window.StateChanged += OnStateChanged;
    }

    /// <summary>
    /// WindowState changed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnStateChanged(object? sender, EventArgs e)
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Window is activated.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnActivated(object? sender, EventArgs e)
    {
        Activated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Window is closing.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnClosing(object? sender, CancelEventArgs e)
    {
        Closing?.Invoke(this, e);
    }

    /// <summary>
    /// Window is closed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnClosed(object? sender, EventArgs e)
    {
        Closed?.Invoke(this, EventArgs.Empty);
        IsClosed = true;
    }


    /// <summary>
    /// Dispose.
    /// </summary>
    public void Dispose()
    {
        Close();
        _window.Activated -= OnActivated;
        _window.Closing -= OnClosing;
        _window.Closed -= OnClosed;
        _window.StateChanged -= OnStateChanged;
    }

    /// <summary>
    /// Window is closed.
    /// </summary>
    public bool IsClosed { get; private set; }

    /// <summary>
    /// WindowState.
    /// </summary>
    public WindowState WindowState => (WindowState)_window.WindowState;

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

    /// <summary>
    /// Show window.
    /// </summary>
    public void Show() => _window.Show();

    /// <summary>
    /// Hide window.
    /// </summary>
    public void Hide() => _window.Hide();

    /// <summary>
    /// Activate window.
    /// </summary>
    public void Activate() => _window.Activate();

    /// <summary>
    /// Minimize window.
    /// </summary>
    public void Minimize() => _window.WindowState = System.Windows.WindowState.Minimized;

    /// <summary>
    /// Maximize window.
    /// </summary>
    public void Maximize() => _window.WindowState = System.Windows.WindowState.Maximized;

    /// <summary>
    /// Restore window.
    /// </summary>
    public void Restore() => _window.WindowState = System.Windows.WindowState.Normal;
}