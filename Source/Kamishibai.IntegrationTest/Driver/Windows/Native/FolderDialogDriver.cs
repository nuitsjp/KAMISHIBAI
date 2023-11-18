using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using Codeer.TestAssistant.GeneratorToolKit;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace Driver.Windows.Native
{
    public class FolderDialogDriver
    {
        public WindowControl Core { get; }
        public NativeTree Tree => new(Core.IdentifyFromWindowClass("SysTreeView32"));
        public NativeButton Button_OK => new(Core.IdentifyFromWindowText("OK"));
        public NativeButton Button_Cancel => new(Core.IdentifyFromWindowText("キャンセル"));

        public FolderDialogDriver(WindowControl core)
        {
            Core = core;
        }

        public void EmulateSelect(params string[] names)
        {
            for (int i = 1; i <= names.Length; i++)
            {
                var current = names.Take(i).ToArray();
                while (true)
                {
                    var item = Tree.FindNode(current);
                    if (item != IntPtr.Zero)
                    {
                        Tree.EnsureVisible(item);
                        Tree.EmulateSelectItem(item);
                        if (i != names.Length) Tree.EmulateExpand(item, true);
                        break;
                    }
                    else Thread.Sleep(10);
                }
            }
        }
    }

    public static class FolderDialogDriver_Extensions
    {
        [WindowDriverIdentify(CustomMethod = "TryAttach")]
        public static FolderDialogDriver Attach_Dlg_Folder(this WindowsAppFriend app, string text)
            => new(WindowControl.WaitForIdentifyFromWindowText(app, text));

        public static bool TryAttach(WindowControl window, out string title)
        {
            title = null;
            if (window.GetFromWindowClass("SysTreeView32").Length != 1 ||
            window.GetFromWindowText("OK").Length != 1 ||
            window.GetFromWindowText("キャンセル").Length != 1) return false;
            title = window.GetWindowText();
            return true;
        }
    }
}
