using Kamishibai.Maui.Mvvm;
using Moq;
using Microsoft.Maui.Controls;
using Xunit;

namespace Kamishibai.Maui.Tests.Mvvm
{
	public class RemovePageFixture
	{
		[Fact]
		public void Navigate()
		{
			var contentPageA = new ContentPageA();
		    // ReSharper disable once UnusedVariable
			var navigationPage = new NavigationPage(contentPageA);
			var contentPageB = new ContentPageB();
			contentPageA.Navigation.PushAsync(contentPageB);
			var contentPageC = new ContentPageC();
			contentPageB.Navigation.PushAsync(contentPageC);
			
			var removePage = new RemovePage<ContentPageB>();
		    var navigatorMock = new Mock<INavigator>();
		    removePage.Navigator = navigatorMock.Object;
		    contentPageC.Behaviors.Add(removePage);

            var parameter = "Hello, Parameter!";
			removePage.Navigate(parameter);
			
			navigatorMock.Verify(m => m.RemovePage(contentPageB), Times.Once);
		}

	    [Fact]
	    public void Navigate_NotExistTarget()
	    {
	        var contentPageA = new ContentPageA();
	        // ReSharper disable once UnusedVariable
	        var navigationPage = new NavigationPage(contentPageA);
	        var contentPageB = new ContentPageB();
	        contentPageA.Navigation.PushAsync(contentPageB);

	        var removePage = new RemovePage<ContentPageC>();
	        var navigatorMock = new Mock<INavigator>();
	        removePage.Navigator = navigatorMock.Object;
	        contentPageB.Behaviors.Add(removePage);

	        var parameter = "Hello, Parameter!";
	        removePage.Navigate(parameter);

	        navigatorMock.Verify(m => m.RemovePage(It.IsAny<Page>()), Times.Never);
	    }

		private class ContentPageA : ContentPage {}
		private class ContentPageB : ContentPage {}
		private class ContentPageC : ContentPage {}
	}
}
