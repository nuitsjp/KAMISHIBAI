using System.Windows;

namespace Kamishibai.Wpf.View;

internal class NavigationStack
{
    private readonly Stack<FrameworkElement> _pages = new();

    internal bool HasPage => 0 < _pages.Count;

    internal FrameworkElement CurrentPage => _pages.Peek();

    internal void Push(FrameworkElement page)
    {
        _pages.Push(page);
    }

    internal FrameworkElement Pop()
    {
        _pages.Pop();
        return _pages.Peek();
    }
}