using System.Diagnostics;
using Kamishibai.Wpf.ViewModel;

namespace Kamishibai.Wpf.Demo.ViewModel;

public class MainWindowViewModel : 
    INavigatingAsyncAware,
    INavigatingAware,
    INavigatedAsyncAware
{
    private readonly INavigationService _navigationService;

    public MainWindowViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public string SecondFrameName => "SecondFrame";

    public async Task OnNavigatedAsync()
    {
        await _navigationService.Frame.NavigateAsync<ContentPageViewModel>(x =>
        {
            x.FrameName = INavigationFrame.DefaultFrameName;
            x.Count = 1;
        });
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
    public DesignMainWindowViewModel() : base(INavigationService.DesignInstance)
    {
    }
}