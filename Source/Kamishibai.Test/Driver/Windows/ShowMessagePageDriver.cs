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
    public class ShowMessagePageDriver
    {
        public WindowControl Core { get; }
        public WPFTextBox Message => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("Message").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu MessageContextMenu => new() {Target = Message.AppVar};
        public WPFTextBox Caption => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("Caption").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu CaptionContextMenu => new() {Target = Caption.AppVar};
        public WPFComboBox SelectedMessageBoxButton => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("SelectedMessageBoxButton").FirstOrDefault()?.Dynamic(); 
        public WPFComboBox SelectedMessageBoxImage => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("SelectedMessageBoxImage").FirstOrDefault()?.Dynamic(); 
        public WPFComboBox SelectedMessageBoxResult => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("SelectedMessageBoxResult").FirstOrDefault()?.Dynamic(); 
        public WPFComboBox SelectedMessageBoxOptions => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("SelectedMessageBoxOptions").FirstOrDefault()?.Dynamic(); 
        public WPFButtonBase ShowMessageCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.Grid").Single().LogicalTree().ByBinding("ShowMessageCommand").FirstOrDefault()?.Dynamic(); 

        public ShowMessagePageDriver(WindowControl core)
        {
            Core = core;
        }

        public ShowMessagePageDriver(AppVar core)
        {
            Core = new WindowControl(core);
        }
    }

    public static class ShowMessagePageDriverExtensions
    {
        [WindowDriverIdentify(TypeFullName = "SampleBrowser.View.MainWindow")]
        public static ShowMessagePageDriver AttachShowMessagePage(this WindowsAppFriend app)
            => app.WaitForIdentifyFromTypeFullName("SampleBrowser.View.MainWindow").Dynamic();
    }
}