using Kamishibai.Xamarin.Forms.Mvvm;
using Moq;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests.Mvvm
{
    public class PopToRootAsyncFixture
    {
        [Fact]
        public void Navigate()
        {
            var popToRootAsync = new PopToRootAsync();
            var navigatorMock = new Mock<INavigator>();
            popToRootAsync.Navigator = navigatorMock.Object;

            var parameter = "Hello, Parameter!";
            popToRootAsync.Navigate(parameter);
			
            navigatorMock.Verify(m => m.PopToRootAsync(true), Times.Once);
        }
    }
}