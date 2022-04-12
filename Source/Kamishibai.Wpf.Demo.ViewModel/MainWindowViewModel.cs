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
        await _navigationService.NavigateToContentPageAsync(1, "");
        await _navigationService.NavigateToContentPageAsync(1, SecondFrameName);
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