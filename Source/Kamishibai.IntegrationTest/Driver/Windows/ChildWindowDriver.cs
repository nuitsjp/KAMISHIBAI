using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;

namespace Driver.Windows
{
    [WindowDriver(TypeFullName = "SampleBrowser.View.ChildWindow")]
    public class ChildWindowDriver
    {
        public WindowControl Core { get; }
        public WPFTextBlock WindowName => Core.LogicalTree().ByBinding("WindowName").FirstOrDefault()?.Dynamic()!;
        public WPFTextBlock Message => Core.LogicalTree().ByBinding("Message").FirstOrDefault()?.Dynamic()!;
        public WPFToggleButton BlockClosing => Core.LogicalTree().ByBinding("BlockClosing").FirstOrDefault()?.Dynamic()!; 
        public WPFButtonBase CloseWindowCommand => Core.LogicalTree().ByBinding("CloseWindowCommand").FirstOrDefault()?.Dynamic()!; 
        public WPFButtonBase CloseSpecifiedWindowCommand => Core.LogicalTree().ByBinding("CloseSpecifiedWindowCommand").FirstOrDefault()?.Dynamic()!;
        public WPFButtonBase CloseDialogCommand => Core.LogicalTree().ByBinding("CloseDialogCommand").FirstOrDefault()?.Dynamic()!;
        public WPFToggleButton DialogResult => Core.LogicalTree().ByBinding("DialogResult").FirstOrDefault()?.Dynamic()!;
        public bool IsLoaded => (bool)((AppVar)Core.Dynamic().IsLoaded).Core;

        // ReSharper disable once UnusedMember.Global
        public ChildWindowDriver(WindowControl core)
        {
            Core = core;
        }

        // ReSharper disable once UnusedMember.Global
        public ChildWindowDriver(AppVar core)
        {
            Core = new WindowControl(core);
        }
    }

    public static class ChildWindowDriverExtensions
    {
        [WindowDriverIdentify(TypeFullName = "SampleBrowser.View.ChildWindow")]
        public static ChildWindowDriver AttachChildWindow(this WindowsAppFriend app)
            => app.WaitForIdentifyFromTypeFullName("SampleBrowser.View.ChildWindow").Dynamic();
    }
}