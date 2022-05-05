using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
// ReSharper disable UnusedMember.Global

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