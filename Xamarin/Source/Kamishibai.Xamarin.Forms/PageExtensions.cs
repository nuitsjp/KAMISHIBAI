using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms
{
    public static class PageExtensions
    {
        public static Page GetParentPage(this Page current, bool incloudCurrent = false)
        {
            return current.GetParentPage<Page>(incloudCurrent);
        }

        public static TPage GetParentPage<TPage>(this Page current, bool incloudCurrent = false) where TPage : Page
        {
            var element = incloudCurrent ? current : current?.Parent;
            do
            {
                if (element is TPage result) return result;

                element = element?.Parent;
            } while (element != null);
            return null;
        }
    }
}
