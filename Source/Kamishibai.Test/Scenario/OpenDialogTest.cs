using NUnit.Framework;
using Driver.Windows;
using FluentAssertions;
using Codeer.Friendly;

namespace Scenario;

[TestFixture]

public class OpenDialogTest : TestBase
{
    [Test]
    public void OpenByType()
    {
        var mainWindow = app.AttachMainWindow();
        var openDialogPage = app.AttachOpenDialogPage();
        // Navigate OpenWindowPage
        mainWindow.SelectedMenuItem.EmulateChangeSelectedIndex(2);

        // Open window
        var async = new Async();
        openDialogPage.OpenByTypeCommand.EmulateClick(async);
        var childWindow = app.AttachChildWindow();
        childWindow.Core.Activate();

        // check blocking
        childWindow.BlockClosing.EmulateCheck(true);
        childWindow.CloseCommand.EmulateClick();
        childWindow.IsLoaded.Should().BeTrue();
        childWindow.Message.Text.Should().Be("Closing blocked.");

        // close window
        childWindow.BlockClosing.EmulateCheck(false);
        childWindow.CloseCommand.EmulateClick();
        async.WaitForCompletion();
        childWindow.IsLoaded.Should().BeFalse();
    }

    [Test]
    public void OpenByGenericType()
    {
        var mainWindow = app.AttachMainWindow();
        var openDialogPage = app.AttachOpenDialogPage();
        // Navigate openDialogPage
        mainWindow.SelectedMenuItem.EmulateChangeSelectedIndex(2);

        // Open window
        var async = new Async();
        openDialogPage.SelectedWindowStartupLocation.EmulateChangeSelectedIndex(1);
        openDialogPage.OpenByGenericTypeCommand.EmulateClick(async);
        var childWindow = app.AttachChildWindow();
        childWindow.IsLoaded.Should().BeTrue();

        // close window
        childWindow.Core.Activate();
        childWindow.CloseSpecifiedWindowCommand.EmulateClick();
        async.WaitForCompletion();
        childWindow.IsLoaded.Should().BeFalse();
    }

    [Test]
    public void OpenByViewModelInstance()
    {
        var mainWindow = app.AttachMainWindow();
        var openDialogPage = app.AttachOpenDialogPage();
        // Navigate openDialogPage
        mainWindow.SelectedMenuItem.EmulateChangeSelectedIndex(2);

        // Open window
        var async = new Async();
        openDialogPage.SelectedWindowStartupLocation.EmulateChangeSelectedIndex(2);
        const string windowName = "Hello, Navigate!";
        openDialogPage.WindowName1.EmulateChangeText(windowName);

        openDialogPage.OpenByInstanceCommand.EmulateClick(async);
        var childWindow = app.AttachChildWindow();
        childWindow.IsLoaded.Should().BeTrue();
        childWindow.WindowName.Text.Should().Be(windowName);


        // close window
        childWindow.Core.Activate();
        childWindow.CloseCommand.EmulateClick();
        async.WaitForCompletion();
        childWindow.IsLoaded.Should().BeFalse();
    }

    [Test]
    public void OpenWithCallbackInitializer()
    {
        var mainWindow = app.AttachMainWindow();
        var openDialogPage = app.AttachOpenDialogPage();
        // Navigate openDialogPage
        mainWindow.SelectedMenuItem.EmulateChangeSelectedIndex(2);

        // Open window
        var async = new Async();
        const string windowName = "Hello, Navigate!";
        openDialogPage.WindowName2.EmulateChangeText(windowName);

        openDialogPage.OpenWithCallbackCommand.EmulateClick(async);
        var childWindow = app.AttachChildWindow();
        childWindow.IsLoaded.Should().BeTrue();
        childWindow.WindowName.Text.Should().Be(windowName);


        // close window
        childWindow.Core.Activate();
        childWindow.CloseCommand.EmulateClick();
        async.WaitForCompletion();
        childWindow.IsLoaded.Should().BeFalse();
    }

    [Test]
    public void OpenWithSafeParameter()
    {
        var mainWindow = app.AttachMainWindow();
        var openDialogPage = app.AttachOpenDialogPage();
        // Navigate openDialogPage
        mainWindow.SelectedMenuItem.EmulateChangeSelectedIndex(2);

        // Open window
        var async = new Async();
        const string windowName = "Hello, Navigate!";
        openDialogPage.WindowName3.EmulateChangeText(windowName);

        openDialogPage.OpenWithSafeParameterCommand.EmulateClick(async);
        var childWindow = app.AttachChildWindow();
        childWindow.IsLoaded.Should().BeTrue();
        childWindow.WindowName.Text.Should().Be(windowName);


        // close window
        childWindow.Core.Activate();
        childWindow.CloseCommand.EmulateClick();
        async.WaitForCompletion();
        childWindow.IsLoaded.Should().BeFalse();
    }
}