using System.Net.Mime;
namespace Kamishibai.Wpf.ViewModel;

public partial interface INavigationService
{
    public static INavigationService DesignInstance => new DesignNavigationService();
    public INavigationFrame Frame { get; }
    public INavigationFrame GetFrame(string frameName);

    private class DesignNavigationService : INavigationService
    {
        public INavigationFrame Frame => new DesignNavigationFrame();
        public INavigationFrame GetFrame(string frameName)
        {
            throw new NotImplementedException();
        }

        private class DesignNavigationFrame : INavigationFrame
        {
            public Task<bool> TryNavigateAsync<TViewModel, T1>(T1 param1) where TViewModel : class, INavigatingAsyncAware<T1>
            {
                throw new NotImplementedException();
            }

            public Task<bool> TryNavigateAsync<TViewModel, T1, T2>(T1 param1, T2 param2) where TViewModel : class, INavigatingAsyncAware<T1, T2>
            {
                throw new NotImplementedException();
            }

            public Task<bool> TryNavigateAsync<TViewModel>() where TViewModel : class
            {
                throw new NotImplementedException();
            }

            public Task<bool> TryNavigateAsync<TViewModel>(TViewModel viewModel) where TViewModel : class
            {
                throw new NotImplementedException();
            }

            public Task<bool> TryNavigateAsync<TViewModel>(Action<TViewModel> init) where TViewModel : class
            {
                throw new NotImplementedException();
            }

            public Task<bool> GoBackAsync()
            {
                throw new NotImplementedException();
            }
        }
    }
}
