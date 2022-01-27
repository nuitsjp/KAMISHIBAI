using System.Windows;
using System.Windows.Controls;
using Kamishibai.Wpf.ViewModel;

namespace Kamishibai.Wpf.View;

public class NavigationFrame : Grid, INavigationFrame
{
    private static readonly LinkedList<WeakReference<NavigationFrame>> NavigationFrames = new();

    public static readonly DependencyProperty FrameNameProperty = DependencyProperty.Register(
        "FrameName", typeof(string), typeof(NavigationFrame), new PropertyMetadata(string.Empty));

    private readonly Stack<FrameworkElement> _pages = new();
    private IViewProvider? _viewProvider;

    public NavigationFrame()
    {
        CleanNavigationFrames();
        NavigationFrames.AddFirst(new WeakReference<NavigationFrame>(this));
    }

    internal IViewProvider ViewProvider
    {
        get => _viewProvider!;
        set => _viewProvider = value;
    }

    public string FrameName
    {
        get => (string) GetValue(FrameNameProperty);
        set => SetValue(FrameNameProperty, value);
    }

    internal static NavigationFrame GetNavigationFrame(string name)
    {
        foreach (var weakReference in NavigationFrames.ToArray())
        {
            if (weakReference.TryGetTarget(out var navigationFrame))
            {
                if (object.Equals(name, navigationFrame.FrameName))
                {
                    return navigationFrame;
                }
            }
            else
            {
                NavigationFrames.Remove(weakReference);
            }
        }

        throw new InvalidOperationException($"There is no NavigationFrame named '{name}'.");
    }

    private static void CleanNavigationFrames()
    {
        foreach (var weakReference in NavigationFrames.ToArray())
        {
            if (!weakReference.TryGetTarget(out var _))
            {
                NavigationFrames.Remove(weakReference);
            }
        }
    }

    internal void Navigate(FrameworkElement page)
    {
        _pages.Push(page);
        Children.Clear();
        Children.Add(page);
    }

    internal void GoBack()
    {
        if (_pages.Count == 1)
        {
            return;
        }

        _pages.Pop();
        Children.Clear();
        Children.Add(_pages.Peek());
    }

    public Task<bool> TryNavigateAsync<TViewModel, T1>(T1 param1) where TViewModel : class, INavigatingAsyncAware<T1>
    {
        throw new NotImplementedException();
    }

    public async Task<bool> TryNavigateAsync<TViewModel, T1, T2>(T1 param1, T2 param2) where TViewModel : class, INavigatingAsyncAware<T1, T2>
    {
        var view = ViewProvider.ResolvePresentation<TViewModel>();

        await ((INavigatingAsyncAware<T1, T2>)view.DataContext).OnNavigatingAsync(param1, param2);
        Navigate(view);
        return false;
    }

    public Task<bool> TryNavigateAsync<TViewModel>() where TViewModel : class
    {
        throw new NotImplementedException();
    }

    public Task<bool> TryNavigateAsync<TViewModel>(TViewModel viewModel) where TViewModel : class
    {
        throw new NotImplementedException();
    }

    public Task<bool> TryNavigateAsync<TViewModel>(Action<TViewModel> init) where TViewModel : class
    {
        throw new NotImplementedException();
    }

    public Task<bool> GoBackAsync()
    {
        GoBack();
        return Task.Run(() => false);
    }
}