using System.Windows;
using Kamishibai.Demo.ViewModel;

namespace Kamishibai.Demo.View;

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

public class DesignChildWindowViewModel : ChildWindowViewModel
{
    public DesignChildWindowViewModel() : base(default!)
    {
    }
}
