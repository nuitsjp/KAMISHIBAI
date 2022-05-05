using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;
// ReSharper disable UnusedMember.Global

namespace Driver.Windows
{
    [WindowDriver(TypeFullName = "SampleBrowser.View.MainWindow")]
    public class NavigationMenuPageDriver
    {
        public WindowControl Core { get; }
        public WPFToggleButton BlockNavigation => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("BlockNavigation").FirstOrDefault()?.Dynamic(); 
        public WPFButtonBase NavigateByTypeCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("NavigateByTypeCommand").FirstOrDefault()?.Dynamic(); 
        public WPFButtonBase NavigateByGenericTypeCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("NavigateByGenericTypeCommand").FirstOrDefault()?.Dynamic(); 
        public WPFButtonBase NavigateByInstanceCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("NavigateByInstanceCommand").FirstOrDefault()?.Dynamic(); 
        public WPFTextBox Message1 => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("Message1").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu Message1ContextMenu => new() {Target = Message1.AppVar};
        public WPFButtonBase NavigateWithCallbackCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("NavigateWithCallbackCommand").FirstOrDefault()?.Dynamic(); 
        public WPFTextBox Message2 => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("Message2").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu Message2ContextMenu => new() {Target = Message2.AppVar};
        public WPFButtonBase NavigateWithSafeParameterCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("NavigateWithSafeParameterCommand").FirstOrDefault()?.Dynamic(); 
        public WPFTextBox Message3 => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("Message3").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu Message3ContextMenu => new() {Target = Message3.AppVar};

        public NavigationMenuPageDriver(WindowControl core)
        {
            Core = core;
        }

        public NavigationMenuPageDriver(AppVar core)
        {
            Core = new WindowControl(core);
        }
    }

    public static class NavigationMenuPageDriverExtensions
    {
        [WindowDriverIdentify(TypeFullName = "SampleBrowser.View.MainWindow")]
        public static NavigationMenuPageDriver AttachNavigationMenuPage(this WindowsAppFriend app)
            => app.WaitForIdentifyFromTypeFullName("SampleBrowser.View.MainWindow").Dynamic();
    }
}