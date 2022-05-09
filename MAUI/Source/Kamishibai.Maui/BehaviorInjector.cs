using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace Kamishibai.Maui
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