#nullable enable
using System.Diagnostics;

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
        _navigationService.Pausing += HandleNavigationEvent;
        _navigationService.Navigating += HandleNavigationEvent;
        _navigationService.Navigated += HandleNavigationEvent;
        _navigationService.Paused += HandleNavigationEvent;
        _navigationService.Disposing += HandleNavigationEvent;
        _navigationService.Resuming += HandleNavigationEvent;
        _navigationService.Resumed += HandleNavigationEvent;
        _navigationService.Disposed += HandleNavigationEvent;
    }

    private void HandleNavigationEvent(object? sender, EventArgs e)
    {
        Debug.WriteLine($"MainWindowViewModel#HandleNavigationEvent sender:{sender} eventArgs:{e}");

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