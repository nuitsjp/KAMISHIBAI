using Kamishibai.Wpf.ViewModel;

namespace Kamishibai.Wpf.Demo.ViewModel;

public class MainWindowViewModel : INavigatedAsyncAware
{
    private readonly INavigationService _navigationService;

    public MainWindowViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public string SecondFrameName => "SecondFrame";

    public async Task OnNavigatedAsync()
    {
        await _navigationService.NavigateAsync<ContentPageViewModel, string, int>(INavigationService.DefaultFrameName, 1);
        await _navigationService.NavigateAsync<ContentPageViewModel, string, int>(SecondFrameName, SecondFrameName, 1);
    }
}

public class DesignMainWindowViewModel : MainWindowViewModel
{
    public DesignMainWindowViewModel() : base(INavigationService.DesignInstance)
    {
    }
}