using Kamishibai.Wpf.ViewModel;

namespace Kamishibai.Wpf.View;

public class NavigationFrameProvider : INavigationFrameProvider
{
    private static readonly LinkedList<WeakReference<INavigationFrame>> NavigationFrames = new();

    internal static void AddNavigationFrame(INavigationFrame navigationFrame)
    {
        foreach (var weakReference in NavigationFrames.ToArray())
        {
            if (!weakReference.TryGetTarget(out var _))
            {
                NavigationFrames.Remove(weakReference);
            }
        }

        NavigationFrames.AddFirst(new WeakReference<INavigationFrame>(navigationFrame));
    }

    public INavigationFrame GetNavigationFrame(string frameName)
    {
        foreach (var weakReference in NavigationFrames.ToArray())
        {
            if (weakReference.TryGetTarget(out var navigationFrame))
            {
                if (Equals(frameName, navigationFrame.FrameName))
                {
                    return navigationFrame;
                }
            }
            else
            {
                NavigationFrames.Remove(weakReference);
            }
        }

        throw new InvalidOperationException($"There is no NavigationFrame named '{frameName}'.");
    }
}