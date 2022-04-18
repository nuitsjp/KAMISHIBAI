using System;
using Kamishibai.Xamarin.Forms.Mvvm;
using Moq;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests.Mvvm
{
	public class NavigationRequestCommandTFixture
	{
		[Fact]
		public void DefaultConstructor()
		{
			var command = new NavigationRequestCommand<string>();
			var actionMock = new Mock<INavigationAction>();
			command.NavigationAction = actionMock.Object;

			var parameter = "Hello, Parameter!";

			Assert.True(command.CanExecute());
			Assert.True(command.CanExecute(parameter));
			Assert.True(command.CanExecute((object)parameter));
			Assert.True(command.CanExecute(new object()));

			command.Execute();
			actionMock.Verify(m => m.Navigate<string>(null), Times.Once);

			command.Execute(new object());
			actionMock.Verify(m => m.Navigate<string>(null), Times.Exactly(2));

			command.Execute(parameter);
			actionMock.Verify(m => m.Navigate(parameter), Times.Once);

			command.Execute((object)parameter);
			actionMock.Verify(m => m.Navigate(parameter), Times.Exactly(2));
		}

		[Fact]
		public void Constructor_WithNonParameterAction()
		{
			var called = false;
			var command = new NavigationRequestCommand<string>(() => called = true);
			var actionMock = new Mock<INavigationAction>();
			command.NavigationAction = actionMock.Object;

			Assert.True(command.CanExecute());
			
			var parameter = "Hello, Parameter!";

			command.Execute(parameter);
			actionMock.Verify(m => m.Navigate(parameter), Times.Once);
			Assert.True(called);
		}

		[Fact]
		public void Constructor_WithNonParameterActionAndFunc()
		{
			var called = false;
			var command = new NavigationRequestCommand<string>(() => called = true, () => true);
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
			string actionParameter = null;
			var command = new NavigationRequestCommand<string>(args => actionParameter = args);
			var actionMock = new Mock<INavigationAction>();
			command.NavigationAction = actionMock.Object;

			Assert.True(command.CanExecute());

			var parameter = "Hello, Parameter!";

			command.Execute(parameter);
			actionMock.Verify(m => m.Navigate(parameter), Times.Once);
			Assert.Equal(parameter, actionParameter);
		}

		[Fact]
		public void Constructor_WithActionAndFunc()
		{
			string funcParameter = null;
			var command = new NavigationRequestCommand<string>(_ => { }, args =>
			{
				funcParameter = args;
				return false;
			});

			var parameter = "Hello, Parameter!";
			Assert.False(command.CanExecute(parameter));
			Assert.Equal(parameter, funcParameter);
		}

		[Fact]
		public void ChangeCanExecute()
		{
			var command = new NavigationRequestCommand<string>();
			var called = false;
		    command.ChangeCanExecute(); // Exception does not occur.

            command.CanExecuteChanged += (sender, args) => called = true;
			command.ChangeCanExecute();
			Assert.True(called);
		}

	    [Fact]
	    public void Constructor_WhenExecuteIsNull()
	    {
	        Assert.Throws<ArgumentNullException>(() => new NavigationRequestCommand<string>((Action)null));
	        Assert.Throws<ArgumentNullException>(() => new NavigationRequestCommand<string>(null, () => true));
	    }

        [Fact]
	    public void Constructor_WhenCanExecuteIsNull()
	    {
	        Assert.Throws<ArgumentNullException>(() => new NavigationRequestCommand<string>(() => { }, null));
	    }

	    [Fact]
	    public void Constructor_WhenExecuteWithTypeParameterIsNull()
	    {
	        Assert.Throws<ArgumentNullException>(() => new NavigationRequestCommand<string>((Action<string>)null));
	    }

	    [Fact]
	    public void Constructor_WhenCanExecuteWithTypeParameterIsNull()
	    {
	        Assert.Throws<ArgumentNullException>(() => new NavigationRequestCommand<string>(_ => { }, null));
	    }
    }
}
