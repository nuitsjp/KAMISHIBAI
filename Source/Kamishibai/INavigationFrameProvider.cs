namespace Kamishibai;

public interface INavigationFrameProvider
{
    public INavigationFrame GetNavigationFrame(string frameName);
}