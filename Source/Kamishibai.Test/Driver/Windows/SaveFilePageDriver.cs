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
    public class SaveFilePageDriver
    {
        public WindowControl Core { get; }
        public WPFToggleButton AddToMostRecentlyUsedList => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("AddToMostRecentlyUsedList").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton CheckBox => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByType("System.Windows.Controls.CheckBox")[1].Dynamic(); // TODO It is not the best way to identify. Please change to a better method.
        public WPFToggleButton CheckBox0 => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByType("System.Windows.Controls.CheckBox")[2].Dynamic(); // TODO It is not the best way to identify. Please change to a better method.
        public WPFToggleButton AlwaysAppendDefaultExtension => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("AlwaysAppendDefaultExtension").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton CreatePrompt => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("CreatePrompt").FirstOrDefault()?.Dynamic(); 
        public WPFTextBox DefaultExtension => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("DefaultExtension").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu DefaultExtensionContextMenu => new WPFContextMenu{Target = DefaultExtension.AppVar};
        public WPFTextBox DefaultFileName => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("DefaultFileName").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu DefaultFileNameContextMenu => new WPFContextMenu{Target = DefaultFileName.AppVar};
        public WPFToggleButton EnsureFileExists => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("EnsureFileExists").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton EnsurePathExists => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("EnsurePathExists").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton EnsureReadOnly => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("EnsureReadOnly").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton EnsureValidNames => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("EnsureValidNames").FirstOrDefault()?.Dynamic(); 
        public WPFTextBox Filter => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("Filter").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu FilterContextMenu => new WPFContextMenu{Target = Filter.AppVar};
        public WPFTextBox InitialDirectory => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("InitialDirectory").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu InitialDirectoryContextMenu => new WPFContextMenu{Target = InitialDirectory.AppVar};
        public WPFToggleButton IsExpandedMode => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("IsExpandedMode").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton NavigateToShortcut => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("NavigateToShortcut").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton OverwritePrompt => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("OverwritePrompt").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton RestoreDirectory => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("RestoreDirectory").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton ShowHiddenItems => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("ShowHiddenItems").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton ShowPlacesList => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("ShowPlacesList").FirstOrDefault()?.Dynamic(); 
        public WPFTextBox Place => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("Place").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu PlaceContextMenu => new WPFContextMenu{Target = Place.AppVar};
        public WPFTextBox Title => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("Title").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu TitleContextMenu => new WPFContextMenu{Target = Title.AppVar};
        public WPFButtonBase SaveFileCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("SaveFileCommand").FirstOrDefault()?.Dynamic(); 
        public WPFTextBlock OpenFileResult => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("OpenFileResult").FirstOrDefault()?.Dynamic(); 
        public WPFTextBlock FilePath => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("FilePath").FirstOrDefault()?.Dynamic(); 

        public SaveFilePageDriver(WindowControl core)
        {
            Core = core;
        }

        public SaveFilePageDriver(AppVar core)
        {
            Core = new WindowControl(core);
        }
    }

    public static class SaveFilePageDriverExtensions
    {
        [WindowDriverIdentify(TypeFullName = "SampleBrowser.View.MainWindow")]
        public static SaveFilePageDriver AttachSaveFilePage(this WindowsAppFriend app)
            => app.WaitForIdentifyFromTypeFullName("SampleBrowser.View.MainWindow").Dynamic();
    }
}