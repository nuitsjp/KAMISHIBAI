using SampleBrowser.ViewModel.Page;

namespace SampleBrowser.View.Page;
/// <summary>
/// OpenWindowPage.xaml の相互作用ロジック
/// </summary>
public partial class OpenWindowPage
{
    public OpenWindowPage()
    {
        InitializeComponent();
    }
}

public class DesignOpenWindowViewModel : OpenWindowViewModel
{
    public DesignOpenWindowViewModel() : base(default!)
    {
    }
}
