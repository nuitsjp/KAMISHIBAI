using System.ComponentModel;

namespace Kamishibai;

/// <summary>
/// WindowHandle is a wrapper class for Window.
/// </summary>
public interface IWindow : IDisposable
{
    /// <summary>
    /// Activated event.
    /// </summary>
    event EventHandler? Activated;

    /// <summary>
    /// Closed event.
    /// </summary>
    event EventHandler? Closed;

    /// <summary>
    /// Closing event.
    /// </summary>
    event EventHandler<CancelEventArgs> Closing;

    /// <summary>
    /// WindowState is changed.
    /// </summary>
    event EventHandler StateChanged;
    /// <summary>
    /// Windows is closed.
    /// </summary>
    bool IsClosed { get; }

    /// <summary>
    /// WindowState.
    /// </summary>
    WindowState WindowState { get; }

    /// <summary>
    /// Close window.
    /// </summary>
    void Close();

    /// <summary>
    /// Show window.
    /// </summary>
    void Show();

    /// <summary>
    /// Hide window.
    /// </summary>
    void Hide();

    /// <summary>
    /// Activate window.
    /// </summary>
    void Activate();

    /// <summary>
    /// Minimize window.
    /// </summary>
    void Minimize();

    /// <summary>
    /// Maximize window.
    /// </summary>
    void Maximize();

    /// <summary>
    /// Restore window.
    /// </summary>
    void Restore();
}