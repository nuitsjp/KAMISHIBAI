namespace Kamishibai.Wpf.ViewModel;

public partial interface INavigationFrame
{
    public Task<bool> TryNavigateAsync<TViewModel, T1>(T1 param1) where TViewModel : class, INavigatingAsyncAware<T1>;
    public Task<bool> TryNavigateAsync<TViewModel, T1, T2>(T1 param1, T2 param2) where TViewModel : class, INavigatingAsyncAware<T1, T2>;
}
