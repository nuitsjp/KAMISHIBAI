using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms
{
    public class BehaviorInjector : IBehaviorInjector
    {
        public void Inject(Page page)
        {
            switch (page)
            {
                case NavigationPage navigationPage:
                    InjectionBehavior<NavigationPageBehavior>(page);
                    Injects(navigationPage.Navigation.NavigationStack);
                    break;
                case TabbedPage tabbedPage:
                    InjectionBehavior<MultiPageBehavior<Page>>(page);
                    Injects(tabbedPage.Children);
                    break;
                case CarouselPage _:
                    InjectionBehavior<MultiPageBehavior<ContentPage>>(page);
                    break;
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