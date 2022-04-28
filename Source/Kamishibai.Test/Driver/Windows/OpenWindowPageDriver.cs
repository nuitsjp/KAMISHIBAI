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
    public class OpenWindowPageDriver
    {
        public WindowControl Core { get; }
        public WPFComboBox SelectedWindowStartupLocation => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("SelectedWindowStartupLocation").FirstOrDefault()?.Dynamic(); 
        public WPFButtonBase OpenByTypeCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("OpenByTypeCommand").FirstOrDefault()?.Dynamic(); 
        public WPFButtonBase OpenByGenericTypeCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("OpenByGenericTypeCommand").FirstOrDefault()?.Dynamic(); 
        public WPFButtonBase OpenByInstanceCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("OpenByInstanceCommand").FirstOrDefault()?.Dynamic(); 
        public WPFTextBox WindowName1 => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("WindowName1").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu WindowName1ContextMenu => new WPFContextMenu{Target = WindowName1.AppVar};
        public WPFButtonBase OpenWithCallbackCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("OpenWithCallbackCommand").FirstOrDefault()?.Dynamic(); 
        public WPFTextBox WindowName2 => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("WindowName2").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu WindowName2ContextMenu => new WPFContextMenu{Target = WindowName2.AppVar};
        public WPFButtonBase OpenWithSafeParameterCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("OpenWithSafeParameterCommand").FirstOrDefault()?.Dynamic(); 
        public WPFTextBox WindowName3 => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("WindowName3").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu WindowName3ContextMenu => new WPFContextMenu{Target = WindowName3.AppVar};

        public OpenWindowPageDriver(WindowControl core)
        {
            Core = core;
        }

        public OpenWindowPageDriver(AppVar core)
        {
            Core = new WindowControl(core);
        }
    }

    public static class OpenWindowPageDriverExtensions
    {
        [WindowDriverIdentify(TypeFullName = "SampleBrowser.View.MainWindow")]
        public static OpenWindowPageDriver AttachOpenWindowPage(this WindowsAppFriend app)
            => app.WaitForIdentifyFromTypeFullName("SampleBrowser.View.MainWindow").Dynamic();
    }
}