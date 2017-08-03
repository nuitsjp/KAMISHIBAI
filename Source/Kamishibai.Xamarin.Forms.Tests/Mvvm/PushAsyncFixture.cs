using Kamishibai.Xamarin.Forms.Mvvm;
using Moq;
using Xamarin.Forms;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests.Mvvm
{
    public class PushAsyncFixture
    {
        [Fact]
        public void Navigate()
        {
            var pushAsync = new PushAsync<ContentPage>();
            var navigatorMock = new Mock<INavigator>();
            pushAsync.Navigator = navigatorMock.Object;

            var parameter = "Hello, Parameter!";
            pushAsync.Navigate(parameter);
			
            navigatorMock.Verify(m => m.PushAsync(It.IsAny<ContentPage>(), parameter, true), Times.Once);
        }
    }
}