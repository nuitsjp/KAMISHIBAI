using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Driver.Windows.Native
{
    public class SaveFileDialogDriver
    {
        public WindowControl Core { get; private set; }

        public NativeButton Button_Save { get; set; }
        public NativeButton Button_Cancel { get; set; }
        public NativeComboBox ComboBox_FileName { get; private set; }
        public NativeComboBox ComboBox_FileType { get; private set; }

        public SaveFileDialogDriver(WindowControl core)
        {
            Core = core;

            var combos = Core.GetFromWindowClass("ComboBox");
            int indexFileName = combos.Length == 3 ? 1 : 0;
            int indexFileType = combos.Length == 3 ? 2 : 1;
            var sortedComobs = combos.OrderBy(e => GetWindowRect(e.Handle).Top).ToArray();
            ComboBox_FileName = new NativeComboBox(sortedComobs[indexFileName]);
            ComboBox_FileType = new NativeComboBox(sortedComobs[indexFileType]);

            Button_Save = new NativeButton(Core.IdentifyFromWindowText("保存(&S)"));
            Button_Cancel = new NativeButton(Core.IdentifyFromWindowText("キャンセル"));
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        static RECT GetWindowRect(IntPtr hwnd)
        {
            RECT lpRect;
            GetWindowRect(hwnd, out lpRect);
            return lpRect;
        }
    }

    public static class SaveFileDialogDriverExtensions
    {
        [WindowDriverIdentify(CustomMethod = "TryAttach")]
        public static SaveFileDialogDriver Attach_SaveFileDlialog(this WindowsAppFriend app, string title)
            => new SaveFileDialogDriver(WindowControl.WaitForIdentifyFromWindowText(app, title));

        public static SaveFileDialogDriver Attach_SaveFileDlialog(this WindowsAppFriend app, string title, Async async)
        {
            var core = WindowControl.WaitForIdentifyFromWindowText(app, title, async);
            if (core == null) return null;
            return new SaveFileDialogDriver(core);
        }

        public static bool TryAttach(WindowControl window, out string title)
        {
            title = null;
            if (window.GetFromWindowText("保存(&S)").Length != 1) return false;
            title = window.GetWindowText();
            return true;
        }
    }
}
