using System.Collections.Generic;
using System.Threading.Tasks;
using Kamishibai.Xamarin.Forms.Tests.Mocks;
using Moq;
using Xamarin.Forms;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests
{
	public class NavigatorFixture
	{
	    [Fact]
	    public void ModalStack()
	    {
	        var contentPage = new ContentPage();
            var navigationMock = new Mock<INavigation>();
	        // ReSharper disable once CollectionNeverUpdated.Local
	        var modalStack = new List<Page>();
	        navigationMock.SetupGet(m => m.ModalStack).Returns(modalStack);

            var navigator = new Navigator(contentPage, navigationMock.Object);

            Assert.Equal(modalStack, navigator.ModalStack);
	    }

	    [Fact]
	    public void NavigationStack()
	    {
	        var contentPage = new ContentPage();
	        var navigationMock = new Mock<INavigation>();
	        // ReSharper disable once CollectionNeverUpdated.Local
	        var navigationStack = new List<Page>();
	        navigationMock.SetupGet(m => m.NavigationStack).Returns(navigationStack);

	        var navigator = new Navigator(contentPage, navigationMock.Object);

	        Assert.Equal(navigationStack, navigator.NavigationStack);
	    }

        [Fact]
		public void InsertPageBefore()
		{
			var eventRecoder = new EventRecorder();
			var contentPage1 = new ContentPageMock(eventRecoder);
			var navigationPage = new NavigationPage(contentPage1);
			var navigator = new Navigator(contentPage1);
			var contentPage2 = new ContentPageMock(eventRecoder);
			navigator.InsertPageBefore(contentPage2, contentPage1);
			
			Assert.Equal(2, navigationPage.Navigation.NavigationStack.Count);
			Assert.Equal(contentPage2, navigationPage.Navigation.NavigationStack[0]);
			Assert.Equal(contentPage1, navigationPage.Navigation.NavigationStack[1]);
			
			Assert.Equal(1, eventRecoder.Count);
			
			Assert.Equal(contentPage2, eventRecoder[0].Sender);
			Assert.Equal("OnInitialize", eventRecoder[0].CallerMemberName);
			Assert.Null(eventRecoder[0].Parameter);
		}

		[Fact]
		public void InsertPageBefore_WithParameter()
		{
			var eventRecoder = new EventRecorder();
			var contentPage1 = new ContentPageMock(eventRecoder);
			var navigationPage = new NavigationPage(contentPage1);
			var navigator = new Navigator(contentPage1);
			var contentPage2 = new ContentPageMock(eventRecoder);
			var parameter = new object();
			navigator.InsertPageBefore(contentPage2, contentPage1, parameter);

			Assert.Equal(2, navigationPage.Navigation.NavigationStack.Count);
			Assert.Equal(contentPage2, navigationPage.Navigation.NavigationStack[0]);
			Assert.Equal(contentPage1, navigationPage.Navigation.NavigationStack[1]);

			Assert.Equal(1, eventRecoder.Count);

			Assert.Equal(contentPage2, eventRecoder[0].Sender);
			Assert.Equal("OnInitialize", eventRecoder[0].CallerMemberName);
			Assert.Equal(parameter, eventRecoder[0].Parameter);
		}

		[Fact]
		public void PopAsync()
		{
			var navigationMock = new Mock<INavigation>();
			var navigator = new Navigator(null, navigationMock.Object);
			navigator.PopAsync();
			
			navigationMock.Verify(x => x.PopAsync(true), Times.Once);
		}

	    [Fact]
	    public void PopAsync_WhenAnimatedIsFalse()
	    {
	        var navigationMock = new Mock<INavigation>();
	        var navigator = new Navigator(null, navigationMock.Object);
	        navigator.PopAsync(false);

	        navigationMock.Verify(x => x.PopAsync(false), Times.Once);
	    }

        [Fact]
		public void PopModalAsync()
		{
			var navigationMock = new Mock<INavigation>();
			var navigator = new Navigator(null, navigationMock.Object);
			navigator.PopModalAsync();

			navigationMock.Verify(x => x.PopModalAsync(true), Times.Once);
		}

	    [Fact]
	    public void PopModalAsync_WhenAnimatedIsFalse()
	    {
	        var navigationMock = new Mock<INavigation>();
	        var navigator = new Navigator(null, navigationMock.Object);
	        navigator.PopModalAsync(false);

	        navigationMock.Verify(x => x.PopModalAsync(false), Times.Once);
	    }

        [Fact]
		public async Task PopToRootAsync()
		{
			var eventRecorder = new EventRecorder();
			var contentPageMock1 = new ContentPageMock(eventRecorder) { Title = "contentPageMock1" };
			var contentPageMock2 = new ContentPageMock(eventRecorder) { Title = "contentPageMock2" };
			var contentPageMock3 = new ContentPageMock(eventRecorder) { Title = "contentPageMock3" };
		    // ReSharper disable once UnusedVariable
			var navigationPage = new NavigationPageMock(contentPageMock1, eventRecorder);
			await contentPageMock1.Navigation.PushAsync(contentPageMock2);
			await contentPageMock2.Navigation.PushAsync(contentPageMock3);

			var navigationMock = new Mock<INavigation>();
			navigationMock
				.Setup(m => m.PopToRootAsync(true))
				.Returns(() => Task.FromResult(true))
				.Callback(() => contentPageMock3.Navigation.PopToRootAsync(true).Wait());

			var navigator = new Navigator(contentPageMock3, navigationMock.Object);
			await navigator.PopToRootAsync();

			navigationMock.Verify(x => x.PopToRootAsync(true), Times.Once);
			
			Assert.Equal(3, eventRecorder.Count);
			
			Assert.Equal(contentPageMock1, eventRecorder[0].Sender);
			Assert.Equal("OnLoaded", eventRecorder[0].CallerMemberName);
			Assert.Null(eventRecorder[0].Parameter);

			Assert.Equal(contentPageMock3, eventRecorder[1].Sender);
			Assert.Equal("OnClosed", eventRecorder[1].CallerMemberName);
			Assert.Null(eventRecorder[1].Parameter);

			Assert.Equal(contentPageMock2, eventRecorder[2].Sender);
			Assert.Equal("OnClosed", eventRecorder[2].CallerMemberName);
			Assert.Null(eventRecorder[2].Parameter);
		}

	    [Fact]
	    public async Task PopToRootAsync_WhenAnimatedIsFalse()
	    {
	        var eventRecorder = new EventRecorder();
	        var contentPageMock1 = new ContentPageMock(eventRecorder) { Title = "contentPageMock1" };
	        var contentPageMock2 = new ContentPageMock(eventRecorder) { Title = "contentPageMock2" };
	        var contentPageMock3 = new ContentPageMock(eventRecorder) { Title = "contentPageMock3" };
	        // ReSharper disable once UnusedVariable
	        var navigationPage = new NavigationPageMock(contentPageMock1, eventRecorder);
	        await contentPageMock1.Navigation.PushAsync(contentPageMock2);
	        await contentPageMock2.Navigation.PushAsync(contentPageMock3);

	        var navigationMock = new Mock<INavigation>();
	        navigationMock
	            .Setup(m => m.PopToRootAsync(false))
	            .Returns(() => Task.FromResult(false))
	            .Callback(() => contentPageMock3.Navigation.PopToRootAsync(false).Wait());

	        var navigator = new Navigator(contentPageMock3, navigationMock.Object);
	        await navigator.PopToRootAsync(false);

	        navigationMock.Verify(x => x.PopToRootAsync(false), Times.Once);
	    }

	    [Fact]
	    public async Task PopToRootAsync_WhenParentIsNotNavigationPage()
	    {
	        var eventRecorder = new EventRecorder();
	        var contentPageMock1 = new ContentPageMock(eventRecorder) { Title = "contentPageMock1" };
	        var contentPageMock2 = new ContentPageMock(eventRecorder) { Title = "contentPageMock2" };
	        var contentPageMock3 = new ContentPageMock(eventRecorder) { Title = "contentPageMock3" };
            var tabbedPage = new TabbedPage();
            tabbedPage.Children.Add(contentPageMock1);
	        tabbedPage.Children.Add(contentPageMock2);
	        tabbedPage.Children.Add(contentPageMock3);

	        var navigationMock = new Mock<INavigation>();
	        var navigator = new Navigator(contentPageMock3, navigationMock.Object);
	        await navigator.PopToRootAsync();

            Assert.Equal(0, eventRecorder.Count);
            navigationMock.Verify(m => m.PopToRootAsync(It.IsAny<bool>()), Times.Never);
        }

        [Fact]
		public async Task PushAsync()
		{
			var eventRecorder = new EventRecorder();
			var contentPageMock1 = new ContentPageMock(eventRecorder);
		    // ReSharper disable once UnusedVariable
			var navigationPageMock = new NavigationPageMock(contentPageMock1, eventRecorder);
			var navigationMock = new Mock<INavigation>();
			var navigator = new Navigator(contentPageMock1, navigationMock.Object);
			var behaviorInjectorMock = new Mock<IBehaviorInjector>();
			navigator.BehaviorInjector = behaviorInjectorMock.Object;

			var contentPageMock2 = new ContentPageMock(eventRecorder);
			await navigator.PushAsync(contentPageMock2);
			
			
			behaviorInjectorMock.Verify(x => x.Inject(contentPageMock2), Times.Once);
			navigationMock.Verify(x => x.PushAsync(contentPageMock2, true), Times.Once);

			Assert.Equal(3, eventRecorder.Count);

			Assert.Equal(contentPageMock2, eventRecorder[0].Sender);
			Assert.Equal("OnInitialize", eventRecorder[0].CallerMemberName);
			Assert.Null(eventRecorder[0].Parameter);

			Assert.Equal(contentPageMock2, eventRecorder[1].Sender);
			Assert.Equal("OnLoaded", eventRecorder[1].CallerMemberName);
			Assert.Null(eventRecorder[1].Parameter);

			Assert.Equal(contentPageMock1, eventRecorder[2].Sender);
			Assert.Equal("OnUnloaded", eventRecorder[2].CallerMemberName);
			Assert.Null(eventRecorder[2].Parameter);
		}

	    [Fact]
	    public async Task PushAsync_WhenAnimatedIsFalse()
	    {
	        var eventRecorder = new EventRecorder();
	        var contentPageMock1 = new ContentPageMock(eventRecorder);
	        // ReSharper disable once UnusedVariable
	        var navigationPageMock = new NavigationPageMock(contentPageMock1, eventRecorder);
	        var navigationMock = new Mock<INavigation>();
	        var navigator = new Navigator(contentPageMock1, navigationMock.Object);
	        var behaviorInjectorMock = new Mock<IBehaviorInjector>();
	        navigator.BehaviorInjector = behaviorInjectorMock.Object;

	        var contentPageMock2 = new ContentPageMock(eventRecorder);
	        await navigator.PushAsync(contentPageMock2, false);


	        behaviorInjectorMock.Verify(x => x.Inject(contentPageMock2), Times.Once);
	        navigationMock.Verify(x => x.PushAsync(contentPageMock2, false), Times.Once);
	    }

		[Fact]
		public async Task PushAsync_WithParameter()
		{
			var eventRecorder = new EventRecorder();
			var contentPageMock1 = new ContentPageMock(eventRecorder);
		    // ReSharper disable once UnusedVariable
			var navigationPageMock = new NavigationPageMock(contentPageMock1, eventRecorder);
			var navigationMock = new Mock<INavigation>();
			var navigator = new Navigator(contentPageMock1, navigationMock.Object);
			var behaviorInjectorMock = new Mock<IBehaviorInjector>();
			navigator.BehaviorInjector = behaviorInjectorMock.Object;

			var contentPageMock2 = new ContentPageMock(eventRecorder);
			var parameter = new object();
			await navigator.PushAsync(contentPageMock2, parameter);


			behaviorInjectorMock.Verify(x => x.Inject(contentPageMock2), Times.Once);
			navigationMock.Verify(x => x.PushAsync(contentPageMock2, true), Times.Once);

			Assert.Equal(3, eventRecorder.Count);

			Assert.Equal(contentPageMock2, eventRecorder[0].Sender);
			Assert.Equal("OnInitialize", eventRecorder[0].CallerMemberName);
			Assert.Equal(parameter, eventRecorder[0].Parameter);

			Assert.Equal(contentPageMock2, eventRecorder[1].Sender);
			Assert.Equal("OnLoaded", eventRecorder[1].CallerMemberName);
			Assert.Null(eventRecorder[1].Parameter);

			Assert.Equal(contentPageMock1, eventRecorder[2].Sender);
			Assert.Equal("OnUnloaded", eventRecorder[2].CallerMemberName);
			Assert.Null(eventRecorder[2].Parameter);
		}

		[Fact]
		public async Task PushModalAsync()
		{
			var eventRecorder = new EventRecorder();
			var contentPageMock1 = new ContentPageMock(eventRecorder);
			var navigationMock = new Mock<INavigation>();
			var navigator = new Navigator(contentPageMock1, navigationMock.Object);
			var behaviorInjectorMock = new Mock<IBehaviorInjector>();
			navigator.BehaviorInjector = behaviorInjectorMock.Object;

			var contentPageMock2 = new ContentPageMock(eventRecorder);
			await navigator.PushModalAsync(contentPageMock2);


			behaviorInjectorMock.Verify(x => x.Inject(contentPageMock2), Times.Once);
			navigationMock.Verify(x => x.PushModalAsync(contentPageMock2, true), Times.Once);

			Assert.Equal(3, eventRecorder.Count);

			Assert.Equal(contentPageMock2, eventRecorder[0].Sender);
			Assert.Equal("OnInitialize", eventRecorder[0].CallerMemberName);
			Assert.Null(eventRecorder[0].Parameter);

			Assert.Equal(contentPageMock2, eventRecorder[1].Sender);
			Assert.Equal("OnLoaded", eventRecorder[1].CallerMemberName);
			Assert.Null(eventRecorder[1].Parameter);

			Assert.Equal(contentPageMock1, eventRecorder[2].Sender);
			Assert.Equal("OnUnloaded", eventRecorder[2].CallerMemberName);
			Assert.Null(eventRecorder[2].Parameter);
		}

	    [Fact]
	    public async Task PushModalAsync_WhenAnimatedIsFalse()
	    {
	        var eventRecorder = new EventRecorder();
	        var contentPageMock1 = new ContentPageMock(eventRecorder);
	        var navigationMock = new Mock<INavigation>();
	        var navigator = new Navigator(contentPageMock1, navigationMock.Object);
	        var behaviorInjectorMock = new Mock<IBehaviorInjector>();
	        navigator.BehaviorInjector = behaviorInjectorMock.Object;

	        var contentPageMock2 = new ContentPageMock(eventRecorder);
	        await navigator.PushModalAsync(contentPageMock2, false);


	        behaviorInjectorMock.Verify(x => x.Inject(contentPageMock2), Times.Once);
	        navigationMock.Verify(x => x.PushModalAsync(contentPageMock2, false), Times.Once);
	    }

		[Fact]
		public async Task PushModalAsync_WithParameter()
		{
			var eventRecorder = new EventRecorder();
			var contentPageMock1 = new ContentPageMock(eventRecorder);
			var navigationMock = new Mock<INavigation>();
			var navigator = new Navigator(contentPageMock1, navigationMock.Object);
			var behaviorInjectorMock = new Mock<IBehaviorInjector>();
			navigator.BehaviorInjector = behaviorInjectorMock.Object;

			var contentPageMock2 = new ContentPageMock(eventRecorder);
			var parameter = new object();
			await navigator.PushModalAsync(contentPageMock2, parameter);


			behaviorInjectorMock.Verify(x => x.Inject(contentPageMock2), Times.Once);
			navigationMock.Verify(x => x.PushModalAsync(contentPageMock2, true), Times.Once);

			Assert.Equal(3, eventRecorder.Count);

			Assert.Equal(contentPageMock2, eventRecorder[0].Sender);
			Assert.Equal("OnInitialize", eventRecorder[0].CallerMemberName);
			Assert.Equal(parameter, eventRecorder[0].Parameter);

			Assert.Equal(contentPageMock2, eventRecorder[1].Sender);
			Assert.Equal("OnLoaded", eventRecorder[1].CallerMemberName);
			Assert.Null(eventRecorder[1].Parameter);

			Assert.Equal(contentPageMock1, eventRecorder[2].Sender);
			Assert.Equal("OnUnloaded", eventRecorder[2].CallerMemberName);
			Assert.Null(eventRecorder[2].Parameter);
		}

		[Fact]
		public void RemovePage_LastPage()
		{
			var eventRecorder = new EventRecorder();
			var contentPageMock1 = new ContentPageMock(eventRecorder);
			var navigationPage = new NavigationPage(contentPageMock1);
			navigationPage.Behaviors.Add(new NavigationPageBehavior());
			var contentPageMock2 = new ContentPageMock(eventRecorder);
			contentPageMock1.Navigation.PushAsync(contentPageMock2);
			
			var navigator= new Navigator(contentPageMock2);
			navigator.RemovePage(contentPageMock2);
			
			Assert.Equal(contentPageMock1, navigationPage.CurrentPage);
			
			Assert.Equal(2, eventRecorder.Count);

			Assert.Equal(contentPageMock1, eventRecorder[0].Sender);
			Assert.Equal("OnLoaded", eventRecorder[0].CallerMemberName);
			Assert.Null(eventRecorder[0].Parameter);

			Assert.Equal(contentPageMock2, eventRecorder[1].Sender);
			Assert.Equal("OnClosed", eventRecorder[1].CallerMemberName);
			Assert.Null(eventRecorder[1].Parameter);
		}

		[Fact]
		public void RemovePage_NotLastPage()
		{
			var eventRecorder = new EventRecorder();
			var contentPageMock1 = new ContentPageMock(eventRecorder);
			var navigationPage = new NavigationPage(contentPageMock1);
			navigationPage.Behaviors.Add(new NavigationPageBehavior());
			var contentPageMock2 = new ContentPageMock(eventRecorder);
			contentPageMock1.Navigation.PushAsync(contentPageMock2);

			var navigator = new Navigator(contentPageMock2);
			navigator.RemovePage(contentPageMock1);

			Assert.Equal(contentPageMock2, navigationPage.CurrentPage);

			Assert.Equal(1, eventRecorder.Count);

			Assert.Equal(contentPageMock1, eventRecorder[0].Sender);
			Assert.Equal("OnClosed", eventRecorder[0].CallerMemberName);
			Assert.Null(eventRecorder[0].Parameter);
		}

	}
}
