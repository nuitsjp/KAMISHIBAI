using System;
using System.Threading.Tasks;
using Kamishibai.Xamarin.Forms.Mvvm;
using Xamarin.Forms;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests.Mvvm
{
	public class AnimatableNavigationFixture
	{
		[Fact]
		public void Animated()
		{
			var mock = new AnimatableNavigationMock();
			Assert.True(mock.Animated);
			mock.Animated = false;
			Assert.False(mock.Animated);
		}
		private class AnimatableNavigationMock : AnimatableNavigation<ContentPage>
		{
			public override Task Navigate<T>(T parameter = default(T))
			{
				throw new NotImplementedException();
			}
		}
	}
}
