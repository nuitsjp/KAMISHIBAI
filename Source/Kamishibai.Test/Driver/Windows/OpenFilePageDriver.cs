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
    public class OpenFilePageDriver
    {
        public WindowControl Core { get; }
        public WPFToggleButton AddToMostRecentlyUsedList => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("AddToMostRecentlyUsedList").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton AllowPropertyEditing => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("AllowPropertyEditing").FirstOrDefault()?.Dynamic(); 
        public WPFTextBox DefaultDirectory => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("DefaultDirectory").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu DefaultDirectoryContextMenu => new() {Target = DefaultDirectory.AppVar};
        public WPFTextBox DefaultExtension => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("DefaultExtension").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu DefaultExtensionContextMenu => new() {Target = DefaultExtension.AppVar};
        public WPFTextBox DefaultFileName => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("DefaultFileName").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu DefaultFileNameContextMenu => new() {Target = DefaultFileName.AppVar};
        public WPFToggleButton EnsureFileExists => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("EnsureFileExists").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton EnsurePathExists => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("EnsurePathExists").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton EnsureReadOnly => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("EnsureReadOnly").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton EnsureValidNames => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("EnsureValidNames").FirstOrDefault()?.Dynamic(); 
        public WPFTextBox Filter => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("Filter").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu FilterContextMenu => new() {Target = Filter.AppVar};
        public WPFTextBox InitialDirectory => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("InitialDirectory").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu InitialDirectoryContextMenu => new() {Target = InitialDirectory.AppVar};
        public WPFToggleButton IsFolderPicker => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("IsFolderPicker").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton Multiselect => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("Multiselect").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton NavigateToShortcut => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("NavigateToShortcut").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton RestoreDirectory => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("RestoreDirectory").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton ShowHiddenItems => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("ShowHiddenItems").FirstOrDefault()?.Dynamic(); 
        public WPFToggleButton ShowPlacesList => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("ShowPlacesList").FirstOrDefault()?.Dynamic(); 
        public WPFTextBox Place => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("Place").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu PlaceContextMenu => new() {Target = Place.AppVar};
        public WPFTextBox Title => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("Title").FirstOrDefault()?.Dynamic(); 
        public WPFContextMenu TitleContextMenu => new() {Target = Title.AppVar};
        public WPFButtonBase OpenFileCommand => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("OpenFileCommand").FirstOrDefault()?.Dynamic(); 
        public WPFTextBlock OpenFileResult => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("OpenFileResult").FirstOrDefault()?.Dynamic(); 
        public WPFTextBlock FilePath => Core.LogicalTree().ByType<ContentControl>().ByContentText("System.Windows.Controls.ScrollViewer").Single().LogicalTree().ByBinding("FilePath").FirstOrDefault()?.Dynamic(); 

        public OpenFilePageDriver(WindowControl core)
        {
            Core = core;
        }

        public OpenFilePageDriver(AppVar core)
        {
            Core = new WindowControl(core);
        }
    }

    public static class OpenFilePageDriverExtensions
    {
        [WindowDriverIdentify(TypeFullName = "SampleBrowser.View.MainWindow")]
        public static OpenFilePageDriver AttachOpenFilePage(this WindowsAppFriend app)
            => app.WaitForIdentifyFromTypeFullName("SampleBrowser.View.MainWindow").Dynamic();
    }
}