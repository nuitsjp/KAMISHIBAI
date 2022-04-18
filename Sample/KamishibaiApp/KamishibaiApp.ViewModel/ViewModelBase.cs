using System.Reactive.Disposables;

namespace KamishibaiApp.ViewModel;

public abstract class ViewModelBase : IDisposable
{
    public readonly CompositeDisposable CompositeDisposable = new();

    public virtual void Dispose()
    {
        CompositeDisposable.Dispose();
    }
}