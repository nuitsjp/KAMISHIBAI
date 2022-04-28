using SampleBrowser.ViewModel.Page;

namespace SampleBrowser.View.Page;

/// <summary>
/// ConstructorWithArgumentsPage.xaml の相互作用ロジック
/// </summary>
public partial class ConstructorWithArgumentsPage
{
    public ConstructorWithArgumentsPage()
    {
        InitializeComponent();
    }
}

public class DesignConstructorWithArgumentsViewModel : ConstructorWithArgumentsViewModel
{
    public DesignConstructorWithArgumentsViewModel() : base(default!, default!)
    {
    }
}