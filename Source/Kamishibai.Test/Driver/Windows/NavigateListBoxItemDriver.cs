using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
using System.Linq;

namespace Driver.Windows
{
    [UserControlDriver(TypeFullName = "System.Windows.Controls.ListBoxItem")]
    public class NavigateListBoxItemDriver
    {
        public WPFUIElement Core { get; }
        public WPFTextBlock Name => Core.VisualTree().ByBinding("Name").FirstOrDefault()?.Dynamic(); 

        public NavigateListBoxItemDriver(AppVar core)
        {
            Core = new WPFUIElement(core);
        }
    }
}