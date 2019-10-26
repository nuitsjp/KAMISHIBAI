using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace Kamishibai.Wpf.MaterialDesignThemes
{
    public class MaterialPresentationService : PresentationService
    {
        private MaterialPresentationService(Window window) : base(window)
        {
        }

        public new static void Setup(Application application)
        {
            application.Activated += ApplicationOnActivated;
        }

        private static void ApplicationOnActivated(object sender, EventArgs e)
        {
            var mainWindow = ((Application) sender).MainWindow;
            var dataContext = mainWindow?.DataContext;
            if (dataContext is IViewModel viewModel)
            {
                viewModel.PresentationService = new MaterialPresentationService(mainWindow);
            }
        }

        public override async Task<IView> ShowDialogAsync<TViewModel>(Action<TViewModel> initializeViewModel)
        {
            var newObject = CreateView<TViewModel>();
            if (newObject is ContentControl contentControl)
            {
                initializeViewModel(contentControl.DataContext as TViewModel);
                await DialogHost.Show(contentControl);
                return new MaterialView(contentControl);
            }
            else
            {
                throw new ArgumentException($"{typeof(TViewModel)} is not window.");
            }
        }
    }
}