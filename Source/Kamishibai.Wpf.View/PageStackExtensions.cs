namespace Kamishibai.Wpf.View;

internal static class PageStackExtensions
{
    public static T Current<T>(this IList<T> list) => list[^1];

    public static T PopCurrent<T>(this IList<T> list)
    {
        var last = list.Current();
        list.RemoveAt(list.Count - 1);
        return last;
    }

    public static T Previous<T>(this IList<T> list) => list[^2];
}