using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace Driver.Windows.Native
{
    public class OpenFileDialogDriver
    {
        public WindowControl Core { get; }

        public NativeListControl ListView => new(Core.IdentifyFromWindowClass("SysListView32"));
        public NativeButton Button_Open { get; set; }
        public NativeButton Button_Cancel { get; set; }
        public NativeComboBox ComboBox_FilePath { get; }
        public NativeComboBox ComboBox_FileType { get; }

        public OpenFileDialogDriver(WindowControl window)
        {
            Core = window;
            ComboBox_FilePath = new NativeComboBox(Core.GetFromWindowClass("ComboBoxEx32").OrderBy(e => GetWindowRect(e.Handle).Top).Last());
            var handle = ComboBox_FilePath.ParentWindow.Handle;
            try
            {
                ComboBox_FileType = new NativeComboBox(Core.GetFromWindowClass("ComboBox").Where(e => e.ParentWindow.Handle == handle).OrderBy(e => GetWindowRect(e.Handle).Top).Last());
            }
            catch
            {
                // ignored
            }

            Button_Open = new NativeButton(Core.IdentifyFromWindowText("開く(&O)"));
            Button_Cancel = new NativeButton(Core.IdentifyFromWindowText("キャンセル"));
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        static RECT GetWindowRect(IntPtr hwnd)
        {
            GetWindowRect(hwnd, out var lpRect);
            return lpRect;
        }
    }

    public static class OpenFileDialogDriverExtensions
    {
        [WindowDriverIdentify(CustomMethod = "TryAttach")]
        public static OpenFileDialogDriver Attach_OpenFileDialog(this WindowsAppFriend app, string title)
            => new(WindowControl.WaitForIdentifyFromWindowText(app, title));

        public static bool TryAttach(WindowControl window, out string title)
        {
            title = null;
            if (window.GetFromWindowText("開く(&O)").Length != 1) return false;
            title = window.GetWindowText();
            return true;
        }
    }
}
