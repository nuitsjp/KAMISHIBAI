using SampleBrowser.ViewModel.Page;

namespace SampleBrowser.View.Page
{
    /// <summary>
    /// NavigationMenuPage.xaml の相互作用ロジック
    /// </summary>
    public partial class NavigationMenuPage
    {
        public NavigationMenuPage()
        {
            InitializeComponent();
        }
    }

    public class DesignNavigationMenuViewModel : NavigationMenuViewModel
    {
        public DesignNavigationMenuViewModel() : base(default!)
        {
        }
    }
}
