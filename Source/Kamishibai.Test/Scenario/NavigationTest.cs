using Codeer.Friendly.Windows;
using Driver.TestController;
using NUnit.Framework;
using System.Diagnostics;
using Driver.Windows;

namespace Scenario
{
    [TestFixture]
    public class NavigationTest
    {
        WindowsAppFriend _app;

        [SetUp]
        public void TestInitialize() => _app = ProcessController.Start();

        [TearDown]
        public void TestCleanup() => _app.Kill();

        [Test]
        public void NavigateWithCallbackInitializer()
        {
            var contentPage = _app.AttachContentPage();
            var navigationMenuPage = _app.AttachNavigationMenuPage();
            navigationMenuPage.NavigateWithCallbackCommand.EmulateClick();
            
            Assert.AreEqual("Hello, Callback!", contentPage.Message.Text);
            contentPage.GoBackCommand.EmulateClick();
        }
    }
}
