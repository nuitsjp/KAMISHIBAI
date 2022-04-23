using System.Runtime.InteropServices;

namespace Kamishibai;

internal static class NativeMethods
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ArrangeTypeMemberModifiers
    // ReSharper disable IdentifierTypo
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOZORDER = 0x0004;
    const uint SWP_NOACTIVATE = 0x0010;
    // ReSharper restore IdentifierTypo
    // ReSharper restore ArrangeTypeMemberModifiers
    // ReSharper restore InconsistentNaming

    public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

    internal struct Rectangle
    {
        public int Left, Top, Right, Bottom;
        public int Width => this.Right - this.Left;
        public int Height => this.Bottom - this.Top;
    }

    [DllImport("kernel32.dll")]
    internal static extern IntPtr GetCurrentThreadId();

    [DllImport("user32.dll")]
    internal static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);

    internal static Rectangle GetWindowRectangle(IntPtr hWnd)
    {
        GetWindowRect(hWnd, out var rectangle);
        return rectangle;
    }

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    public static bool SetWindowPos(IntPtr hWnd, int x, int y)
    {
        var flags = SWP_NOSIZE | SWP_NOZORDER | SWP_NOACTIVATE;
        return SetWindowPos(hWnd, 0, x, y, 0, 0, flags);
    }

    [DllImport("user32.dll")]
    internal static extern IntPtr SetWindowsHookEx(int idHook, HookProc hookProc, IntPtr hInstance, IntPtr threadId);

    [DllImport("user32.dll")]
    internal static extern int UnhookWindowsHookEx(IntPtr idHook);

    [DllImport("user32.dll")]
    internal static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

}