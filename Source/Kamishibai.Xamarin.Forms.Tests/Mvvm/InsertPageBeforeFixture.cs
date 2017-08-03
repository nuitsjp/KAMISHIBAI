using Kamishibai.Xamarin.Forms.Mvvm;
using Moq;
using Xamarin.Forms;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests.Mvvm
{
    public class InsertPageBeforeFixture
    {
        [Fact]
        public void Navigate()
        {
            var contentPageA = new ContentPageA();
            var navigationPage = new NavigationPage(contentPageA);
            var contentPageB = new ContentPageB();
            contentPageA.Navigation.PushAsync(contentPageB);

            var insertPageBefore = new InsertPageBefore<ContentPageC, ContentPageB>();
            var navigatorMock = new Mock<INavigator>();
            insertPageBefore.Navigator = navigatorMock.Object;
            contentPageB.Behaviors.Add(insertPageBefore);

            var parameter = "Hello, Parameter!";
            insertPageBefore.Navigate(parameter);

			navigatorMock.Verify(m => m.InsertPageBefore(It.IsAny<ContentPageC>(), contentPageB, parameter), Times.Once);
        }

        private class ContentPageA : ContentPage { }
        private class ContentPageB : ContentPage { }
        private class ContentPageC : ContentPage { }
    }
}
