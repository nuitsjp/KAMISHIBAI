using Kamishibai.Maui.Tests.Mocks;
using Xunit;

namespace Kamishibai.Maui.Tests
{
    public class LifecycleNoticeServiceFixture : TestFixture
    {
        [Fact]
        public void OnInitialize()
        {
            var eventRecorder = new EventRecorder();
            var contentPageMock = new ContentPageMock(eventRecorder);
            var parameter = new object();
            LifecycleNoticeService.OnInitialize(contentPageMock, parameter);

            Assert.Equal(1, eventRecorder.Count);
            Assert.Equal(contentPageMock, eventRecorder[0].Sender);
            Assert.Equal("OnInitialize", eventRecorder[0].CallerMemberName);
            Assert.Equal(parameter, eventRecorder[0].Parameter);
        }

        [Fact]
        public void OnLoaded()
        {
            var eventRecorder = new EventRecorder();
            var contentPageMock = new ContentPageMock(eventRecorder);
            LifecycleNoticeService.OnLoaded(contentPageMock);

            Assert.Equal(1, eventRecorder.Count);
            Assert.Equal(contentPageMock, eventRecorder[0].Sender);
            Assert.Equal("OnLoaded", eventRecorder[0].CallerMemberName);
            Assert.Null(eventRecorder[0].Parameter);
        }

        [Fact]
        public void OnUnloaded()
        {
            var eventRecorder = new EventRecorder();
            var contentPageMock = new ContentPageMock(eventRecorder);
            LifecycleNoticeService.OnUnloaded(contentPageMock);

            Assert.Equal(1, eventRecorder.Count);
            Assert.Equal(contentPageMock, eventRecorder[0].Sender);
            Assert.Equal("OnUnloaded", eventRecorder[0].CallerMemberName);
            Assert.Null(eventRecorder[0].Parameter);
        }

        [Fact]
        public void OnClosed()
        {
            var eventRecorder = new EventRecorder();
            var contentPageMock = new ContentPageMock(eventRecorder);
            LifecycleNoticeService.OnClosed(contentPageMock);

            Assert.Equal(1, eventRecorder.Count);
            Assert.Equal(contentPageMock, eventRecorder[0].Sender);
            Assert.Equal("OnClosed", eventRecorder[0].CallerMemberName);
            Assert.Null(eventRecorder[0].Parameter);
        }

        [Fact]
        public void OnResume()
        {
            var eventRecorder = new EventRecorder();
            var contentPageMock = new ContentPageMock(eventRecorder);

            var application = new ApplicationMock { MainPage = contentPageMock };

            LifecycleNoticeService.OnResume(application);

            Assert.Equal(1, eventRecorder.Count);
            Assert.Equal(contentPageMock, eventRecorder[0].Sender);
            Assert.Equal("OnResume", eventRecorder[0].CallerMemberName);
            Assert.Null(eventRecorder[0].Parameter);
        }

        [Fact]
        public void OnResume_WithModalStackPages()
        {
            var eventRecorder = new EventRecorder();
            var contentPageMock1 = new ContentPageMock(eventRecorder);

            var application = new ApplicationMock { MainPage = contentPageMock1 };

            var contentPageMock2 = new ContentPageMock(eventRecorder);
            contentPageMock1.Navigation.PushModalAsync(contentPageMock2);
            
            var contentPageMock3 = new ContentPageMock(eventRecorder);
            contentPageMock2.Navigation.PushModalAsync(contentPageMock3);

            LifecycleNoticeService.OnResume(application);

            Assert.Equal(3, eventRecorder.Count);
            
            Assert.Equal(contentPageMock1, eventRecorder[0].Sender);
            Assert.Equal("OnResume", eventRecorder[0].CallerMemberName);
            Assert.Null(eventRecorder[0].Parameter);
            
            Assert.Equal(contentPageMock2, eventRecorder[1].Sender);
            Assert.Equal("OnResume", eventRecorder[1].CallerMemberName);
            Assert.Null(eventRecorder[1].Parameter);

            Assert.Equal(contentPageMock3, eventRecorder[2].Sender);
            Assert.Equal("OnResume", eventRecorder[2].CallerMemberName);
            Assert.Null(eventRecorder[2].Parameter);
        }

        [Fact]
        public void OnSleep()
        {
            lock (ApplicationServiceFixture.ApplicationMock)
            {
                var eventRecorder = new EventRecorder();
                var contentPageMock = new ContentPageMock(eventRecorder);

                var application = new ApplicationMock { MainPage = contentPageMock };

                LifecycleNoticeService.OnSleep(application);

                Assert.Equal(1, eventRecorder.Count);
                Assert.Equal(contentPageMock, eventRecorder[0].Sender);
                Assert.Equal("OnSleep", eventRecorder[0].CallerMemberName);
                Assert.Null(eventRecorder[0].Parameter);
            }
        }

        [Fact]
        public void OnSleep_WithModalStackPages()
        {
            var eventRecorder = new EventRecorder();
            var contentPageMock1 = new ContentPageMock(eventRecorder);

            var application = new ApplicationMock { MainPage = contentPageMock1 };

            var contentPageMock2 = new ContentPageMock(eventRecorder);
            contentPageMock1.Navigation.PushModalAsync(contentPageMock2);

            var contentPageMock3 = new ContentPageMock(eventRecorder);
            contentPageMock2.Navigation.PushModalAsync(contentPageMock3);

            LifecycleNoticeService.OnSleep(application);

            Assert.Equal(3, eventRecorder.Count);

            Assert.Equal(contentPageMock3, eventRecorder[0].Sender);
            Assert.Equal("OnSleep", eventRecorder[0].CallerMemberName);
            Assert.Null(eventRecorder[0].Parameter);

            Assert.Equal(contentPageMock2, eventRecorder[1].Sender);
            Assert.Equal("OnSleep", eventRecorder[1].CallerMemberName);
            Assert.Null(eventRecorder[1].Parameter);

            Assert.Equal(contentPageMock1, eventRecorder[2].Sender);
            Assert.Equal("OnSleep", eventRecorder[2].CallerMemberName);
            Assert.Null(eventRecorder[2].Parameter);
        }
    }
}
