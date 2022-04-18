using Kamishibai.Xamarin.Forms.Mvvm;
using Moq;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests.Mvvm
{
	public class PopAsyncFixture
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
