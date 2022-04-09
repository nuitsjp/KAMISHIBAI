using System.Diagnostics;
using Kamishibai.Wpf.ViewModel;

namespace Kamishibai.Wpf.Demo.ViewModel;

public class MainWindowViewModel : 
    INavigatingAsyncAware,
    INavigatingAware,
    INavigatedAsyncAware
{
    private readonly INavigationService _navigationService;

    private readonly ISafeContentPageViewModelProvider _safeContentPageViewModelProvider;

    public MainWindowViewModel(INavigationService navigationService, ISafeContentPageViewModelProvider safeContentPageViewModelProvider)
    {
        _navigationService = navigationService;
        _safeContentPageViewModelProvider = safeContentPageViewModelProvider;
    }

    public string SecondFrameName => "SecondFrame";

    public async Task OnNavigatedAsync()
    {
        await _navigationService.Frame.NavigateAsync(
            _safeContentPageViewModelProvider.Resolve(1));
        await _navigationService.GetFrame(SecondFrameName).NavigateAsync<ContentPageViewModel>(x =>
        {
            x.FrameName = SecondFrameName;
            x.Count = 1;
        });
    }

    public Task OnNavigatingAsync()
    {
        Debug.WriteLine("MainWindowViewModel#OnNavigatingAsync");
        return Task.CompletedTask;
    }

    public void OnNavigating()
    {
        Debug.WriteLine("MainWindowViewModel#OnNavigating");
    }
}

public class DesignMainWindowViewModel : MainWindowViewModel
{
    private static readonly ISafeContentPageViewModelProvider DesignInstance =
        new SafeContentPageViewModelProvider(INavigationService.DesignInstance);
    public DesignMainWindowViewModel() : base(INavigationService.DesignInstance, DesignInstance)
    {
    }
}