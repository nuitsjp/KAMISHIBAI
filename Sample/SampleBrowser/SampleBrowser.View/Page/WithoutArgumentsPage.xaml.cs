using SampleBrowser.ViewModel.Page;

namespace SampleBrowser.View.Page;

public partial class WithoutArgumentsPage
{
    public WithoutArgumentsPage()
    {
        InitializeComponent();
    }
}

public class DesignWithoutArgumentsViewModel : WithoutArgumentsViewModel
{
    public DesignWithoutArgumentsViewModel() : base(default!)
    {
    }
}