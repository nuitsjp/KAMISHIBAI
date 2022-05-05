using Driver.Windows;

// ReSharper disable once CheckNamespace
namespace Scenario;

public static class NavigationFrameExtensions
{
    public static NavigationFrameDriverAssertions Should(this NavigationFrameDriver instance)
    {
        return new NavigationFrameDriverAssertions(instance);
    }
}