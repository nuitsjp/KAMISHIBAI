using Kamishibai.Xamarin.Forms.Tests.Mocks;
using Xamarin.Forms;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests
{
	public class MultiPageBehaviorFixture
	{
		[Fact]
		public void OnCurrentPageChanged()
		{
			var eventRecoder = new EventRecorder();
			var contentPageMock1 = new ContentPageMock(eventRecoder) { Title = "contentPageMock1" };
			var contentPageMock2 = new ContentPageMock(eventRecoder) { Title = "contentPageMock2" };
			var tabbedPageMock = new TabbedPageMock(eventRecoder);
			tabbedPageMock.Children.Add(contentPageMock1);
			tabbedPageMock.Children.Add(contentPageMock2);

			tabbedPageMock.Behaviors.Add(new MultiPageBehavior<Page>());

			tabbedPageMock.CurrentPage = contentPageMock2;
			
			Assert.Equal(2, eventRecoder.Count);

			Assert.NotNull(eventRecoder[0]);
			Assert.Equal(contentPageMock2, eventRecoder[0].Sender);
			Assert.Equal("OnLoaded", eventRecoder[0].CallerMemberName);
			Assert.Null(eventRecoder[0].Parameter);

			Assert.NotNull(eventRecoder[1]);
			Assert.Equal(contentPageMock1, eventRecoder[1].Sender);
			Assert.Equal("OnUnloaded", eventRecoder[1].CallerMemberName);
			Assert.Null(eventRecoder[1].Parameter);

		}

		[Fact]
		public void OnPagesChanged_WhenAddChild()
		{
			var eventRecoder = new EventRecorder();
			var contentPageMock1 = new ContentPageMock(eventRecoder) { Title = "contentPageMock1" };
			var tabbedPageMock = new TabbedPageMock(eventRecoder);
			tabbedPageMock.Children.Add(contentPageMock1);

			tabbedPageMock.Behaviors.Add(new MultiPageBehavior<Page>());

			var contentPageMock2 = new ContentPageMock(eventRecoder) { Title = "contentPageMock2" };
			tabbedPageMock.Children.Add(contentPageMock2);

			Assert.Equal(1, eventRecoder.Count);

			Assert.NotNull(eventRecoder[0]);
			Assert.Equal(contentPageMock2, eventRecoder[0].Sender);
			Assert.Equal("OnInitialize", eventRecoder[0].CallerMemberName);
			Assert.Null(eventRecoder[0].Parameter);
		}

		[Fact]
		public void OnPagesChanged_WhenRemoveChild()
		{
			var eventRecoder = new EventRecorder();
			var contentPageMock1 = new ContentPageMock(eventRecoder) { Title = "contentPageMock1" };
			var contentPageMock2 = new ContentPageMock(eventRecoder) { Title = "contentPageMock2" };
			var tabbedPageMock = new TabbedPageMock(eventRecoder);
			tabbedPageMock.Children.Add(contentPageMock1);
			tabbedPageMock.Children.Add(contentPageMock2);

			tabbedPageMock.Behaviors.Add(new MultiPageBehavior<Page>());

			tabbedPageMock.Children.Remove(contentPageMock1);

			Assert.Equal(3, eventRecoder.Count);

			Assert.NotNull(eventRecoder[0]);
			Assert.Equal(contentPageMock1, eventRecoder[0].Sender);
			Assert.Equal("OnClosed", eventRecoder[0].CallerMemberName);
			Assert.Null(eventRecoder[0].Parameter);

			Assert.NotNull(eventRecoder[1]);
			Assert.Equal(contentPageMock2, eventRecoder[1].Sender);
			Assert.Equal("OnLoaded", eventRecoder[1].CallerMemberName);
			Assert.Null(eventRecoder[1].Parameter);

			Assert.NotNull(eventRecoder[2]);
			Assert.Equal(contentPageMock1, eventRecoder[2].Sender);
			Assert.Equal("OnUnloaded", eventRecoder[2].CallerMemberName);
			Assert.Null(eventRecoder[2].Parameter);
		}

		[Fact]
		public void OnDetachingFrom()
		{
			var eventRecoder = new EventRecorder();
			var contentPageMock1 = new ContentPageMock(eventRecoder) { Title = "contentPageMock1" };
			var contentPageMock2 = new ContentPageMock(eventRecoder) { Title = "contentPageMock2" };
			var tabbedPageMock = new TabbedPageMock(eventRecoder);
			tabbedPageMock.Children.Add(contentPageMock1);
			tabbedPageMock.Children.Add(contentPageMock2);

			tabbedPageMock.Behaviors.Add(new MultiPageBehavior<Page>());
			tabbedPageMock.Behaviors.Clear();

			tabbedPageMock.CurrentPage = contentPageMock2;

			Assert.Equal(0, eventRecoder.Count);
		}

	}
}
