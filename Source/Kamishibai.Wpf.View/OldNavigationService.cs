//using System.Windows;
//using Kamishibai.Wpf.ViewModel;

//namespace Kamishibai.Wpf.View;

//public class NavigationService : INavigationService
//{
//    private readonly IViewProvider _viewProvider;
//    private readonly Stack<NavigationStack> _modelStack = new();

//    public NavigationService(IViewProvider viewProvider)
//    {
//        _viewProvider = viewProvider;
//        _modelStack.Push(new NavigationStack());
//    }

//    private Window CurrentWindow
//    {
//        get
//        {
//            var navigationService = _modelStack.Peek();
//            if (navigationService.HasPage)
//            {
//                return Window.GetWindow(navigationService.CurrentPage)!;
//            }
//            else
//            {
//                return _viewProvider.Application.MainWindow!;
//            }
//        }
//    }

//    private NavigationStack CurrentNavigationStack => _modelStack.Peek();

//    public void ShowMainWindow<TWindowViewModel>() where TWindowViewModel : class, IWindowViewModel
//    {
//        var mainWindow = (Window)_viewProvider.ResolvePresentation<TWindowViewModel>();
//        TWindowViewModel viewModel = (TWindowViewModel)mainWindow.DataContext;

//        _viewProvider.Application.MainWindow = mainWindow;
//        mainWindow.Loaded += async (_, _) =>
//        {
//            if (viewModel is INavigationAware navigatable)
//            {
//                await navigatable.OnEntryAsync();
//            }
//        };
//        mainWindow.Show();
//    }

//    public Task PushAsync<TViewModel>() where TViewModel : class
//    {
//        return PushAsync<TViewModel>(_ => { });
//    }

//    public async Task PushAsync<TViewModel>(Action<TViewModel> initialize) where TViewModel : class
//    {
//        var nextPage = _viewProvider.ResolvePresentation<TViewModel>();
//        TViewModel viewModel = (TViewModel) nextPage.DataContext;
//        initialize(viewModel);

//        var window = CurrentWindow;
//        _modelStack.Peek().Push(nextPage);

//        var navigationFrame = window.GetChildOfType<NavigationFrame>()!;
//        navigationFrame.Children.Clear();
//        navigationFrame.Children.Add(nextPage);

//        if (viewModel is INavigationAware navigatable)
//        {
//            await navigatable.OnEntryAsync();
//        }
//    }

//    public async Task PopAsync()
//    {
//        var currentPage = CurrentNavigationStack.CurrentPage;
//        var previousPage = CurrentNavigationStack.Pop();

//        var window = Window.GetWindow(currentPage)!;
//        var navigationFrame = window.GetChildOfType<NavigationFrame>()!;
//        navigationFrame.Children.Clear();
//        navigationFrame.Children.Add(previousPage);

//        if (previousPage.DataContext is INavigationAware navigatable)
//        {
//            await navigatable.OnEntryAsync();
//        }
//    }

//    public Task PushModalAsync<TViewModel>() where TViewModel : class
//    {
//        throw new NotImplementedException();
//    }

//    public async Task PushModalAsync<TViewModel>(Action<TViewModel> initialize) where TViewModel : class
//    {
//        var nextPage = _viewProvider.ResolvePresentation<TViewModel>();
//        TViewModel viewModel = (TViewModel)nextPage.DataContext;
//        initialize(viewModel);

//        NavigationStack navigationStack = new();
//        navigationStack.Push(nextPage);
//        _modelStack.Push(navigationStack);
//        if (viewModel is INavigationAware navigatable)
//        {
//            await navigatable.OnEntryAsync();
//        }
//    }
//}