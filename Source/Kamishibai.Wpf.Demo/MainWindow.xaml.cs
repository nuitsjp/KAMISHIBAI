using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Kamishibai.Wpf.Demo
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Debug.WriteLine("MainWindow.Ctor()");
            this.Activated += (sender, args) => Debug.WriteLine("MainWindow.Activated");
            this.Deactivated += (sender, args) => Debug.WriteLine("MainWindow.Deactivated");
            this.Loaded += (sender, args) => Debug.WriteLine("MainWindow.Loaded");
            this.Initialized += (sender, args) => Debug.WriteLine("MainWindow.Initialized");
            this.Closed += (sender, args) => Debug.WriteLine("MainWindow.Closed");
            this.Closing += (sender, args) => Debug.WriteLine("MainWindow.Closing");
            this.FocusableChanged += (sender, args) => Debug.WriteLine("MainWindow.FocusableChanged");
            this.LostFocus += (sender, args) => Debug.WriteLine("MainWindow.LostFocus");
            this.GotFocus += (sender, args) => Debug.WriteLine("MainWindow.GotFocus");
            this.Unloaded += (sender, args) => Debug.WriteLine("MainWindow.Unloaded");
            this.DataContextChanged += (sender, args) => Debug.WriteLine("MainWindow.DataContextChanged");
            InitializeComponent();
        }
    }
}
