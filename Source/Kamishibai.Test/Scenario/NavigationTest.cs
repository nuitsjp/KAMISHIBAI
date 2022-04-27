using Codeer.Friendly.Windows;
using Driver.TestController;
using NUnit.Framework;
using Driver.Windows;
using FluentAssertions;

namespace Scenario;

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
        var mainWindow = _app.AttachMainWindow();
        var contentPage = _app.AttachContentPage();
        var navigationMenuPage = _app.AttachNavigationMenuPage();

        // Navigate to ContentPage
        const string message = "Hello, Navigate!";
        navigationMenuPage.Message2.EmulateChangeText(message);
        navigationMenuPage.NavigateWithCallbackCommand.EmulateClick();

        mainWindow.NavigationFrame.Should().BeOfPage("SampleBrowser.View.Page.ContentPage");
        contentPage.Message.Text.Should().Be(message);

        // Go back
        contentPage.GoBackCommand.EmulateClick();
        mainWindow.NavigationFrame.Should().BeOfPage("SampleBrowser.View.Page.NavigationMenuPage");
    }
}