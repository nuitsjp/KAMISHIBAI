using System;
using System.Deployment.Internal;
using System.Threading.Tasks;

namespace Kamishibai.Wpf
{
    public interface IPresentationService
    {
        IPresentationService Real { set; }
        Task<IView> ShowWindowAsync<TViewModel>(Action<TViewModel> initializeViewModel) where TViewModel : class;
        Task<IView> ShowDialogAsync<TViewModel>(Action<TViewModel> initializeViewModel) where TViewModel : class;
    }
}