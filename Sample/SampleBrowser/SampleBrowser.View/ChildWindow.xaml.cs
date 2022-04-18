using System.Windows;
using SampleBrowser.ViewModel;

namespace SampleBrowser.View;
/// <summary>
/// ChildWindow.xaml の相互作用ロジック
/// </summary>
public partial class ChildWindow : Window
{
    public ChildWindow()
    {
        InitializeComponent();
    }
}

public class DesignChildViewModel : ChildViewModel
{
    public DesignChildViewModel() : base(default!)
    {
    }
}