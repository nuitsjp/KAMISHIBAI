using Kamishibai.Xamarin.Forms.Tests.Mocks;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests.Lifecycle
{
    public class OnInitializeFixture
    {
        [Fact]
        public void ContentPage()
        {
            var eventRecoder = new EventRecorder();
            var viewModelMock = new ViewModelMock(eventRecoder);
            var contentPageMock = new ContentPageMock(eventRecoder)
            {
                BindingContext = viewModelMock
            };
            var parameter = "Hello, Parameter!";

            LifecycleNoticeService.OnInitialize(contentPageMock, parameter);

            Assert.Equal(2, eventRecoder.Count);

            Assert.NotNull(eventRecoder[0]);
            Assert.Equal(contentPageMock, eventRecoder[0].Sender);
            Assert.Equal("OnInitialize", eventRecoder[0].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[0].Parameter);

            Assert.NotNull(eventRecoder[1]);
            Assert.Equal(viewModelMock, eventRecoder[1].Sender);
            Assert.Equal("OnInitialize", eventRecoder[1].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[1].Parameter);
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
            var parameter = "Hello, Parameter!";

            LifecycleNoticeService.OnInitialize(masterDetailPageMock, parameter);

            Assert.Equal(4, eventRecoder.Count);

            Assert.NotNull(eventRecoder[0]);
            Assert.Equal(masterDetailPageMock, eventRecoder[0].Sender);
            Assert.Equal("OnInitialize", eventRecoder[0].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[0].Parameter);

            Assert.NotNull(eventRecoder[1]);
            Assert.Equal(viewModelMock, eventRecoder[1].Sender);
            Assert.Equal("OnInitialize", eventRecoder[1].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[1].Parameter);

            Assert.NotNull(eventRecoder[2]);
            Assert.Equal(contentPageMock1, eventRecoder[2].Sender);
            Assert.Equal("OnInitialize", eventRecoder[2].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[2].Parameter);

            Assert.NotNull(eventRecoder[3]);
            Assert.Equal(contentPageMock2, eventRecoder[3].Sender);
            Assert.Equal("OnInitialize", eventRecoder[3].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[3].Parameter);
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
            var parameter = "Hello, Parameter!";

            LifecycleNoticeService.OnInitialize(navigationPageMock, parameter);

            Assert.Equal(4, eventRecoder.Count);

            Assert.NotNull(eventRecoder[0]);
            Assert.Equal(navigationPageMock, eventRecoder[0].Sender);
            Assert.Equal("OnInitialize", eventRecoder[0].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[0].Parameter);

            Assert.NotNull(eventRecoder[1]);
            Assert.Equal(viewModelMock, eventRecoder[1].Sender);
            Assert.Equal("OnInitialize", eventRecoder[1].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[1].Parameter);

            Assert.NotNull(eventRecoder[2]);
            Assert.Equal(contentPageMock1, eventRecoder[2].Sender);
            Assert.Equal("OnInitialize", eventRecoder[2].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[2].Parameter);

            Assert.NotNull(eventRecoder[3]);
            Assert.Equal(contentPageMock2, eventRecoder[3].Sender);
            Assert.Equal("OnInitialize", eventRecoder[3].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[3].Parameter);
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
            var parameter = "Hello, Parameter!";

            LifecycleNoticeService.OnInitialize(tabbedPageMock, parameter);

            Assert.Equal(4, eventRecoder.Count);

            Assert.NotNull(eventRecoder[0]);
            Assert.Equal(tabbedPageMock, eventRecoder[0].Sender);
            Assert.Equal("OnInitialize", eventRecoder[0].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[0].Parameter);

            Assert.NotNull(eventRecoder[1]);
            Assert.Equal(viewModelMock, eventRecoder[1].Sender);
            Assert.Equal("OnInitialize", eventRecoder[1].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[1].Parameter);

            Assert.NotNull(eventRecoder[2]);
            Assert.Equal(contentPageMock1, eventRecoder[2].Sender);
            Assert.Equal("OnInitialize", eventRecoder[2].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[2].Parameter);

            Assert.NotNull(eventRecoder[3]);
            Assert.Equal(contentPageMock2, eventRecoder[3].Sender);
            Assert.Equal("OnInitialize", eventRecoder[3].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[3].Parameter);
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
            var parameter = "Hello, Parameter!";

            LifecycleNoticeService.OnInitialize(carouselPageMock, parameter);

            Assert.Equal(4, eventRecoder.Count);

            Assert.NotNull(eventRecoder[0]);
            Assert.Equal(carouselPageMock, eventRecoder[0].Sender);
            Assert.Equal("OnInitialize", eventRecoder[0].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[0].Parameter);

            Assert.NotNull(eventRecoder[1]);
            Assert.Equal(viewModelMock, eventRecoder[1].Sender);
            Assert.Equal("OnInitialize", eventRecoder[1].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[1].Parameter);

            Assert.NotNull(eventRecoder[2]);
            Assert.Equal(contentPageMock1, eventRecoder[2].Sender);
            Assert.Equal("OnInitialize", eventRecoder[2].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[2].Parameter);

            Assert.NotNull(eventRecoder[3]);
            Assert.Equal(contentPageMock2, eventRecoder[3].Sender);
            Assert.Equal("OnInitialize", eventRecoder[3].CallerMemberName);
            Assert.Equal(parameter, eventRecoder[3].Parameter);
        }
    }
}
