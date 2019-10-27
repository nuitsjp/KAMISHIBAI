using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kamishibai.Wpf
{
    internal class RealPresentationService : IPresentationService
    {
        internal RealPresentationService(Window window)
        {
            Window = window;
        }

        private Window Window { get; set; }

        public IPresentationService Real { get; set; }

        public Task<IView> ShowWindowAsync<TViewModel>(Action<TViewModel> initializeViewModel) where TViewModel : class
        {
            var newObject = ViewProvider.Resolve<TViewModel>();
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

        public virtual Task<IView> ShowDialogAsync<TViewModel>(Action<TViewModel> initializeViewModel) where TViewModel : class
        {
            var newObject = ViewProvider.Resolve<TViewModel>();
            if (newObject is Window window)
            {
                initializeViewModel(window.DataContext as TViewModel);
                window.ShowDialog();
                return Task.Run(() => (IView)new View(window));
            }
            else
            {
                throw new ArgumentException($"{typeof(TViewModel)} is not window.");
            }
        }

    }
}