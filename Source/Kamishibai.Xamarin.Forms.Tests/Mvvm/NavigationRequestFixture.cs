using System;
using Kamishibai.Xamarin.Forms.Mvvm;
using Moq;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests.Mvvm
{
	public class NavigationRequestFixture
	{
		[Fact]
		public void RaiseAsync()
		{
			var navigationRequest = new NavigationRequest();
			var actionMock = new Mock<INavigationAction>();
			navigationRequest.NavigationAction = actionMock.Object;
			navigationRequest.RaiseAsync();
			
			actionMock.Verify(m => m.Navigate<object>(null), Times.Once);
		}

	    [Fact]
	    public async void RaiseAsync_WhenNavigationActionIsNull()
	    {
	        var navigationRequest = new NavigationRequest();
	        await Assert.ThrowsAsync<InvalidOperationException>(() => navigationRequest.RaiseAsync());
	    }

		[Fact]
		public void RaiseAsync_WithParameter()
		{
			var navigationRequest = new NavigationRequest<string>();
			var actionMock = new Mock<INavigationAction>();
			navigationRequest.NavigationAction = actionMock.Object;
			var parameter = "Hello, Parameter!";
			navigationRequest.RaiseAsync(parameter);

			actionMock.Verify(m => m.Navigate(parameter), Times.Once);
		}

	    [Fact]
	    public void RaiseAsync_WithNullParameter()
	    {
	        var navigationRequest = new NavigationRequest<DateTime>();
	        var actionMock = new Mock<INavigationAction>();
	        navigationRequest.NavigationAction = actionMock.Object;
	        ((INavigationRequest)navigationRequest).RaiseAsync();

	        actionMock.Verify(m => m.Navigate(default(DateTime)), Times.Once);
	    }

	    [Fact]
	    public async void RaiseAsync_WithParameter_WhenNavigationActionIsNull()
	    {
	        var navigationRequest = new NavigationRequest<string>();
	        await Assert.ThrowsAsync<InvalidOperationException>(() => navigationRequest.RaiseAsync("Hello, Parameter!"));
	    }
	}
}
