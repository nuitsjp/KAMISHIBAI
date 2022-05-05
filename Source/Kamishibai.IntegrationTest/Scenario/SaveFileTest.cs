using NUnit.Framework;
using Codeer.Friendly;
using Driver.Windows;
using Driver.Windows.Native;
using FluentAssertions;

namespace Scenario;

[TestFixture]
public class SaveFileTest : TestBase
{
    [Test]
    public void SaveFile()
    {
        var mainWindow = app.AttachMainWindow();
        var saveFilePage = app.AttachSaveFilePage();
        mainWindow.SelectedMenuItem.EmulateChangeSelectedIndex(5);

        saveFilePage.InitialDirectory.EmulateChangeText(@"C:\");

        var async = new Async();
        saveFilePage.SaveFileCommand.EmulateClick(async);
        var saveFileDialog = app.Attach_SaveFileDialog(@"名前を付けて保存");
        saveFileDialog.ComboBox_FileName.EmulateChangeEditText("foo");
        saveFileDialog.Button_Save.EmulateClick();
        async.WaitForCompletion();

        saveFilePage.OpenFileResult.Text.Should().Be("Ok");
        saveFilePage.FilePath.Text.Should().Be(@"C:\foo.png");
    }

    [Test]
    public void CancelSaveFile()
    {
        var mainWindow = app.AttachMainWindow();
        var saveFilePage = app.AttachSaveFilePage();
        mainWindow.SelectedMenuItem.EmulateChangeSelectedIndex(5);

        saveFilePage.InitialDirectory.EmulateChangeText(@"C:\");

        var async = new Async();
        saveFilePage.SaveFileCommand.EmulateClick(async);
        var saveFileDialog = app.Attach_SaveFileDialog(@"名前を付けて保存");
        saveFileDialog.ComboBox_FileName.EmulateChangeEditText("foo");
        saveFileDialog.Button_Cancel.EmulateClick();
        async.WaitForCompletion();

        saveFilePage.OpenFileResult.Text.Should().Be("Cancel");
        saveFilePage.FilePath.Text.Should().BeEmpty();
    }

}