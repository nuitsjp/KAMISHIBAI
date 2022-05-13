using Kamishibai.Maui.Mvvm;
using Moq;
using Microsoft.Maui.Controls;
using Xunit;

namespace Kamishibai.Maui.Tests.Mvvm
{
	public class SetMainPageFixture : TestFixture
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
