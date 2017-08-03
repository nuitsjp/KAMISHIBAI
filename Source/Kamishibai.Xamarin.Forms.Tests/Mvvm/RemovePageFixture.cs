using Kamishibai.Xamarin.Forms.Mvvm;
using Moq;
using Xamarin.Forms;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests.Mvvm
{
	public class RemovePageFixture
	{
		[Fact]
		public void Navigate()
		{
			var contentPageA = new ContentPageA();
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
		
		private class ContentPageA : ContentPage {}
		private class ContentPageB : ContentPage {}
		private class ContentPageC : ContentPage {}
	}
}
