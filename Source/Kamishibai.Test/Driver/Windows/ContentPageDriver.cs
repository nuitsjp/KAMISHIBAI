using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
using System.Linq;
using System.Windows.Controls;

namespace Driver.Windows
{
    [WindowDriver(TypeFullName = "SampleBrowser.View.MainWindow")]
    public class ContentPageDriver
    {
        public WindowControl Core { get; }
        public WPFTextBlock Message => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("Message").FirstOrDefault()?.Dynamic(); 
        public WPFButtonBase GoBackCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("GoBackCommand").FirstOrDefault()?.Dynamic(); 

        public ContentPageDriver(WindowControl core)
        {
            Core = core;
        }

        public ContentPageDriver(AppVar core)
        {
            Core = new WindowControl(core);
        }
    }

    public static class ContentPageDriverExtensions
    {
        [WindowDriverIdentify(TypeFullName = "SampleBrowser.View.MainWindow")]
        public static ContentPageDriver AttachContentPage(this WindowsAppFriend app)
            => app.WaitForIdentifyFromTypeFullName("SampleBrowser.View.MainWindow").Dynamic();
    }
}