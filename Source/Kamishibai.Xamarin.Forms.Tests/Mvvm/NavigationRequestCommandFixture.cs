using Kamishibai.Xamarin.Forms.Mvvm;
using Moq;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests.Mvvm
{
	public class NavigationRequestCommandFixture
	{
		[Fact]
		public void DefaultConstructor()
		{
			var command = new NavigationRequestCommand();
			var actionMock = new Mock<INavigationAction>();
			command.NavigationAction = actionMock.Object;

			Assert.True(command.CanExecute());
			Assert.True(command.CanExecute(new object()));

			command.Execute();
			actionMock.Verify(m => m.Navigate<object>(null), Times.Once);

			var parameter = new object();
			command.Execute(parameter);
			actionMock.Verify(m => m.Navigate(parameter), Times.Once);
		}

		[Fact]
		public void Constructor_WithNonParameterAction()
		{
			var called = false;
			var command = new NavigationRequestCommand(() => called = true);
			var actionMock = new Mock<INavigationAction>();
			command.NavigationAction = actionMock.Object;

			Assert.True(command.CanExecute());
			
			var parameter = new object();

			command.Execute(parameter);
			actionMock.Verify(m => m.Navigate(parameter), Times.Once);
			Assert.True(called);
		}

		[Fact]
		public void Constructor_WithNonParameterActionAndFunc()
		{
			var called = false;
			var command = new NavigationRequestCommand(() => called = true, () => true);
			var actionMock = new Mock<INavigationAction>();
			command.NavigationAction = actionMock.Object;

			Assert.True(command.CanExecute());

			command.Execute();
			actionMock.Verify(m => m.Navigate((object)null), Times.Once);
			Assert.True(called);
		}

		[Fact]
		public void Constructor_WithAction()
		{
			object actionParameter = null;
			var command = new NavigationRequestCommand(args => actionParameter = args);
			var actionMock = new Mock<INavigationAction>();
			command.NavigationAction = actionMock.Object;

			Assert.True(command.CanExecute());

			var parameter = new object();

			command.Execute(parameter);
			actionMock.Verify(m => m.Navigate(parameter), Times.Once);
			Assert.Equal(parameter, actionParameter);
		}

		[Fact]
		public void Constructor_WithActionAndFunc()
		{
			object funcParameter = null;
			var command = new NavigationRequestCommand(_ => { }, args =>
			{
				funcParameter = args;
				return false;
			});

			var parameter = new object();
			Assert.False(command.CanExecute(parameter));
			Assert.Equal(parameter, funcParameter);
		}
	}
}
