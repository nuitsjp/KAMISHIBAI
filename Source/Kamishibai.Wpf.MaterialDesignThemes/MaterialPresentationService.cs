using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace Kamishibai.Wpf.MaterialDesignThemes
{
    internal class MaterialPresentationService : IPresentationService
    {
        internal MaterialPresentationService(Window window)
        {
        }

        public IPresentationService Real { get; set; }

        public Task<IView> ShowWindowAsync<TViewModel>(Action<TViewModel> initializeViewModel) where TViewModel : class
        {
            var newObject = ViewLocator.GetInstance<TViewModel>();
            if (newObject is Window window)
            {
                initializeViewModel(window.DataContext as TViewModel);
                window.Show();
                return Task.Run(() => (IView)new View(window));
            }
            else
            {
                throw new ArgumentException($"{typeof(TViewModel)} is not window.");
            }
        }

        public async Task<IView> ShowDialogAsync<TViewModel>(Action<TViewModel> initializeViewModel) where TViewModel : class
        {
            var newObject = ViewLocator.GetInstance<TViewModel>();
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