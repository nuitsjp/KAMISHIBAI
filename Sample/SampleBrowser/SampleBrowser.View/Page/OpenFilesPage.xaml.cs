using System.Windows.Controls;
using SampleBrowser.ViewModel;
using SampleBrowser.ViewModel.Page;

namespace SampleBrowser.View.Page;

/// <summary>
/// OpenFilePage.xaml の相互作用ロジック
/// </summary>
public partial class OpenFilesPage : UserControl
{
    public OpenFilesPage()
    {
        InitializeComponent();
    }
}

public class DesignOpenFilesPageViewModel : OpenFilesViewModel
{
    public DesignOpenFilesPageViewModel() : base(default!)
    {
    }
}