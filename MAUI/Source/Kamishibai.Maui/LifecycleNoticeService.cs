using System;
using System.Linq;
using Microsoft.Maui.Controls;

namespace Kamishibai.Maui
{
    public static class LifecycleNoticeService
    {
        public static void OnInitialize(this Page page)
        {
            page.Notify<IPageInitializeAware, object>((target, param) => target.OnInitialize());
        }
        public static void OnInitialize<TParam>(this Page page, TParam parameter)
        {
            page.Notify<IPageInitializeAware<TParam>, TParam>((target, param) => target.OnInitialize(param), parameter);
        }
        public static void OnLoaded(this Page page)
        {
            page.Notify<IPageLoadedAware, object>((target, param) => target.OnLoaded());
        }

        public static void OnUnloaded(this Page page)
        {
            page.Notify<IPageUnloadedAware, object>((target, param) => target.OnUnloaded());
        }

        public static void OnClosed(this Page page)
        {
            page.Notify<IPageClosedAware, object>((target, param) => target.OnClosed());
        }

        public static void OnResume(Application application)
        {
            var notifier =
                new LifecycleNotifier<IApplicationOnResumeAware, object>((target, param) => target.OnResume());
            notifier.Notify(application.MainPage);
            foreach (var page in application.MainPage.Navigation.ModalStack)
            {
                notifier.Notify(page);
            }
        }

        public static void OnSleep(Application application)
        {
            var notifier =
                new LifecycleNotifier<IApplicationOnSleepAware, object>((target, param) => target.OnSleep());
            foreach (var page in application.MainPage.Navigation.ModalStack.Reverse())
            {
                notifier.Notify(page);
            }
            notifier.Notify(application.MainPage);
        }

        private static void Notify<TTarget, TParam>(this Page page, Action<TTarget, TParam> action, TParam param = default(TParam)) where TTarget : class
        {
            new LifecycleNotifier<TTarget, TParam>(action).Notify(page, param);
        }
    }
}
