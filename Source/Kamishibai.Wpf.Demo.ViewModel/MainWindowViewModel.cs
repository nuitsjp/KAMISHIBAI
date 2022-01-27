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
        await _navigationService.Frame.TryNavigateAsync<ContentPageViewModel, string, int>(INavigationFrame.DefaultFrameName, 1);
        await _navigationService.GetFrame(SecondFrameName).TryNavigateAsync<ContentPageViewModel, string, int>(INavigationFrame.DefaultFrameName, 1);
    }
}

public class DesignMainWindowViewModel : MainWindowViewModel
{
    public DesignMainWindowViewModel() : base(INavigationService.DesignInstance)
    {
    }
}