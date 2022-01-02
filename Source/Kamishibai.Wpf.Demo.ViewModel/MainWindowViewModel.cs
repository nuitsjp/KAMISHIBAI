using Kamishibai.Wpf.ViewModel;

namespace Kamishibai.Wpf.Demo.ViewModel;

public class MainWindowViewModel : INavigationAware
{
    private readonly INavigationService _navigationService;

    public MainWindowViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public Task OnEntryAsync()
    {
        return _navigationService.NavigateAsync<ContentPageViewModel, int>(1);
    }

    public Task OnExitAsync()
    {
        return Task.CompletedTask;
    }
}