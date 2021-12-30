using System.Reactive.Disposables;

namespace Kamishibai.Wpf.ViewModel;

public abstract class WindowViewModelBase : IWindowViewModel
{
    protected CompositeDisposable CompositeDisposable { get; } = new();

    public void Dispose()
    {
        CompositeDisposable.Dispose();
    }
}