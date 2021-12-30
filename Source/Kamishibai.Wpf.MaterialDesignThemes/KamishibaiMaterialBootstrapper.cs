using System;
using System.Windows;

namespace Kamishibai.Wpf.MaterialDesignThemes
{
    public class KamishibaiMaterialBootstrapper
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
                viewModel.PresentationService.Real = new MaterialPresentationService(mainWindow);
            }
        }

    }
}