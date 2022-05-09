using Kamishibai.Maui.Tests.Mocks;
using Xunit;

namespace Kamishibai.Maui.Tests
{
    public class NavigationPageBehaviorFixture
    {
        [Fact]
		public void OnCurrentPageChanged()
		{
            var eventRecoder = new EventRecorder();
			var contentPageMock1 = new ContentPageMock(eventRecoder) { Title = "contentPageMock1" };
			var contentPageMock2 = new ContentPageMock(eventRecoder) { Title = "contentPageMock2" };
			var navigationPageMock = new NavigationPageMock(contentPageMock1, eventRecoder);
		    contentPageMock1.Navigation.PushAsync(contentPageMock2);

		    navigationPageMock.Behaviors.Add(new NavigationPageBehavior());

		    contentPageMock2.Navigation.PopAsync();

			Assert.Equal(2, eventRecoder.Count);

			Assert.NotNull(eventRecoder[0]);
			Assert.Equal(contentPageMock1, eventRecoder[0].Sender);
			Assert.Equal("OnLoaded", eventRecoder[0].CallerMemberName);
			Assert.Null(eventRecoder[0].Parameter);

			Assert.NotNull(eventRecoder[1]);
			Assert.Equal(contentPageMock2, eventRecoder[1].Sender);
			Assert.Equal("OnClosed", eventRecoder[1].CallerMemberName);
			Assert.Null(eventRecoder[1].Parameter);

		}


		[Fact]
		public void OnDetachingFrom()
		{
		    var eventRecoder = new EventRecorder();
		    var contentPageMock1 = new ContentPageMock(eventRecoder) { Title = "contentPageMock1" };
		    var contentPageMock2 = new ContentPageMock(eventRecoder) { Title = "contentPageMock2" };
		    var navigationPageMock = new NavigationPageMock(contentPageMock1, eventRecoder);
		    contentPageMock1.Navigation.PushAsync(contentPageMock2);

		    navigationPageMock.Behaviors.Add(new NavigationPageBehavior());
		    navigationPageMock.Behaviors.Clear();

		    contentPageMock2.Navigation.PopAsync();

            Assert.Equal(0, eventRecoder.Count);
		}

        [Fact]
        public void CanNotPop()
        {
            var eventRecoder = new EventRecorder();
            var contentPageMock1 = new ContentPageMock(eventRecoder) { Title = "contentPageMock1" };
            var navigationPageMock = new NavigationPageMock(contentPageMock1, eventRecoder);

            navigationPageMock.Behaviors.Add(new NavigationPageBehavior());

            contentPageMock1.Navigation.PopAsync();

            Assert.Equal(0, eventRecoder.Count);
        }

    }
}
