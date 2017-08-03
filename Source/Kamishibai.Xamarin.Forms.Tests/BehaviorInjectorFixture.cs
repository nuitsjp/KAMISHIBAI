using System.Linq;
using Kamishibai.Xamarin.Forms.Tests.Mocks;
using Xamarin.Forms;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests
{
    public class BehaviorInjectorFixture
    {
        [Fact]
        public async void RecursiveInject()
        {
            var eventRecoder = new EventRecorder();
            var carouselPageMock1 = new CarouselPageMock(eventRecoder);
            var tabbedPageMock1 = new TabbedPageMock(eventRecoder);
            tabbedPageMock1.Children.Add(new ContentPage());
            tabbedPageMock1.Children.Add(carouselPageMock1);
            var navigationPage = new NavigationPageMock(tabbedPageMock1, eventRecoder);

            var carouselPageMock2 = new CarouselPageMock(eventRecoder);
            var tabbedPageMock2 = new TabbedPageMock(eventRecoder);
            tabbedPageMock2.Children.Add(new ContentPage());
            tabbedPageMock2.Children.Add(carouselPageMock2);

            await tabbedPageMock1.Navigation.PushAsync(tabbedPageMock2);

            var actual = new BehaviorInjector();
            actual.Inject(navigationPage);

            Assert.Equal(1, navigationPage.Behaviors.Count);
            Assert.IsType<NavigationPageBehavior>(navigationPage.Behaviors.Single());

            Assert.Equal(1, tabbedPageMock1.Behaviors.Count);
            Assert.IsType<MultiPageBehavior<Page>>(tabbedPageMock1.Behaviors.Single());

            Assert.Equal(1, tabbedPageMock2.Behaviors.Count);
            Assert.IsType<MultiPageBehavior<Page>>(tabbedPageMock2.Behaviors.Single());

            Assert.Equal(1, carouselPageMock1.Behaviors.Count);
            Assert.IsType<MultiPageBehavior<ContentPage>>(carouselPageMock1.Behaviors.Single());

            Assert.Equal(1, carouselPageMock2.Behaviors.Count);
            Assert.IsType<MultiPageBehavior<ContentPage>>(carouselPageMock2.Behaviors.Single());
        }

        [Fact]
        public void Inject_WhenOriginallyExisted()
        {
            var navigationPage = new NavigationPage(new ContentPage());
            var behavior = new NavigationPageBehavior();;
            navigationPage.Behaviors.Add(new BehaviorMock());
            navigationPage.Behaviors.Add(behavior);

            var injector = new BehaviorInjector();
            injector.Inject(navigationPage);

            Assert.Equal(2, navigationPage.Behaviors.Count);
            Assert.Equal(behavior, navigationPage.Behaviors[1]);
        }

        private class BehaviorMock : Behavior<Page>
        {
        }
    }
}
