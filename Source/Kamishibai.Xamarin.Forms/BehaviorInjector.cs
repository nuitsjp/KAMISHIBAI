using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms
{
    public class BehaviorInjector : IBehaviorInjector
    {
        public void Inject(Page page)
        {
            if (page is NavigationPage navigationPage)
            {
                InjectionBehavior<NavigationPageBehavior>(page);
                Injects(navigationPage.Navigation.NavigationStack);
            }
            else if (page is TabbedPage tabbedPage)
            {
                InjectionBehavior<MultiPageBehavior<Page>>(page);
                Injects(tabbedPage.Children);
            }
            else if (page is CarouselPage)
            {
                InjectionBehavior<MultiPageBehavior<ContentPage>>(page);
            }
        }

        private void Injects(IEnumerable<Page> pages)
        {
            foreach (var page in pages)
            {
                Inject(page);
            }
        }

        private static void InjectionBehavior<TBehavior>(Page page) where TBehavior : Behavior
        {
            foreach (var behavior in page.Behaviors)
            {
                if (behavior is TBehavior)
                {
                    return;
                }
            }
            page.Behaviors.Add(Activator.CreateInstance<TBehavior>());
        }
    }
}