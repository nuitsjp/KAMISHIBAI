using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Kamishibai.Maui
{
    public class Navigator : INavigator
    {
        private readonly Page _currentPage;

        private INavigation Navigation { get; }
        
        internal IBehaviorInjector BehaviorInjector { get; set; } = new BehaviorInjector();

        public IReadOnlyList<Page> ModalStack => Navigation.ModalStack;
        public IReadOnlyList<Page> NavigationStack => Navigation.NavigationStack;

        public Navigator(Page page) : this(page, page.Navigation)
        {
        }

	    internal Navigator(Page page, INavigation navigation)
	    {
		    _currentPage = page;
		    Navigation = navigation;
	    }

		public void InsertPageBefore(Page page, Page before)
        {
            InsertPageBefore<object>(page, before);
        }

        public void InsertPageBefore<TParam>(Page page, Page before, TParam parameter = default(TParam))
        {
            page.OnInitialize(parameter);
            Navigation.InsertPageBefore(page, before);
        }
        public Task<Page> PopAsync(bool animated = true)
        {
            return Navigation.PopAsync(animated);
        }

        public Task<Page> PopModalAsync(bool animated = true)
        {
            return Navigation.PopModalAsync(animated);
        }

        public async Task PopToRootAsync(bool animated = true)
        {
            var navigationPage = _currentPage.Parent as NavigationPage;
            if (navigationPage != null)
            {
                var pages = _currentPage.Navigation.NavigationStack.ToList();

                await Navigation.PopToRootAsync(animated);

                navigationPage.CurrentPage.OnLoaded();
                pages.RemoveAt(0);
                pages.Reverse();
                foreach (var page in pages)
                {
                    page.OnClosed();
                }
            }
        }

        public async Task PushAsync(Page page, bool animated = true)
        {
            page.OnInitialize();

            BehaviorInjector.Inject(page);
            await Navigation.PushAsync(page, animated);

            page.OnLoaded();
            _currentPage.OnUnloaded();
        }
        
        public async Task PushAsync<TParam>(Page page, TParam parameter = default(TParam), bool animated = true)
        {
            page.OnInitialize(parameter);

            BehaviorInjector.Inject(page);
            await Navigation.PushAsync(page, animated);
            
            page.OnLoaded();
            _currentPage.OnUnloaded();
        }

        public async Task PushModalAsync(Page page, bool animated = true)
        {
            page.OnInitialize();

            BehaviorInjector.Inject(page);
            await Navigation.PushModalAsync(page, animated);

            page.OnLoaded();
            _currentPage.OnUnloaded();
        }
        public async Task PushModalAsync<TParam>(Page page, TParam parameter = default(TParam), bool animated = true)
        {
            page.OnInitialize(parameter);

            BehaviorInjector.Inject(page);
            await Navigation.PushModalAsync(page, animated);

            page.OnLoaded();
            _currentPage.OnUnloaded();
        }

        public void RemovePage(Page page)
        {
            var needToCall = Navigation.NavigationStack.Last() != page;
            Navigation.RemovePage(page);
            
            if(needToCall) page.OnClosed();
        }
    }
}
