using Kamishibai.Xamarin.Forms.Mvvm;
using Moq;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests.Mvvm
{
    public class PopModalAsyncFixture
    {
        [Fact]
        public void Navigate()
        {
			
            var popModalAsync = new PopModalAsync();
            var navigatorMock = new Mock<INavigator>();
            popModalAsync.Navigator = navigatorMock.Object;

            var parameter = "Hello, Parameter!";
            popModalAsync.Navigate(parameter);
			
            navigatorMock.Verify(m => m.PopModalAsync(true), Times.Once);
        }
    }
}