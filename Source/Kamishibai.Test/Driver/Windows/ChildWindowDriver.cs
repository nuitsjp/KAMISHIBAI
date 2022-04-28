using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
using System.Linq;

namespace Driver.Windows
{
    [WindowDriver(TypeFullName = "SampleBrowser.View.ChildWindow")]
    public class ChildWindowDriver
    {
        public WindowControl Core { get; }
        public WPFToggleButton BlockClosing => Core.LogicalTree().ByBinding("BlockClosing").FirstOrDefault()?.Dynamic(); 
        public WPFButtonBase CloseCommand => Core.LogicalTree().ByBinding("CloseCommand").FirstOrDefault()?.Dynamic(); 
        public WPFButtonBase CloseSpecifiedWindowCommand => Core.LogicalTree().ByBinding("CloseSpecifiedWindowCommand").FirstOrDefault()?.Dynamic();
        public bool IsLoaded => (bool)((AppVar)Core.Dynamic().IsLoaded).Core;

        public ChildWindowDriver(WindowControl core)
        {
            Core = core;
        }

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