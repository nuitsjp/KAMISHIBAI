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

    private object? CurrentViewModel => 
        _pages.TryPeek(out var currentView) 
            ? currentView.DataContext 
            : null;

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

    public Task<bool> NavigateAsync<TViewModel>() where TViewModel : class
    {
        throw new NotImplementedException();
    }

    public Task<bool> NavigateAsync<TViewModel>(TViewModel viewModel) where TViewModel : class
    {
        var view = ViewProvider.ResolvePresentation<TViewModel>();
        throw new NotImplementedException();
    }

    public Task<bool> NavigateAsync<TViewModel>(Action<TViewModel> init) where TViewModel : class
    {
        var view = ViewProvider.ResolvePresentation<TViewModel>();
        var viewModel = (TViewModel) view.DataContext;
        init(viewModel);

        return NavigateAsync(view, viewModel);
    }

    private async Task<bool> NavigateAsync(FrameworkElement view, object? viewModel)
    {
        if (CurrentViewModel is IPausingAsyncAware pausingAsyncAware) await pausingAsyncAware.OnPausingAsync();
        if (CurrentViewModel is IPausingAware pausingAware) pausingAware.OnPausing();
        if (viewModel is INavigatingAsyncAware navigatingAsyncAware) await navigatingAsyncAware.OnNavigatingAsync();
        if (viewModel is INavigatingAware navigatingAware) navigatingAware.OnNavigating();

        _pages.Push(view);
        Children.Clear();
        Children.Add(view);

        if (viewModel is INavigatedAsyncAware navigatedAsyncAware) await navigatedAsyncAware.OnNavigatedAsync();
        if (viewModel is INavigatedAware navigatedAware) navigatedAware.OnNavigated();
        if (CurrentViewModel is IPausedAsyncAware pausedAsyncAware) await pausedAsyncAware.OnPausedAsync();
        if (CurrentViewModel is IPausedAware pausedAware) pausedAware.OnPaused();

        return true;
    }

    public async Task<bool> GoBackAsync()
    {
        if (_pages.Count == 1)
        {
            return false;
        }

        var sourceView = _pages.Pop();
        var sourceViewModel = sourceView.DataContext;
        var destinationView = _pages.Peek();
        var destinationViewModel = destinationView.DataContext;

        if (sourceViewModel is IDisposingAsyncAware disposingAsyncAware) await disposingAsyncAware.OnDisposingAsync();
        if (sourceViewModel is IDisposingAware disposingAware) disposingAware.OnDisposing();
        if (destinationViewModel is IResumingAsyncAware resumingAsyncAware) await resumingAsyncAware.OnResumingAsync();
        if (destinationViewModel is IResumingAware resumingAware) resumingAware.OnResuming();

        Children.Clear();
        Children.Add(destinationView);

        if (destinationViewModel is IResumedAsyncAware resumedAsyncAware) await resumedAsyncAware.OnResumedAsync();
        if (destinationViewModel is IResumedAware resumedAware) resumedAware.OnResumed();
        if (sourceViewModel is IDisposedAsyncAware disposedAsyncAware) await disposedAsyncAware.OnDisposedAsync();
        if (sourceViewModel is IDisposable disposable) disposable.Dispose();

        return true;
    }
}