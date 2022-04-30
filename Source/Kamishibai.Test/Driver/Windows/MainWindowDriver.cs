using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;

namespace Driver.Windows
{
    [WindowDriver(TypeFullName = "SampleBrowser.View.MainWindow")]
    public class MainWindowDriver
    {
        public WindowControl Core { get; }
        public WPFListBox SelectedMenuItem => Core.VisualTree().ByBinding("SelectedMenuItem").FirstOrDefault()?.Dynamic(); 
        public NavigationFrameDriver NavigationFrame => Core.LogicalTree().ByType("Kamishibai.NavigationFrame").FirstOrDefault()?.Dynamic();
        // ReSharper disable once UnusedMember.Global
        public bool IsLoaded => (bool)((AppVar)Core.Dynamic().IsLoaded).Core;

        // ReSharper disable once UnusedMember.Global
        public MainWindowDriver(WindowControl core)
        {
            Core = core;
        }

        // ReSharper disable once UnusedMember.Global
        public MainWindowDriver(AppVar core)
        {
            Core = new WindowControl(core);
        }
    }

    public static class MainWindowDriverExtensions
    {
        [WindowDriverIdentify(TypeFullName = "SampleBrowser.View.MainWindow")]
        public static MainWindowDriver AttachMainWindow(this WindowsAppFriend app)
            => app.WaitForIdentifyFromTypeFullName("SampleBrowser.View.MainWindow").Dynamic();
    }
}