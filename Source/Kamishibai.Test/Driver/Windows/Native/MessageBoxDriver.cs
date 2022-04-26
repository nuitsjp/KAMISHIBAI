using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.Runtime.InteropServices;

namespace Driver.Windows.Native
{
    public class MessageBoxDriver
    {
        public WindowControl Core { get; }

        public MessageBoxDriver(WindowControl core) { Core = core; }
        public string Message => Core.IdentifyFromWindowClass("Static").GetWindowText();
        public NativeButton Button_Yes => new NativeButton(Core.IdentifyFromWindowText("はい(&Y)"));
        public NativeButton Button_No => new NativeButton(Core.IdentifyFromWindowText("いいえ(&N)"));
        public NativeButton Button_OK => new NativeButton(Core.IdentifyFromWindowText("OK"));
        public NativeButton Button_Cancel => new NativeButton(Core.IdentifyFromWindowText("キャンセル"));
    }

    public static class MessageBoxDriverExtensions
    {
        [WindowDriverIdentify(CustomMethod = "TryAttach")]
        public static MessageBoxDriver Attach_MessageBox(this WindowsAppFriend app, string title)
            => new MessageBoxDriver(app.WaitForIdentifyFromWindowText(title));

        public static bool TryAttach(WindowControl window, out string title)
        {
            title = null;
            if (window.AppVar != null) return false;

            var staticCount = window.GetFromWindowClass("Static").Length;

            var buttons = window.GetFromWindowClass("Button");
            if (buttons.Length == 0 || 3 < buttons.Length) return false;

            //The message box must consist of buttons and static.
            int childCount = 0;
            EnumWindowsDelegate emumFunc = (hWnd, param) => { childCount++; return true; };
            EnumChildWindows(window.Handle, emumFunc, IntPtr.Zero);
            GC.KeepAlive(emumFunc);
            if (buttons.Length + staticCount != childCount) return false;

            title = window.GetWindowText();
            return true;
        }

        private delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lparam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private extern static bool EnumChildWindows(IntPtr hWnd, EnumWindowsDelegate lpEnumFunc, IntPtr lparam);
    }
}