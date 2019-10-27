using System;
using System.Threading.Tasks;

namespace Kamishibai.Wpf
{
    public class PresentationService : IPresentationService
    {
        public IPresentationService Real { private get; set; }

        public Task<IView> ShowWindowAsync<TViewModel>(Action<TViewModel> initializeViewModel) where TViewModel : class
        {
            return Real.ShowWindowAsync(initializeViewModel);
        }

        public Task<IView> ShowDialogAsync<TViewModel>(Action<TViewModel> initializeViewModel) where TViewModel : class
        {
            return Real.ShowDialogAsync(initializeViewModel);
        }
    }
}