using System.Net.Mime;

namespace Kamishibai.Wpf.ViewModel
{
    public interface INavigationService
    {
        public static readonly string DefaultFrameName = string.Empty;
        public static INavigationService DesignInstance => new DesignNavigationService();
        public Task NavigateAsync<TViewModel>() where TViewModel : class;
        public Task NavigateAsync<TViewModel, T1>(T1 param1) where TViewModel : class, INavigatedAsyncAware<T1>;
        public Task NavigateAsync<TViewModel, T1, T2>(T1 param1, T2 param2) where TViewModel : class, INavigatedAsyncAware<T1, T2>;

        public Task NavigateAsync<TViewModel>(string frameName) where TViewModel : class;
        public Task NavigateAsync<TViewModel, T1>(string frameName, T1 param1) where TViewModel : class, INavigatedAsyncAware<T1>;
        public Task NavigateAsync<TViewModel, T1, T2>(string frameName, T1 param1, T2 param2) where TViewModel : class, INavigatedAsyncAware<T1, T2>;

        public void GoBack();
        public void GoBack(string frameName);

        private class DesignNavigationService : INavigationService
        {
            public Task NavigateAsync<TViewModel>() where TViewModel : class
            {
                throw new NotImplementedException();
            }

            public Task NavigateAsync<TViewModel, T>(T param1) where TViewModel : class, INavigatedAsyncAware<T>
            {
                throw new NotImplementedException();
            }

            public Task NavigateAsync<TViewModel, T1, T2>(T1 param1, T2 param2) where TViewModel : class, INavigatedAsyncAware<T1, T2>
            {
                throw new NotImplementedException();
            }

            public Task NavigateAsync<TViewModel>(string frameName) where TViewModel : class
            {
                throw new NotImplementedException();
            }

            Task INavigationService.NavigateAsync<TViewModel, T>(string frameName, T param1)
            {
                throw new NotImplementedException();
            }

            public Task NavigateAsync<TViewModel, T1, T2>(string frameName, T1 param1, T2 param2) where TViewModel : class, INavigatedAsyncAware<T1, T2>
            {
                throw new NotImplementedException();
            }

            public void GoBack()
            {
            }

            public void GoBack(string frameName)
            {
                throw new NotImplementedException();
            }
        }
    }
}