using Kamishibai.Wpf.ViewModel;

namespace Kamishibai.Wpf.Demo.ViewModel;

public class MainWindowViewModel : INavigationAware
{
    public string Message => "Hello, WPF on Generic Host!";
    public Task OnEntryAsync()
    {
        return Task.CompletedTask;
    }

    public Task OnExitAsync()
    {
        return Task.CompletedTask;
    }
}