using System.Windows;
using System.Windows.Media;

namespace Kamishibai.Wpf.View;

public static class DependencyObjectExtensions
{
    public static T? GetChildOfType<T>(this DependencyObject depObj)
        where T : DependencyObject
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        {
            var child = VisualTreeHelper.GetChild(depObj, i);

            var result = (child as T) ?? GetChildOfType<T>(child);
            if (result != null) return result;
        }

        return null;
    }
}