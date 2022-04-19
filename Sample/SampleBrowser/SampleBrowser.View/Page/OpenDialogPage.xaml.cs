using System.Windows.Controls;
using SampleBrowser.ViewModel.Page;

namespace SampleBrowser.View.Page;
/// <summary>
/// OpenWindowPage.xaml の相互作用ロジック
/// </summary>
public partial class OpenDialogPage : UserControl
{
    public OpenDialogPage()
    {
        InitializeComponent();
    }
}

public class DesignDialogWindowViewModel : OpenDialogViewModel
{
    public DesignDialogWindowViewModel() : base(default!)
    {
    }
}
