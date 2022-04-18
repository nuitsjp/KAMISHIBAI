using System.Windows.Controls;
using SampleBrowser.ViewModel.Page;

namespace SampleBrowser.View.Page;

/// <summary>
/// MessagePage.xaml の相互作用ロジック
/// </summary>
public partial class MessagePage : UserControl
{
    public MessagePage()
    {
        InitializeComponent();
    }
}

public class DesignMessageViewModel : MessageViewModel
{
    public DesignMessageViewModel() : base(default!, default!)
    {
    }
}