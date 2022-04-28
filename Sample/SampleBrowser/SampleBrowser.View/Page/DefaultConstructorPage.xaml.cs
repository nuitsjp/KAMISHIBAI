using SampleBrowser.ViewModel.Page;

namespace SampleBrowser.View.Page;

public partial class DefaultConstructorPage
{
    public DefaultConstructorPage()
    {
        InitializeComponent();
    }
}

public class DesignDefaultConstructorViewModel : DefaultConstructorViewModel
{
    public DesignDefaultConstructorViewModel() : base(default!)
    {
    }
}