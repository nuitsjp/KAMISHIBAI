#nullable enable
using System.Diagnostics;

namespace Kamishibai.Demo.ViewModel;

public class MainWindowViewModel : 
    INavigatingAsyncAware,
    INavigatingAware,
    INavigatedAsyncAware
{
    private readonly IPresentationService _presentationService;

    public MainWindowViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    private void HandleNavigationEvent(object? sender, EventArgs e)
    {
        Debug.WriteLine($"MainWindowViewModel#HandleNavigationEvent sender:{sender} eventArgs:{e}");

    }

    public string SecondFrameName => "SecondFrame";

    public async Task OnNavigatedAsync()
    {
        _presentationService.GetNavigationFrame().Pausing += HandleNavigationEvent;
        _presentationService.GetNavigationFrame().Navigating += HandleNavigationEvent;
        _presentationService.GetNavigationFrame().Navigated += HandleNavigationEvent;
        _presentationService.GetNavigationFrame().Paused += HandleNavigationEvent;
        _presentationService.GetNavigationFrame().Disposing += HandleNavigationEvent;
        _presentationService.GetNavigationFrame().Resuming += HandleNavigationEvent;
        _presentationService.GetNavigationFrame().Resumed += HandleNavigationEvent;
        _presentationService.GetNavigationFrame().Disposed += HandleNavigationEvent;

        await _presentationService.NavigateToContentPageAsync(1, "");
        await _presentationService.NavigateToContentPageAsync(1, SecondFrameName);
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