using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Kamishibai.Wpf.Demo
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Debug.WriteLine("App.Ctor()");
            this.Activated += (sender, args) => { Debug.WriteLine("App.Activated"); };
            this.Deactivated += (sender, args) => { Debug.WriteLine("App.Deactivated"); };
            this.Exit += (sender, args) => { Debug.WriteLine("App.Exit"); };
            this.FragmentNavigation += (sender, args) => { Debug.WriteLine("App.FragmentNavigation"); };
            this.LoadCompleted += (sender, args) => { Debug.WriteLine("App.LoadCompleted"); };
            this.Navigated += (sender, args) => { Debug.WriteLine("App.Navigated"); };
            this.Navigating += (sender, args) => { Debug.WriteLine("App.Navigating"); };
            this.NavigationFailed += (sender, args) => { Debug.WriteLine("App.NavigationFailed"); };
            this.NavigationProgress += (sender, args) => { Debug.WriteLine("App.NavigationProgress"); };
            this.NavigationStopped += (sender, args) => { Debug.WriteLine("App.NavigationStopped"); };
            this.Startup += (sender, args) => { Debug.WriteLine("App.Startup"); };
            this.SessionEnding += (sender, args) => { Debug.WriteLine("App.SessionEnding"); };
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            ViewLocator.Register<MainWindowViewModel, MainWindow>();
            new KamishibaiBootstrapper().Run(this);
        }
    }
}
