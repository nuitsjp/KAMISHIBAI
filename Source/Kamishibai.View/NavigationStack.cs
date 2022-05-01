using System.Collections;
using System.Windows;

namespace Kamishibai;

internal class NavigationStack : IObservable<FrameworkElement>, IReadOnlyList<FrameworkElement>
{
    private readonly List<IObserver<FrameworkElement>> _observers;

    private readonly List<FrameworkElement> _pages = new();

    public NavigationStack()
    {
        _observers = new List<IObserver<FrameworkElement>>();
    }

    public FrameworkElement Peek => _pages[_pages.Count - 1];
    public FrameworkElement Previous => _pages[_pages.Count - 2];

    public bool CanPop => 1 < _pages.Count;

    public bool TryPeek(out FrameworkElement? page)
    {
        if (_pages.Count == 0)
        {
            page = null;
            return false;
        }

        page = Peek;
        return true;
    }
    public void Push(FrameworkElement page)
    {
        _pages.Add(page);
        foreach (var observer in _observers)
        {
            observer.OnNext(page);
        }
    }

    public FrameworkElement Pop()
    {
        var peek = Peek;
        var previous = Previous;
        _pages.RemoveAt(_pages.Count - 1);

        foreach (var observer in _observers)
        {
            observer.OnNext(previous);
        }
        return peek;
    }

    private class UnSubscriber : IDisposable
    {
        private readonly List<IObserver<FrameworkElement>> _observers;
        private IObserver<FrameworkElement>? _observer;

        public UnSubscriber(List<IObserver<FrameworkElement>> observers, IObserver<FrameworkElement> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer == null) return;

            _observers.Remove(_observer);
            _observer = null;
        }
    }

    public IDisposable Subscribe(IObserver<FrameworkElement> observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);

        return new UnSubscriber(_observers, observer);
    }

    public IEnumerator<FrameworkElement> GetEnumerator()
    {
        return _pages.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => _pages.Count;

    public FrameworkElement this[int index] => _pages[index];
}