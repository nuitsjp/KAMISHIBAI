using NUnit.Framework;
using Codeer.Friendly;
using Driver.Windows;
using Driver.Windows.Native;
using FluentAssertions;

namespace Scenario;

[TestFixture]
public class ShowMessageTest : TestBase
{
    [Test]
    public void Show()
    {
        var mainWindow = app.AttachMainWindow();
        var showMessagePage = app.AttachShowMessagePage();
        // Navigate show message
        mainWindow.SelectedMenuItem.EmulateChangeSelectedIndex(3);

        // Setup dialog 
        showMessagePage.Message.EmulateChangeText("Hello, Message!");
        showMessagePage.Caption.EmulateChangeText("Hello, Caption!");
        showMessagePage.SelectedMessageBoxButton.EmulateChangeSelectedIndex(1);
        showMessagePage.SelectedMessageBoxImage.EmulateChangeSelectedIndex(2);
        showMessagePage.SelectedMessageBoxResult.EmulateChangeSelectedIndex(3);

        // Show message dialog
        var async = new Async();
        showMessagePage.ShowMessageCommand.EmulateClick(async);
        var messageBox = app.Attach_MessageBox(@"Hello, Caption!");
        messageBox.Button_OK.Enabled.Should().BeTrue();
        messageBox.Button_Cancel.Enabled.Should().BeTrue();

        // Cancel dialog
        messageBox.Button_Cancel.EmulateClick();
        async.WaitForCompletion();

    }
}