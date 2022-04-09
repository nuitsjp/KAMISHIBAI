namespace Kamishibai.Wpf.Demo.ViewModel;

public interface ISafeContentPageViewModelProvider
{
    SafeContentPageViewModel Resolve(int count);
}