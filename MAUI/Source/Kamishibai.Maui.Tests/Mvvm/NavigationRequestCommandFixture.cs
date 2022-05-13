using System;
using Kamishibai.Maui.Mvvm;
using Moq;
using Xunit;

namespace Kamishibai.Maui.Tests.Mvvm
{
	public class NavigationRequestCommandFixture : TestFixture
	{
		[Fact]
		public void DefaultConstructor()
		{
			var command = new NavigationRequestCommand();
			var actionMock = new Mock<INavigationAction>();
			command.NavigationAction = actionMock.Object;

			Assert.True(command.CanExecute(new object()));

			command.Execute(new object());
			actionMock.Verify(m => m.Navigate<object>(null), Times.Once);
		}

		[Fact]
		public void Constructor_WithNonParameterAction()
		{
			var called = false;
			var command = new NavigationRequestCommand(() => called = true);
			var actionMock = new Mock<INavigationAction>();
			command.NavigationAction = actionMock.Object;

			Assert.True(command.CanExecute(new object()));
			
			var parameter = new object();

			command.Execute(parameter);
			actionMock.Verify(m => m.Navigate((object)null), Times.Once);
			Assert.True(called);
		}

		[Fact]
		public void Constructor_WithNonParameterActionAndFunc()
		{
			var called = false;
			var command = new NavigationRequestCommand(() => called = true, () => true);
			var actionMock = new Mock<INavigationAction>();
			command.NavigationAction = actionMock.Object;

			Assert.True(command.CanExecute(new object()));

			command.Execute(new object());
			actionMock.Verify(m => m.Navigate((object)null), Times.Once);
			Assert.True(called);
		}

		[Fact]
		public void Constructor_WithAction()
		{
			var called = false;
			var command = new NavigationRequestCommand(() => called = true);
			var actionMock = new Mock<INavigationAction>();
			command.NavigationAction = actionMock.Object;

			Assert.True(command.CanExecute(new object()));

			var parameter = new object();

			command.Execute(parameter);
			actionMock.Verify(m => m.Navigate((object)null), Times.Once);
			Assert.True(called);
		}

		[Fact]
		public void Constructor_WithActionAndFunc()
		{
		    var command = new NavigationRequestCommand(() => { }, () => false);
			Assert.False(command.CanExecute(new object()));
		}

	    [Fact]
	    public void Constructor_WhenExecuteIsNull()
	    {
	        Assert.Throws<ArgumentNullException>(() => new NavigationRequestCommand(null));
	    }

	    [Fact]
	    public void Constructor_WhenCanExecuteIsNull()
	    {
	        Assert.Throws<ArgumentNullException>(() => new NavigationRequestCommand(() => { }, null));
	    }

        [Fact]
	    public void ChangeCanExecute()
	    {
	        var command = new NavigationRequestCommand();
	        var called = false;

	        command.ChangeCanExecute(); // Exception does not occur.

            command.CanExecuteChanged += (sender, args) => called = true;

	        command.ChangeCanExecute();

            Assert.True(called);
	    }
    }
}
