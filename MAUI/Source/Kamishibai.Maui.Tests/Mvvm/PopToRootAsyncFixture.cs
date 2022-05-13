using Kamishibai.Maui.Mvvm;
using Moq;
using Xunit;

namespace Kamishibai.Maui.Tests.Mvvm
{
    public class PopToRootAsyncFixture : TestFixture
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