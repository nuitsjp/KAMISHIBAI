using Kamishibai.Maui.Mvvm;
using Moq;
using Xunit;

namespace Kamishibai.Maui.Tests.Mvvm
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