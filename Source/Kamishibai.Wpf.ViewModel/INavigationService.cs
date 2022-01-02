using System.Net.Mime;

namespace Kamishibai.Wpf.ViewModel
{
    public interface INavigationService
    {
        public static INavigationService DesignInstance => new DesignNavigationService();
        public Task NavigateAsync<TViewModel>() where TViewModel : class;
        public Task NavigateAsync<TViewModel, T>(T obj) where TViewModel : class, INavigationAware<T>;

        private class DesignNavigationService : INavigationService
        {
            public Task NavigateAsync<TViewModel>() where TViewModel : class
            {
                throw new NotImplementedException();
            }

            public Task NavigateAsync<TViewModel, T>(T obj) where TViewModel : class, INavigationAware<T>
            {
                throw new NotImplementedException();
            }
        }
    }
}