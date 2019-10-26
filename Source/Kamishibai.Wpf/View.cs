using System.Windows;

namespace Kamishibai.Wpf
{
    public class View : IView
    {
        public View(Window window)
        {
            Window = window;
        }

        private Window Window { get; }
    }
}