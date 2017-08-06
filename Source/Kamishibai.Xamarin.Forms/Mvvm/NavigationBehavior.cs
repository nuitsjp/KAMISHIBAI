using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms.Mvvm
{
    public abstract class NavigationBehavior<TPage> : BehaviorBase<Page>, INavigationAction where TPage : Page, new ()
    {
        public static readonly BindableProperty RequestProperty =
            BindableProperty.Create(
                nameof(Request),
                typeof(INavigationRequest),
                typeof(NavigationBehavior<TPage>),
                propertyChanged: NavigationRequestPropertyChanged);

	    private INavigator _navigator;

	    public INavigationRequest Request
        {
            get => (INavigationRequest)GetValue(RequestProperty);
            set => SetValue(RequestProperty, value);
        }

	    public INavigator Navigator
	    {
		    get => _navigator ?? (_navigator = new Navigator(AssociatedObject));
		    set => _navigator = value;
	    }

	    public Page CurrentPage => AssociatedObject;

        private static void NavigationRequestPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var navigationAction = (NavigationBehavior<TPage>)bindable;
                ((INavigationRequest)newValue).NavigationAction = navigationAction;
            }
            if (oldValue != null)
            {
                ((INavigationRequest)oldValue).NavigationAction = null;
            }
        }

        public abstract Task Navigate<T>(T parameter = default(T));
    }
}
