using System.Windows;
using System.Windows.Controls;

namespace Kamishibai.Wpf.View;

public class NavigationFrame : Grid
{
    private readonly Stack<FrameworkElement> _pages = new();

    internal bool HasPage => 0 < _pages.Count;

    internal FrameworkElement CurrentPage => _pages.Peek();

    internal void Push(FrameworkElement page)
    {
        _pages.Push(page);
        Children.Clear();
        Children.Add(page);
    }

    internal FrameworkElement Pop()
    {
        _pages.Pop();
        return _pages.Peek();
    }

}