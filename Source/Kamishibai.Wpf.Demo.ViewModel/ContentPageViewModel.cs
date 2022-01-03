using Kamishibai.Wpf.ViewModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Kamishibai.Wpf.Demo.ViewModel;

public class ContentPageViewModel : INavigatedAsyncAware<string, int>
{
    private readonly INavigationService _navigationService;

    public ContentPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        NavigateNextCommand = new AsyncRelayCommand(OnNavigateNext);
        GoBackCommand = new RelayCommand(OnGoBack);
    }

    public string FrameName { get; set; } = string.Empty;
    public int Count { get; set; }

    public AsyncRelayCommand NavigateNextCommand { get; }
    public RelayCommand GoBackCommand { get; }

    public Task OnNavigatedAsync(string frameName, int count)
    {
        FrameName = frameName;
        Count = count;
        return Task.CompletedTask;
    }

    private Task OnNavigateNext()
    {
        return _navigationService.NavigateAsync<ContentPageViewModel, string, int>(FrameName, FrameName, ++Count);
    }

    private void OnGoBack()
    {
        _navigationService.GoBack(FrameName);
    }
}

public class DesignContentPageViewModel : ContentPageViewModel
{
    public DesignContentPageViewModel() : base(INavigationService.DesignInstance)
    {
    }
}
