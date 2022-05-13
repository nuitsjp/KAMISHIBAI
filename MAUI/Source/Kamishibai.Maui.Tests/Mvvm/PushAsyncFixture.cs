using Kamishibai.Maui.Mvvm;
using Moq;
using Microsoft.Maui.Controls;
using Xunit;

namespace Kamishibai.Maui.Tests.Mvvm
{
    public class PushAsyncFixture : TestFixture
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