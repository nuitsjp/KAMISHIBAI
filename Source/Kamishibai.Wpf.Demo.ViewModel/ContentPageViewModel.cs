using Kamishibai.Wpf.ViewModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Kamishibai.Wpf.Demo.ViewModel;

public class ContentPageViewModel
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
        //return _navigationService.GetFrame(FrameName).NavigateAsync<ContentPageViewModel, string, int>(FrameName, ++Count);
        return _navigationService.GetFrame(FrameName).NavigateAsync<ContentPageViewModel>(x =>
        {
            x.FrameName = FrameName;
            x.Count = ++Count;
        });
    }

    private Task OnGoBack()
    {
        return _navigationService.GetFrame(FrameName).GoBackAsync();
    }
}

public class DesignContentPageViewModel : ContentPageViewModel
{
    public DesignContentPageViewModel() : base(INavigationService.DesignInstance)
    {
    }
}
