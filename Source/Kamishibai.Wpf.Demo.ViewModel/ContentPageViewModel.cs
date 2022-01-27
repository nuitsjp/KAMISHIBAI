using Kamishibai.Wpf.ViewModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Kamishibai.Wpf.Demo.ViewModel;

public class ContentPageViewModel : INavigatingAsyncAware<string, int>
{
    private readonly INavigationService _navigationService;

    public ContentPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        NavigateNextCommand = new AsyncRelayCommand(OnNavigateNext);
        GoBackCommand = new AsyncRelayCommand(OnGoBack);
    }

    public string FrameName { get; set; } = string.Empty;
    public int Count { get; set; }

    public AsyncRelayCommand NavigateNextCommand { get; }
    public AsyncRelayCommand GoBackCommand { get; }

    private Task OnNavigateNext()
    {
        return _navigationService.GetFrame(FrameName).TryNavigateAsync<ContentPageViewModel, string, int>(FrameName, ++Count);
    }

    private Task OnGoBack()
    {
        return _navigationService.GetFrame(FrameName).GoBackAsync();
    }

    public Task OnNavigatingAsync(string frameName, int count)
    {
        FrameName = frameName;
        Count = count;
        return Task.CompletedTask;
    }
}

public class DesignContentPageViewModel : ContentPageViewModel
{
    public DesignContentPageViewModel() : base(INavigationService.DesignInstance)
    {
    }
}
