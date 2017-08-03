using System;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms
{
    public class MultiPageBehavior<TPage> : BehaviorBase<MultiPage<TPage>> where TPage : Page
    {
        protected TPage PreviousPage { get; set; }
        protected override void OnAttachedTo(MultiPage<TPage> bindableObject)
        {
            base.OnAttachedTo(bindableObject);
            PreviousPage = AssociatedObject.CurrentPage;
            AssociatedObject.CurrentPageChanged += OnCurrentPageChanged;
            AssociatedObject.PagesChanged += OnPagesChanged;
        }

        protected override void OnDetachingFrom(MultiPage<TPage> bindableObject)
        {
            AssociatedObject.PagesChanged -= OnPagesChanged;
            AssociatedObject.CurrentPageChanged -= OnCurrentPageChanged;
            base.OnDetachingFrom(bindableObject);
        }

        private void OnCurrentPageChanged(object sender, EventArgs eventArgs)
        {
            AssociatedObject.CurrentPage?.OnLoaded();
            PreviousPage?.OnUnloaded();
            PreviousPage = AssociatedObject.CurrentPage;
        }

        private void OnPagesChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
	        if (eventArgs.NewItems != null)
	        {
		        foreach (var newItem in eventArgs.NewItems)
		        {
			        (newItem as Page)?.OnInitialize((object)null);
		        }
	        }
            if (eventArgs.OldItems != null)
            {
	            foreach (var oldItem in eventArgs.OldItems)
	            {
		            (oldItem as Page)?.OnClosed();
	            }
            }
        }
    }
}
