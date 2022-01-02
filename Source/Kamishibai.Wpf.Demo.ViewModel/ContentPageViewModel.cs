using Kamishibai.Wpf.ViewModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Kamishibai.Wpf.Demo.ViewModel;

public class ContentPageViewModel : INavigationAware<int>
{
    private INavigationService _navigationService;

    public ContentPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        NavigateNextCommand = new AsyncRelayCommand(OnNavigateNext);
    }

    public int Count { get; set; }

    public AsyncRelayCommand NavigateNextCommand { get; }

    public Task OnEntryAsync(int count)
    {
        Count = count;
        return Task.CompletedTask;
    }

    private Task OnNavigateNext()
    {
        return _navigationService.NavigateAsync<ContentPageViewModel, int>(++Count);
    }


    public Task OnExitAsync()
    {
        return Task.CompletedTask;
    }
}