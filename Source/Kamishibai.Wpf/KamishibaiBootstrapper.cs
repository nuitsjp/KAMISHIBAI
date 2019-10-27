using System;
using System.Windows;

namespace Kamishibai.Wpf
{
    public class KamishibaiBootstrapper
    {
        public void Run(Application application)
        {
            application.Activated += ApplicationOnActivated;
        }


        private void ApplicationOnActivated(object sender, EventArgs e)
        {
            var mainWindow = ((Application)sender).MainWindow;
            var dataContext = mainWindow?.DataContext;
            if (dataContext is IViewModel viewModel)
            {
                viewModel.PresentationService.Real = new RealPresentationService(mainWindow);
            }
        }
    }
}