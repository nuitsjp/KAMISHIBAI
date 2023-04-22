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
    [UserControlDriver(TypeFullName = "SampleBrowser.View.Page.OpenDialogPage")]
    public class OpenDialogPageDriver
    {
        public WindowControl Core { get; }
        public WPFComboBox SelectedWindowStartupLocation => Core.LogicalTree().ByBinding("SelectedWindowStartupLocation").FirstOrDefault()?.Dynamic()!; 
        public WPFButtonBase OpenByTypeCommand => Core.LogicalTree().ByBinding("OpenByTypeCommand").FirstOrDefault()?.Dynamic()!; 
        public WPFButtonBase OpenByGenericTypeCommand => Core.LogicalTree().ByBinding("OpenByGenericTypeCommand").FirstOrDefault()?.Dynamic()!; 
        public WPFButtonBase OpenByInstanceCommand => Core.LogicalTree().ByBinding("OpenByInstanceCommand").FirstOrDefault()?.Dynamic()!; 
        public WPFTextBox WindowName1 => Core.LogicalTree().ByBinding("WindowName1").FirstOrDefault()?.Dynamic()!; 
        public WPFContextMenu WindowName1ContextMenu => new() {Target = WindowName1.AppVar};
        public WPFButtonBase OpenWithCallbackCommand => Core.LogicalTree().ByBinding("OpenWithCallbackCommand").FirstOrDefault()?.Dynamic()!; 
        public WPFTextBox WindowName2 => Core.LogicalTree().ByBinding("WindowName2").FirstOrDefault()?.Dynamic()!; 
        public WPFContextMenu WindowName2ContextMenu => new() {Target = WindowName2.AppVar};
        public WPFButtonBase OpenWithSafeParameterCommand => Core.LogicalTree().ByBinding("OpenWithSafeParameterCommand").FirstOrDefault()?.Dynamic()!; 
        public WPFTextBox WindowName3 => Core.LogicalTree().ByBinding("WindowName3").FirstOrDefault()?.Dynamic()!;
        public WPFTextBox DialogResult => Core.LogicalTree().ByBinding("DialogResult").FirstOrDefault()?.Dynamic()!;
        public WPFContextMenu WindowName3ContextMenu => new() {Target = WindowName3.AppVar};

        public OpenDialogPageDriver(WindowControl core)
        {
            Core = core;
        }

        public OpenDialogPageDriver(AppVar core)
        {
            Core = new WindowControl(core);
        }
    }

    public static class OpenDialogPageDriverExtensions
    {
        [WindowDriverIdentify(TypeFullName = "SampleBrowser.View.MainWindow")]
        public static OpenDialogPageDriver AttachOpenDialogPage(this WindowsAppFriend app)
            => app.WaitForIdentifyFromTypeFullName("SampleBrowser.View.MainWindow").Dynamic();
    }

}