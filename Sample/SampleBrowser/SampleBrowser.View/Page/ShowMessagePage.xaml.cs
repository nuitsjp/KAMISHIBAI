using System.Windows.Controls;
using SampleBrowser.ViewModel;
using SampleBrowser.ViewModel.Page;

namespace SampleBrowser.View.Page;
/// <summary>
/// ShowMessagePage.xaml の相互作用ロジック
/// </summary>
public partial class ShowMessagePage : UserControl
{
    public ShowMessagePage()
    {
        InitializeComponent();
    }
}

public class DesignShowMessageViewModel : ShowMessageViewModel
{
    public DesignShowMessageViewModel() : base(default!)
    {
    }
}