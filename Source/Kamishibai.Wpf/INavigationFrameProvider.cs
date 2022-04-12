namespace Kamishibai.Wpf;

public interface INavigationFrameProvider
{
    public INavigationFrame GetNavigationFrame(string frameName);
}