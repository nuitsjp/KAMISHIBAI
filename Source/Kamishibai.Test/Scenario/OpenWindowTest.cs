using Codeer.Friendly.Windows;
using Driver.TestController;
using NUnit.Framework;
using Driver.Windows;
using FluentAssertions;

namespace Scenario;

[TestFixture]

public class OpenWindowTest : TestBase
{
    [Test]
    public void OpenByType()
    {
        var mainWindow = app.AttachMainWindow();
        var openWindowPage = app.AttachOpenWindowPage();

        // Navigate OpenWindowPage
        mainWindow.SelectedMenuItem.EmulateChangeSelectedIndex(1);

        // Open window
        openWindowPage.OpenByTypeCommand.EmulateClick();
        var childWindow = app.AttachChildWindow();
        childWindow.Core.Activate();
        childWindow.IsLoaded.Should().BeTrue();

        // check blocking
        childWindow.BlockClosing.EmulateCheck(true);
        childWindow.CloseCommand.EmulateClick();
        childWindow.IsLoaded.Should().BeTrue();

        // close window
        childWindow.BlockClosing.EmulateCheck(false);
        childWindow.CloseCommand.EmulateClick();
        childWindow.IsLoaded.Should().BeFalse();
    }

    [Test]
    public void OpenByGenericType()
    {
        var mainWindow = app.AttachMainWindow();
        var openWindowPage = app.AttachOpenWindowPage();
        // Navigate OpenWindowPage
        mainWindow.SelectedMenuItem.EmulateChangeSelectedIndex(1);

        // Open window
        openWindowPage.SelectedWindowStartupLocation.EmulateChangeSelectedIndex(1);
        openWindowPage.OpenByGenericTypeCommand.EmulateClick();
        var childWindow = app.AttachChildWindow();
        childWindow.IsLoaded.Should().BeTrue();

        // close window
        childWindow.Core.Activate();
        childWindow.CloseSpecifiedWindowCommand.EmulateClick();
        childWindow.IsLoaded.Should().BeFalse();
    }

    [Test]
    public void OpenByViewModelInstance()
    {
        var mainWindow = app.AttachMainWindow();
        var openWindowPage = app.AttachOpenWindowPage();
        // Navigate OpenWindowPage
        mainWindow.SelectedMenuItem.EmulateChangeSelectedIndex(1);

        // Open window
        openWindowPage.SelectedWindowStartupLocation.EmulateChangeSelectedIndex(2);
        openWindowPage.OpenByInstanceCommand.EmulateClick();
        var childWindow = app.AttachChildWindow();
        const string windowName = "Hello, Navigate!";
        openWindowPage.WindowName1.EmulateChangeText(windowName);
        childWindow.IsLoaded.Should().BeTrue();


        // close window
        childWindow.Core.Activate();
        childWindow.CloseCommand.EmulateClick();
        childWindow.IsLoaded.Should().BeFalse();
    }
}