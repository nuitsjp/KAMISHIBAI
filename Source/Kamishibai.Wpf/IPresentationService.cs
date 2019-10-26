using System;
using System.Threading.Tasks;

namespace Kamishibai.Wpf
{
    public interface IPresentationService
    {
        Task<IView> ShowWindowAsync<TViewModel>(Action<TViewModel> initializeViewModel) where TViewModel : class;
        Task<IView> ShowDialogAsync<TViewModel>(Action<TViewModel> initializeViewModel) where TViewModel : class;
    }
}