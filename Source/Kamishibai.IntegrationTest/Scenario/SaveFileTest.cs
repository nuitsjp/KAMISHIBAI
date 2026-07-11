using NUnit.Framework;
using Codeer.Friendly;
using System.IO;
using Driver.Windows;
using Driver.Windows.Native;
using FluentAssertions;

namespace Scenario;

[TestFixture]
public class SaveFileTest : TestBase
{
    private string _testDirectory = null!;

    [SetUp]
    public void CreateTestDirectory()
    {
        _testDirectory = Path.Combine(Path.GetTempPath(), $"Kamishibai-{Guid.NewGuid():N}");
        Directory.CreateDirectory(_testDirectory);
    }

    [TearDown]
    public void DeleteTestDirectory()
    {
        if (Directory.Exists(_testDirectory)) Directory.Delete(_testDirectory, true);
    }

    [Test]
    public void SaveFile()
    {
        var mainWindow = app.AttachMainWindow();
        var saveFilePage = app.AttachSaveFilePage();
        mainWindow.SelectedMenuItem.EmulateChangeSelectedIndex(5);

        saveFilePage.InitialDirectory.EmulateChangeText(_testDirectory);

        var async = new Async();
        saveFilePage.SaveFileCommand.EmulateClick(async);
        Thread.Sleep(500);
        saveFilePage.ErrorMessage.Text.Should().BeEmpty();
        var saveFileDialog = app.Attach_SaveFileDialog(@"名前を付けて保存");
        saveFileDialog.ComboBox_FileName.EmulateChangeEditText("foo");
        saveFileDialog.Button_Save.EmulateClick();
        async.WaitForCompletion();

        saveFilePage.OpenFileResult.Text.Should().Be("Ok");
        saveFilePage.FilePath.Text.Should().Be(Path.Combine(_testDirectory, "foo.png"));
    }

    [Test]
    public void CancelSaveFile()
    {
        var mainWindow = app.AttachMainWindow();
        var saveFilePage = app.AttachSaveFilePage();
        mainWindow.SelectedMenuItem.EmulateChangeSelectedIndex(5);

        saveFilePage.InitialDirectory.EmulateChangeText(_testDirectory);

        var async = new Async();
        saveFilePage.SaveFileCommand.EmulateClick(async);
        Thread.Sleep(500);
        saveFilePage.ErrorMessage.Text.Should().BeEmpty();
        var saveFileDialog = app.Attach_SaveFileDialog(@"名前を付けて保存");
        saveFileDialog.ComboBox_FileName.EmulateChangeEditText("foo");
        saveFileDialog.Button_Cancel.EmulateClick();
        async.WaitForCompletion();

        saveFilePage.OpenFileResult.Text.Should().Be("Cancel");
        saveFilePage.FilePath.Text.Should().BeEmpty();
    }

}
