namespace Kamishibai.Wpf.ViewModel
{
    public interface INavigationService
    {
        void ShowMainWindow<TWindowViewModel>() where TWindowViewModel : class, IWindowViewModel;
        Task PushAsync<TViewModel>() where TViewModel : class;
        Task PopAsync();
        Task PushAsync<TViewModel>(Action<TViewModel> initialize) where TViewModel : class;
        Task PushModalAsync<TViewModel>() where TViewModel : class;
        Task PushModalAsync<TViewModel>(Action<TViewModel> initialize) where TViewModel : class;
    }

    public class DesignNavigationService : INavigationService
    {
        public void ShowMainWindow<TWindowViewModel>() where TWindowViewModel : class, IWindowViewModel
        {
            throw new NotImplementedException();
        }

        public Task PushAsync<TViewModel>() where TViewModel : class
        {
            throw new NotImplementedException();
        }

        public Task PopAsync()
        {
            throw new NotImplementedException();
        }

        public Task PushAsync<TViewModel>(Action<TViewModel> initialize) where TViewModel : class
        {
            throw new NotImplementedException();
        }

        public Task PushModalAsync<TViewModel>() where TViewModel : class
        {
            throw new NotImplementedException();
        }

        public Task PushModalAsync<TViewModel>(Action<TViewModel> initialize) where TViewModel : class
        {
            throw new NotImplementedException();
        }
    }
}