namespace KamishibaiApp.ViewModel;

public static class DisposableExtensions
{
    public static void AddTo(this IDisposable disposable, ViewModelBase viewModelBase)
        => viewModelBase.CompositeDisposable.Add(disposable);
}