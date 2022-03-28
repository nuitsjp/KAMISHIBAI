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
            public Task<bool> NavigateAsync<TViewModel>() where TViewModel : class
            {
                throw new NotImplementedException();
            }

            public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel) where TViewModel : class
            {
                throw new NotImplementedException();
            }

            public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init) where TViewModel : class
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
