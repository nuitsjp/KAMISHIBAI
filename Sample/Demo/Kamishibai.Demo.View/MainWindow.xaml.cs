using Kamishibai.Demo.ViewModel;

namespace Kamishibai.Demo.View
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
        }
    }
    public class DesignMainWindowViewModel : MainWindowViewModel
    {
        public DesignMainWindowViewModel() : base(default!)
        {
        }
    }
}
