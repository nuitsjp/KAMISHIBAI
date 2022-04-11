namespace Kamishibai.Wpf.ViewModel;

public interface INavigationFrameProvider
{
    public INavigationFrame GetNavigationFrame(string frameName);
}