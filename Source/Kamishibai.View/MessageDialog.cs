using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Kamishibai;

public class MessageDialog
{
    // ReSharper disable IdentifierTypo
    // ReSharper disable InconsistentNaming
    private const int WH_CALLWNDPROCRET = 12;
    private const int HCBT_ACTIVATE = 5;
    // ReSharper restore InconsistentNaming
    // ReSharper restore IdentifierTypo

    private readonly Window? _ownerWindow;
    private IntPtr _hHook;

    public MessageDialog()
    {
        _ownerWindow = null;
        _hHook = IntPtr.Zero;
    }

    public MessageDialog(Window ownerWindow)
    {
        this._ownerWindow = ownerWindow;
        _hHook = IntPtr.Zero;
    }

    public string Text { get; set; } = string.Empty;
    public string Caption { get; set; } = string.Empty;
    public MessageBoxButton Buttons { get; set; } = MessageBoxButton.OK;
    public MessageBoxImage Icon { get; set; } = MessageBoxImage.None;
    public MessageBoxResult DefaultResult { get; set; } = MessageBoxResult.None;
    public MessageBoxOptions Options { get; set; } = MessageBoxOptions.None;

    public System.Windows.MessageBoxResult Show()
    {
        if (_ownerWindow is null)
        {
            return MessageBox.Show(
                Text, 
                Caption, 
                (System.Windows.MessageBoxButton)Buttons, 
                (System.Windows.MessageBoxImage)Icon, 
                (System.Windows.MessageBoxResult)DefaultResult, 
                (System.Windows.MessageBoxOptions)Options);
        }

        _hHook = NativeMethods.SetWindowsHookEx(WH_CALLWNDPROCRET, MessageBoxHookProc, IntPtr.Zero, NativeMethods.GetCurrentThreadId());
        return MessageBox.Show(
            _ownerWindow,
            Text,
            Caption,
            (System.Windows.MessageBoxButton)Buttons,
            (System.Windows.MessageBoxImage)Icon,
            (System.Windows.MessageBoxResult)DefaultResult,
            (System.Windows.MessageBoxOptions)Options);

    }

    private IntPtr MessageBoxHookProc(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode < 0)
        {
            return NativeMethods.CallNextHookEx(_hHook, nCode, wParam, lParam);
        }

        var message = (CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(CWPRETSTRUCT))!;
        var hook = _hHook;

        if (message.Message == HCBT_ACTIVATE)
        {
            try
            {
                var owner = NativeMethods.GetWindowRectangle(new WindowInteropHelper(_ownerWindow!).Handle);
                var dialog = NativeMethods.GetWindowRectangle(message.WindowHandle);

                var x = owner.Left + (owner.Width - dialog.Width) / 2;
                var y = owner.Top + (owner.Height - dialog.Height) / 2;
                NativeMethods.SetWindowPos(message.WindowHandle, x, y);
            }
            finally
            {
                NativeMethods.UnhookWindowsHookEx(_hHook);
                _hHook = IntPtr.Zero;
            }
        }

        return NativeMethods.CallNextHookEx(hook, nCode, wParam, lParam);
    }

    // ReSharper disable IdentifierTypo
    // ReSharper disable InconsistentNaming
    [StructLayout(LayoutKind.Sequential)]
    internal struct CWPRETSTRUCT
    {
        internal IntPtr lResult;
        internal IntPtr lParam;
        internal IntPtr WParam;
        internal uint Message;
        internal IntPtr WindowHandle;
    };
    // ReSharper restore InconsistentNaming
    // ReSharper restore IdentifierTypo
}