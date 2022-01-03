namespace Kamishibai.Wpf.ViewModel;

public interface INavigatedAsyncAware
{
    Task OnNavigatedAsync();
}

public interface INavigatedAsyncAware<in T1>
{
    Task OnNavigatedAsync(T1 param1);
}

public interface INavigatedAsyncAware<in T1, in T2>
{
    Task OnNavigatedAsync(T1 frameName, T2 count);
}