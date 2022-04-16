using KamishibaiApp.ViewModel;

namespace KamishibaiApp.View
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow
    {
        public ShellWindow(ShellWindowViewModel shellWindowViewModel)
        {
            InitializeComponent();
            DataContext = shellWindowViewModel;
        }
    }

    public class DesignShellWindowViewModel : ShellWindowViewModel
    {
        public DesignShellWindowViewModel() : base(default!, default!)
        {
        }
    }
}
