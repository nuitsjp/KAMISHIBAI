using System;
using System.Linq;
using Microsoft.Maui.Controls;

namespace Kamishibai.Maui
{
    public class LifecycleNotifier<TTarget, TParam> : ILifecycleNotifier<TParam> where TTarget : class 
    {
        private readonly Action<TTarget, TParam> _action;

        private static bool IsTopDown => !(typeof(TTarget) == typeof(IPageUnloadedAware)
                                           || typeof(TTarget) == typeof(IPageClosedAware)
                                           || typeof(TTarget) == typeof(IApplicationOnSleepAware));

        public LifecycleNotifier(Action<TTarget, TParam> action)
        {
            _action = action;
        }

        public void Notify(Page page, TParam param = default(TParam))
        {
            if (IsTopDown)
            {
                NotifyToPage(page, param);
                NotifyToViewModel(page, param);
                
                Visit(page, param);
            }
            else
            {
                Visit(page, param);
                
                NotifyToViewModel(page, param);
                NotifyToPage(page, param);
            }
        }

        private void NotifyToPage(Page page, TParam param)
        {
            var pageAsT = page as TTarget;
            if (pageAsT != null)
            {
                _action(pageAsT, param);
            }
        }

        private void NotifyToViewModel(Page page, TParam param)
        {
            var parent = page.GetParentPage();
            if (parent?.BindingContext != page.BindingContext)
            {
                var viewModelAsT = page.BindingContext as TTarget;
                if (viewModelAsT != null)
                {
                    _action(viewModelAsT, param);
                }
            }
        }

        public void Visit(Page page, TParam param)
        {
            if (page is NavigationPage navigationPage)
            {
                Visit(navigationPage, param);
            }
            else if (page is FlyoutPage FlyoutPage)
            {
                Visit(FlyoutPage, param);
            }
            else if (page is TabbedPage tabbedPage)
            {
                Visit(tabbedPage, param);
            }
        }

        private void Visit(NavigationPage page, TParam param)
        {
            if (typeof(TTarget) == typeof(IPageLoadedAware)
                || typeof(TTarget) == typeof(IPageUnloadedAware))
            {
                Notify(page.CurrentPage, param);
            }
            else if (typeof(TTarget) == typeof(IPageClosedAware)
                || typeof(TTarget) == typeof(IApplicationOnSleepAware))
            {
                foreach (var child in page.Navigation.NavigationStack.Reverse())
                {
                    Notify(child, param);
                }
            }
            else
            {
                foreach (var child in page.Navigation.NavigationStack)
                {
                    Notify(child, param);
                }
            }
        }

        private void Visit(FlyoutPage page, TParam param)
        {
            if (IsTopDown)
            {
                Notify(page.Flyout, param);
                Notify(page.Detail, param);
            }
            else
            {
                Notify(page.Detail, param);
                Notify(page.Flyout, param);
            }
        }

        private void Visit(TabbedPage page, TParam param)
        {
            if (typeof(TTarget) == typeof(IPageLoadedAware)
                || typeof(TTarget) == typeof(IPageUnloadedAware))
            {
                Notify(page.CurrentPage, param);
            }
            else if (typeof(TTarget) == typeof(IPageClosedAware)
                     || typeof(TTarget) == typeof(IApplicationOnSleepAware))
            {
                foreach (var pageChild in page.Children.Reverse())
                {
                    Notify(pageChild, param);
                }
            }
            else
            {
                foreach (var pageChild in page.Children)
                {
                    Notify(pageChild, param);
                }
            }
        }
    }
}
