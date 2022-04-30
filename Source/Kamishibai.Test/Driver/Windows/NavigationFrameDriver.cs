using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;

namespace Driver.Windows
{
    [UserControlDriver(TypeFullName = "Kamishibai.NavigationFrame")]
    public class NavigationFrameDriver
    {
        public WPFUIElement Core { get; }
        public AppVar Current => Core.Dynamic().Current; 

        public NavigationFrameDriver(AppVar core)
        {
            Core = new WPFUIElement(core);
        }
    }
}