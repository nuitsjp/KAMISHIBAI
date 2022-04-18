using System.Windows.Controls;
using SampleBrowser.ViewModel;
using SampleBrowser.ViewModel.Page;

namespace SampleBrowser.View.Page;

public partial class ContentPage
{
    public ContentPage()
    {
        InitializeComponent();
    }
}

public class DesignContentViewModel : ContentViewModel
{
    public DesignContentViewModel() : base(default!)
    {
    }
}