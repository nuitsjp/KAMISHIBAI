using System.Windows.Controls;
using SampleBrowser.ViewModel;
using SampleBrowser.ViewModel.Page;

namespace SampleBrowser.View.Page;

/// <summary>
/// OpenFilePage.xaml の相互作用ロジック
/// </summary>
public partial class OpenFilePage : UserControl
{
    public OpenFilePage()
    {
        InitializeComponent();
    }
}

public class DesignOpenFileViewModel : OpenFileViewModel
{
    public DesignOpenFileViewModel() : base(default!)
    {
    }
}