using System;
using System.Threading.Tasks;
using Kamishibai.Maui.Mvvm;
using Microsoft.Maui.Controls;
using Xunit;

namespace Kamishibai.Maui.Tests.Mvvm
{
	public class AnimatableNavigationFixture : TestFixture
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
