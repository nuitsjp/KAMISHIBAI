using System;
using System.Linq;
using System.Threading.Tasks;
using Kamishibai.Maui.Mvvm;
using Microsoft.Maui.Controls;
using Xunit;

namespace Kamishibai.Maui.Tests.Mvvm
{
	public class NavigationBehaviorFixture
	{
		[Fact]
		public void OnAttachedTo()
		{
			var contentPage1 = new ContentPage();
			var navigationBehavior = new NavigationBehaviorMock<ContentPage>();
			contentPage1.Behaviors.Add(navigationBehavior);
			
			Assert.NotNull(navigationBehavior.Navigator);

			var contentPage2 = new ContentPage();
			navigationBehavior.Navigator.PushModalAsync(contentPage2);
			
			Assert.Equal(1, contentPage1.Navigation.ModalStack.Count);
			Assert.Equal(contentPage2, contentPage1.Navigation.ModalStack.Single());
		}

	    [Fact]
	    public void Navigate()
	    {
	        var navigationRequest = new NavigationRequest();
	        var navigationActionMock = new NavigationBehaviorMock<ContentPage> { Request = navigationRequest };

            navigationRequest.RaiseAsync();

	        Assert.Equal(navigationRequest, navigationActionMock.Request);

	        Assert.True(navigationActionMock.Navigated);
	        Assert.Equal(typeof(object), navigationActionMock.ParameterType);
	        Assert.Null(navigationActionMock.Parameter);
	    }

	    [Fact]
	    public void Navigate_WithParameter()
	    {
	        var navigationRequest = new NavigationRequest<string>();
	        var navigationActionMock = new NavigationBehaviorMock<ContentPage> { Request = navigationRequest };

            var parameter = "Hello, Parameter!";
	        navigationRequest.RaiseAsync(parameter);

	        Assert.Equal(navigationRequest, navigationActionMock.Request);

	        Assert.True(navigationActionMock.Navigated);
	        Assert.Equal(typeof(string), navigationActionMock.ParameterType);
	        Assert.Equal(parameter, navigationActionMock.Parameter);
	    }

	    [Fact]
	    public async void NavigationRequestPropertyChanged_ToNull()
	    {
	        var navigationRequest = new NavigationRequest();
	        var navigationActionMock = new NavigationBehaviorMock<ContentPage> { Request = navigationRequest };

	        navigationActionMock.Request = null;
	        Assert.Null(navigationActionMock.Request);

            await Assert.ThrowsAsync<InvalidOperationException>(() => navigationRequest.RaiseAsync());


	        Assert.False(navigationActionMock.Navigated);
	        Assert.Null(navigationActionMock.ParameterType);
	        Assert.Null(navigationActionMock.Parameter);
        }


        private class NavigationBehaviorMock<TNavigatePage> : NavigationBehavior<TNavigatePage> where TNavigatePage : Page, new()
	    {
	        public bool Navigated { get; private set; }
	        public Type ParameterType { get; private set; }
	        public object Parameter { get; private set; }
	        public override Task Navigate<T>(T parameter = default(T))
	        {
	            Navigated = true;
	            ParameterType = typeof(T);
	            Parameter = parameter;
	            return Task.FromResult(true);
	        }
	    }
	}
}
