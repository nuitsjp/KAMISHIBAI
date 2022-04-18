using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SampleBrowser.ViewModel;
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