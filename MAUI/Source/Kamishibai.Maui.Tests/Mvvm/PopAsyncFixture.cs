using Kamishibai.Maui.Mvvm;
using Moq;
using Xunit;

namespace Kamishibai.Maui.Tests.Mvvm
{
	public class PopAsyncFixture : TestFixture
	{
		[Fact]
		public void Navigate()
		{
			var popAsync = new PopAsync();
		    var navigatorMock = new Mock<INavigator>();
		    popAsync.Navigator = navigatorMock.Object;

			var parameter = "Hello, Parameter!";
			popAsync.Navigate(parameter);
			
			navigatorMock.Verify(m => m.PopAsync(true), Times.Once);
		}
	}
}
