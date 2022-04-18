using System;
using System.Threading.Tasks;

namespace Kamishibai.Xamarin.Forms.Mvvm
{
    public class NavigationRequest<TParam> : INavigationRequest<TParam>
    {
        private INavigationAction _navigationAction;

	    public INavigationAction NavigationAction
	    {
		    set => _navigationAction = value;
	    }

        Task INavigationRequest.RaiseAsync()
        {
            return RaiseAsync(default(TParam));
        }

        public Task RaiseAsync(TParam parameter)
        {
            if (_navigationAction == null) throw new InvalidOperationException("NavigationAction is null");
            return _navigationAction.Navigate(parameter);
        }
    }

    public class NavigationRequest : INavigationRequest
    {
        private INavigationAction _navigationAction;

        public INavigationAction NavigationAction
        {
            set => _navigationAction = value;
        }

        public Task RaiseAsync()
        {
            if(_navigationAction == null) throw new InvalidOperationException("NavigationAction is null");
            return _navigationAction.Navigate<object>();
        }
    }

}
