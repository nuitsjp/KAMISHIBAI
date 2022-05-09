using Kamishibai.Xamarin.Forms.Tests.Mocks;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests.Lifecycle
{
    public class OnUnloadedFixture
    {
        public OnUnloadedFixture()
        {
            // Initializes Xamarin.Forms services.
            var platformServicesFake = FakeItEasy.A.Fake<global::Xamarin.Forms.Internals.IPlatformServices>();
            global::Xamarin.Forms.Device.PlatformServices = platformServicesFake;
        }

        [Fact]
        public void ContentPage()
        {
            var eventRecoder = new EventRecorder();
            var viewModelMock = new ViewModelMock(eventRecoder);
            var contentPageMock = new ContentPageMock(eventRecoder)
            {
                BindingContext = viewModelMock
            };

            LifecycleNoticeService.OnUnloaded(contentPageMock);

            Assert.Equal(2, eventRecoder.Count);

            Assert.NotNull(eventRecoder[0]);
            Assert.Equal(viewModelMock, eventRecoder[0].Sender);
            Assert.Equal("OnUnloaded", eventRecoder[0].CallerMemberName);
            Assert.Null(eventRecoder[0].Parameter);

            Assert.NotNull(eventRecoder[1]);
            Assert.Equal(contentPageMock, eventRecoder[1].Sender);
            Assert.Equal("OnUnloaded", eventRecoder[1].CallerMemberName);
            Assert.Null(eventRecoder[1].Parameter);
        }

        [Fact]
        public void MasterDetailPage()
        {
            var eventRecoder = new EventRecorder();
            var contentPageMock1 = new ContentPageMock(eventRecoder) { Title = "contentPageMock1" };
            var contentPageMock2 = new ContentPageMock(eventRecoder) { Title = "contentPageMock2" };
            var viewModelMock = new ViewModelMock(eventRecoder);
            var masterDetailPageMock = new MasterDetailPageMock(eventRecoder)
            {
                Master = contentPageMock1,
                Detail = contentPageMock2,
                BindingContext = viewModelMock
            };

            LifecycleNoticeService.OnUnloaded(masterDetailPageMock);

            Assert.Equal(4, eventRecoder.Count);

            Assert.NotNull(eventRecoder[0]);
            Assert.Equal(contentPageMock2, eventRecoder[0].Sender);
            Assert.Equal("OnUnloaded", eventRecoder[0].CallerMemberName);
            Assert.Null(eventRecoder[0].Parameter);

            Assert.NotNull(eventRecoder[1]);
            Assert.Equal(contentPageMock1, eventRecoder[1].Sender);
            Assert.Equal("OnUnloaded", eventRecoder[1].CallerMemberName);
            Assert.Null(eventRecoder[1].Parameter);

            Assert.NotNull(eventRecoder[2]);
            Assert.Equal(viewModelMock, eventRecoder[2].Sender);
            Assert.Equal("OnUnloaded", eventRecoder[2].CallerMemberName);
            Assert.Null(eventRecoder[2].Parameter);

            Assert.NotNull(eventRecoder[3]);
            Assert.Equal(masterDetailPageMock, eventRecoder[3].Sender);
            Assert.Equal("OnUnloaded", eventRecoder[3].CallerMemberName);
            Assert.Null(eventRecoder[3].Parameter);
        }

        [Fact]
        public void NavigationPage()
        {
            var eventRecoder = new EventRecorder();
            var contentPageMock1 = new ContentPageMock(eventRecoder);
            var contentPageMock2 = new ContentPageMock(eventRecoder);
            var viewModelMock = new ViewModelMock(eventRecoder);
            var navigationPageMock = new NavigationPageMock(contentPageMock1, eventRecoder)
            {
                BindingContext = viewModelMock
            };
            contentPageMock1.Navigation.PushAsync(contentPageMock2);

            LifecycleNoticeService.OnUnloaded(navigationPageMock);

            Assert.Equal(3, eventRecoder.Count);

            Assert.NotNull(eventRecoder[0]);
            Assert.Equal(contentPageMock2, eventRecoder[0].Sender);
            Assert.Equal("OnUnloaded", eventRecoder[0].CallerMemberName);
            Assert.Null(eventRecoder[0].Parameter);

            Assert.NotNull(eventRecoder[1]);
            Assert.Equal(viewModelMock, eventRecoder[1].Sender);
            Assert.Equal("OnUnloaded", eventRecoder[1].CallerMemberName);
            Assert.Null(eventRecoder[1].Parameter);

            Assert.NotNull(eventRecoder[2]);
            Assert.Equal(navigationPageMock, eventRecoder[2].Sender);
            Assert.Equal("OnUnloaded", eventRecoder[2].CallerMemberName);
            Assert.Null(eventRecoder[2].Parameter);
        }

        [Fact]
        public void TabbedPage()
        {
            var eventRecoder = new EventRecorder();
            var contentPageMock1 = new ContentPageMock(eventRecoder) { Title = "contentPageMock1" };
            var contentPageMock2 = new ContentPageMock(eventRecoder) { Title = "contentPageMock2" };
            var viewModelMock = new ViewModelMock(eventRecoder);
            var tabbedPageMock = new TabbedPageMock(eventRecoder)
            {
                BindingContext = viewModelMock
            };
            tabbedPageMock.Children.Add(contentPageMock1);
            tabbedPageMock.Children.Add(contentPageMock2);
            tabbedPageMock.CurrentPage = contentPageMock2;

            LifecycleNoticeService.OnUnloaded(tabbedPageMock);

            Assert.Equal(3, eventRecoder.Count);

            Assert.NotNull(eventRecoder[0]);
            Assert.Equal(contentPageMock2, eventRecoder[0].Sender);
            Assert.Equal("OnUnloaded", eventRecoder[0].CallerMemberName);
            Assert.Null(eventRecoder[0].Parameter);

            Assert.NotNull(eventRecoder[1]);
            Assert.Equal(viewModelMock, eventRecoder[1].Sender);
            Assert.Equal("OnUnloaded", eventRecoder[1].CallerMemberName);
            Assert.Null(eventRecoder[1].Parameter);

            Assert.NotNull(eventRecoder[2]);
            Assert.Equal(tabbedPageMock, eventRecoder[2].Sender);
            Assert.Equal("OnUnloaded", eventRecoder[2].CallerMemberName);
            Assert.Null(eventRecoder[2].Parameter);
        }

        [Fact]
        public void CarouselPage()
        {
            var eventRecoder = new EventRecorder();
            var contentPageMock1 = new ContentPageMock(eventRecoder) { Title = "contentPageMock1" };
            var contentPageMock2 = new ContentPageMock(eventRecoder) { Title = "contentPageMock2" };
            var viewModelMock = new ViewModelMock(eventRecoder);
            var carouselPageMock = new CarouselPageMock(eventRecoder)
            {
                BindingContext = viewModelMock
            };
            carouselPageMock.Children.Add(contentPageMock1);
            carouselPageMock.Children.Add(contentPageMock2);
            carouselPageMock.CurrentPage = contentPageMock2;

            LifecycleNoticeService.OnUnloaded(carouselPageMock);

            Assert.Equal(3, eventRecoder.Count);

            Assert.NotNull(eventRecoder[0]);
            Assert.Equal(contentPageMock2, eventRecoder[0].Sender);
            Assert.Equal("OnUnloaded", eventRecoder[0].CallerMemberName);
            Assert.Null(eventRecoder[0].Parameter);

            Assert.NotNull(eventRecoder[1]);
            Assert.Equal(viewModelMock, eventRecoder[1].Sender);
            Assert.Equal("OnUnloaded", eventRecoder[1].CallerMemberName);
            Assert.Null(eventRecoder[1].Parameter);

            Assert.NotNull(eventRecoder[2]);
            Assert.Equal(carouselPageMock, eventRecoder[2].Sender);
            Assert.Equal("OnUnloaded", eventRecoder[2].CallerMemberName);
            Assert.Null(eventRecoder[2].Parameter);
        }
    }
}
