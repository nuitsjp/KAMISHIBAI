using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
using System.Linq;

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