using NUnit.Framework;
using Driver.Windows;
using FluentAssertions;
using Codeer.Friendly;

namespace Scenario;

[TestFixture]

public class OpenDialogTest : TestBase
{
    [Test]
    [TestCase(true, ExpectedResult = "True")]
    [TestCase(false, ExpectedResult = "False")]
    public string OpenByType(bool dialogResult)
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
        childWindow.CloseDialogCommand.EmulateClick();
        childWindow.IsLoaded.Should().BeTrue();
        childWindow.Message.Text.Should().Be("Closing blocked.");

        // Set dialog result
        childWindow.DialogResult.EmulateCheck(dialogResult);

        // close window
        childWindow.BlockClosing.EmulateCheck(false);
        childWindow.CloseDialogCommand.EmulateClick();
        async.WaitForCompletion();
        childWindow.IsLoaded.Should().BeFalse();

        return openDialogPage.DialogResult.Text;
    }

    [Test]
    [TestCase(true, ExpectedResult = "True")]
    [TestCase(false, ExpectedResult = "False")]
    public string OpenByGenericType(bool dialogResult)
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

        // Set dialog result
        childWindow.DialogResult.EmulateCheck(dialogResult);

        // close window
        childWindow.Core.Activate();
        childWindow.CloseDialogCommand.EmulateClick();
        async.WaitForCompletion();
        childWindow.IsLoaded.Should().BeFalse();

        // Since the window is closed internally and asynchronously,
        // the window will not be terminated until it waits.
        Thread.Sleep(TimeSpan.FromMilliseconds(500));
        return openDialogPage.DialogResult.Text;
    }

    [Test]
    [TestCase(true, ExpectedResult = "True")]
    [TestCase(false, ExpectedResult = "False")]
    public string OpenByViewModelInstance(bool dialogResult)
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

        // Set dialog result
        childWindow.DialogResult.EmulateCheck(dialogResult);


        // close window
        childWindow.Core.Activate();
        childWindow.CloseDialogCommand.EmulateClick();
        async.WaitForCompletion();
        childWindow.IsLoaded.Should().BeFalse();

        return openDialogPage.DialogResult.Text;
    }

    [Test]
    [TestCase(true, ExpectedResult = "True")]
    [TestCase(false, ExpectedResult = "False")]
    public string OpenWithCallbackInitializer(bool dialogResult)
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

        // Set dialog result
        childWindow.DialogResult.EmulateCheck(dialogResult);

        // close window
        childWindow.Core.Activate();
        childWindow.CloseDialogCommand.EmulateClick();
        async.WaitForCompletion();
        childWindow.IsLoaded.Should().BeFalse();

        return openDialogPage.DialogResult.Text;
    }

    [Test]
    [TestCase(true, ExpectedResult = "True")]
    [TestCase(false, ExpectedResult = "False")]
    public string OpenWithSafeParameter(bool dialogResult)
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

        // Set dialog result
        childWindow.DialogResult.EmulateCheck(dialogResult);

        // close window
        childWindow.Core.Activate();
        childWindow.CloseDialogCommand.EmulateClick();
        async.WaitForCompletion();
        childWindow.IsLoaded.Should().BeFalse();

        return openDialogPage.DialogResult.Text;
    }
}