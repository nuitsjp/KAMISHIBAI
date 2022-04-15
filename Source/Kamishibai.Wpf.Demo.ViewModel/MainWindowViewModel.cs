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
    }

    private void HandleNavigationEvent(object? sender, EventArgs e)
    {
        Debug.WriteLine($"MainWindowViewModel#HandleNavigationEvent sender:{sender} eventArgs:{e}");

    }

    public string SecondFrameName => "SecondFrame";

    public async Task OnNavigatedAsync()
    {
        _navigationService.GetNavigationFrame().Pausing += HandleNavigationEvent;
        _navigationService.GetNavigationFrame().Navigating += HandleNavigationEvent;
        _navigationService.GetNavigationFrame().Navigated += HandleNavigationEvent;
        _navigationService.GetNavigationFrame().Paused += HandleNavigationEvent;
        _navigationService.GetNavigationFrame().Disposing += HandleNavigationEvent;
        _navigationService.GetNavigationFrame().Resuming += HandleNavigationEvent;
        _navigationService.GetNavigationFrame().Resumed += HandleNavigationEvent;
        _navigationService.GetNavigationFrame().Disposed += HandleNavigationEvent;

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