using Kamishibai.Wpf.ViewModel;

namespace Kamishibai.Wpf.Demo.ViewModel;

public class SafeContentPageViewModelProvider : ISafeContentPageViewModelProvider
{
    private readonly INavigationService _navigationService;

    public SafeContentPageViewModelProvider(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public SafeContentPageViewModel Resolve(int count)
        => new(count, _navigationService);
}