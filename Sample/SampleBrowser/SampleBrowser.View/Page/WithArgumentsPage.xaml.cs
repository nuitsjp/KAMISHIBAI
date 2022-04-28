using SampleBrowser.ViewModel.Page;

namespace SampleBrowser.View.Page;

/// <summary>
/// WithArgumentsPage.xaml の相互作用ロジック
/// </summary>
public partial class WithArgumentsPage
{
    public WithArgumentsPage()
    {
        InitializeComponent();
    }
}

public class DesignWithArgumentsViewModel : WithArgumentsViewModel
{
    public DesignWithArgumentsViewModel() : base(default!, default!)
    {
    }
}