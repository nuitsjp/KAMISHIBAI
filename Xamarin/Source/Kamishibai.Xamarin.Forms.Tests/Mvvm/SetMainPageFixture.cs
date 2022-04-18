using Kamishibai.Xamarin.Forms.Mvvm;
using Moq;
using Xamarin.Forms;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests.Mvvm
{
	public class SetMainPageFixture
	{
		[Fact]
		public void Navigate()
		{
		    var mock = ApplicationServiceFixture.ApplicationMock;
            var setMainPage = new SetMainPage<TestContentPage>();
		    var navigatorMock = new Mock<INavigator>();
		    setMainPage.Navigator = navigatorMock.Object;

		    var parameter = "Hello, Parameter!";
		    setMainPage.Navigate(parameter);

		    mock.VerifySet(m => m.MainPage = It.IsAny<TestContentPage>());
        }


        private class TestContentPage : ContentPage {}
	}
}
