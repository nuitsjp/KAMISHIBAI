using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms
{
    public class NavigationPageBehavior : BehaviorBase<NavigationPage>
    {
        protected override void OnAttachedTo(NavigationPage navigationPage)
        {
            base.OnAttachedTo(navigationPage);
            navigationPage.Popped += OnPopped;
        }

        protected override void OnDetachingFrom(NavigationPage navigationPage)
        {
            navigationPage.Popped -= OnPopped;
            base.OnDetachingFrom(navigationPage);
        }

        private void OnPopped(object sender, NavigationEventArgs navigationEventArgs)
        {
            AssociatedObject.CurrentPage?.OnLoaded();
            navigationEventArgs.Page?.OnClosed();
        }
    }
}
