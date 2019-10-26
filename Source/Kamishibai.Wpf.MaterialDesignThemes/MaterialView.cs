using System.Windows;
using System.Windows.Controls;

namespace Kamishibai.Wpf.MaterialDesignThemes
{
    public class MaterialView : IView
    {
        public MaterialView(ContentControl contentControl)
        {
            ContentControl = contentControl;
        }

        private ContentControl ContentControl { get; }
    }
}