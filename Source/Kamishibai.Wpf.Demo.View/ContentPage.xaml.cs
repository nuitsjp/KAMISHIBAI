using Kamishibai.Wpf.Demo.ViewModel;

namespace Kamishibai.Wpf.Demo.View
{
    /// <summary>
    /// ContentPage.xaml の相互作用ロジック
    /// </summary>
    public partial class ContentPage
    {
        public ContentPage()
        {
            InitializeComponent();
        }
    }

    public class DesignContentPageViewModel : ContentPageViewModel
    {
        public DesignContentPageViewModel() : base(2, "frameName", default!, default!)
        {
        }
    }
}
