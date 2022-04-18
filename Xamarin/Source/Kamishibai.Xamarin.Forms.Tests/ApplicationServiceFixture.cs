using Kamishibai.Xamarin.Forms.Tests.Mocks;
using Moq;
using Xamarin.Forms;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests
{
    public class ApplicationServiceFixture
    {
        internal static Mock<ApplicationService.IApplication> ApplicationMock = new Mock<ApplicationService.IApplication>();
        static ApplicationServiceFixture()
        {
            ApplicationService.Initialize(new ApplicationMock());
            ApplicationService.Current = new Mock<ApplicationService.IApplication>().Object;
            ApplicationService.Current = ApplicationMock.Object;
        }

        [Fact]
        public void SetMainPage()
        {
            var behaviorInjector = new Mock<IBehaviorInjector>();
            var eventRecorder = new EventRecorder();
            var contentPageMock = new ContentPageMock(eventRecorder);

            behaviorInjector.Setup(m => m.Inject(contentPageMock))
                .Callback(() => eventRecorder.Record(behaviorInjector));

            ApplicationService.BehaviorInjector = behaviorInjector.Object;

            ApplicationService.SetMainPage(contentPageMock);

            ApplicationMock.VerifySet(m => m.MainPage = contentPageMock);

            Assert.Equal(3, eventRecorder.Count);

            // BehaviorInjector
            Assert.NotNull(eventRecorder[0]);
            Assert.Equal(behaviorInjector, eventRecorder[0].Sender);

            // OnInitialize
            Assert.NotNull(eventRecorder[1]);
            Assert.Equal(contentPageMock, eventRecorder[1].Sender);
            Assert.Equal("OnInitialize", eventRecorder[1].CallerMemberName);

            // OnLoaded
            Assert.NotNull(eventRecorder[2]);
            Assert.Equal(contentPageMock, eventRecorder[2].Sender);
            Assert.Equal("OnLoaded", eventRecorder[2].CallerMemberName);
        }

        [Fact]
        public void SetMainPage_WithParameter()
        {
            var behaviorInjector = new Mock<IBehaviorInjector>();
            var eventRecorder = new EventRecorder();
            var contentPageMock = new ContentPageMock(eventRecorder);

            behaviorInjector.Setup(m => m.Inject(contentPageMock))
                .Callback(() => eventRecorder.Record(behaviorInjector));

            ApplicationService.BehaviorInjector = behaviorInjector.Object;

            var parameter = new object();

            ApplicationService.SetMainPage(contentPageMock, parameter);

            ApplicationMock.VerifySet(m => m.MainPage = contentPageMock);

            Assert.Equal(3, eventRecorder.Count);

            // BehaviorInjector
            Assert.NotNull(eventRecorder[0]);
            Assert.Equal(behaviorInjector, eventRecorder[0].Sender);

            // OnInitialize
            Assert.NotNull(eventRecorder[1]);
            Assert.Equal(contentPageMock, eventRecorder[1].Sender);
            Assert.Equal("OnInitialize", eventRecorder[1].CallerMemberName);
            Assert.Equal(parameter, eventRecorder[1].Parameter);

            // OnLoaded
            Assert.NotNull(eventRecorder[2]);
            Assert.Equal(contentPageMock, eventRecorder[2].Sender);
            Assert.Equal("OnLoaded", eventRecorder[2].CallerMemberName);
        }

        [Fact]
        public void PopModal()
        {
            var eventRecorder = new EventRecorder();
            var contentPageMock1 = new ContentPageMock(eventRecorder) { Title = "contentPageMock1" };
            var contentPageMock2 = new ContentPageMock(eventRecorder) { Title = "contentPageMock2" };
            var contentPageMock3 = new ContentPageMock(eventRecorder) { Title = "contentPageMock3" };

            //Mocks.ApplicationMock.MainPage = contentPageMock1;
            contentPageMock1.Navigation.PushModalAsync(contentPageMock2);
            contentPageMock2.Navigation.PushModalAsync(contentPageMock3);

            ApplicationMock.SetupGet(m => m.MainPage).Returns(contentPageMock1);

            ApplicationMock.Raise(m => m.ModalPopped += null, new ModalPoppedEventArgs(contentPageMock3));

            Assert.Equal(2, eventRecorder.Count);

            // OnLoaded
            Assert.NotNull(eventRecorder[0]);
            Assert.Equal(contentPageMock2, eventRecorder[0].Sender);
            Assert.Equal("OnLoaded", eventRecorder[0].CallerMemberName);

            // OnClosed
            Assert.NotNull(eventRecorder[1]);
            Assert.Equal(contentPageMock3, eventRecorder[1].Sender);
            Assert.Equal("OnClosed", eventRecorder[1].CallerMemberName);
        }

        [Fact]
        public void ApplicationAdapter_MainPage()
        {
            var mock = new ApplicationMock();
            var adapter = (ApplicationService.IApplication)new ApplicationService.ApplicationAdapter(mock);
            var page = new ContentPage();
            adapter.MainPage = page;
            
            Assert.Equal(page, mock.MainPage);
            Assert.Equal(page, adapter.MainPage);
        }

        [Fact]
        public void ApplicationAdapter_ModalPopped()
        {
            var mock = new ApplicationMock();
            var adapter = (ApplicationService.IApplication)new ApplicationService.ApplicationAdapter(mock);
            var page1 = new ContentPage();
            adapter.MainPage = page1;
            
            var page2 = new ContentPage();
            page1.Navigation.PushModalAsync(page2);

            bool popped = false;
            adapter.ModalPopped += (sender, args) =>
            {
                popped = true;
                Assert.NotNull(sender);
                Assert.NotNull(args);
                Assert.Equal(page2, args.Modal);
            };

            page2.Navigation.PopModalAsync();
            
            Assert.True(popped);
        }

        [Fact]
        public void ApplicationAdapter_ModalPopped_WhenNotListened()
        {
            var mock = new ApplicationMock();
            var adapter = (ApplicationService.IApplication)new ApplicationService.ApplicationAdapter(mock);
            var page1 = new ContentPage();
            adapter.MainPage = page1;

            var page2 = new ContentPage();
            page1.Navigation.PushModalAsync(page2);

            page2.Navigation.PopModalAsync();

            Assert.True(true);
        }

        [Fact]
        public void ApplicationAdapter_WhenApplicationIsNull()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new ApplicationService.ApplicationAdapter(null);
        }
    }
}
