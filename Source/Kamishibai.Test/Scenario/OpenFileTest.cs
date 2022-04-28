using System.IO;
using NUnit.Framework;
using Codeer.Friendly;
using Driver.Windows;
using Driver.Windows.Native;
using FluentAssertions;

namespace Scenario;

[TestFixture]
public class OpenFileTest : TestBase
{
    private const string FilePath1 = @"test1.txt";
    private const string FilePath2 = @"test1.txt";
    [SetUp]
    public void CreateTestFiles()
    {
        File.WriteAllText(FilePath1, "Hello, OpenFile!");
        File.WriteAllText(FilePath2, "Hello, OpenFile!");
    }

    [TearDown]
    public void CleanTestFiles()
    {
        if (File.Exists(FilePath1)) File.Delete(FilePath1);
        if (File.Exists(FilePath2)) File.Delete(FilePath2);
    }


    [Test]
    public void OpenSingleFile()
    {
        var mainWindow = app.AttachMainWindow();
        var openFilePage = app.AttachOpenFilePage();
        mainWindow.SelectedMenuItem.EmulateChangeSelectedIndex(4);

        openFilePage.InitialDirectory.EmulateChangeText(Directory.GetCurrentDirectory());

        var async = new Async();
        openFilePage.OpenFileCommand.EmulateClick(async);
        var openFileDialog = app.Attach_OpenFileDialog(@"開く");
        openFileDialog.ComboBox_FilePath.EmulateChangeEditText(FilePath1);
        openFileDialog.Button_Open.EmulateClick();
        async.WaitForCompletion();

        openFilePage.OpenFileResult.Text.Should().Be("Ok");
        openFilePage.FilePath.Text.Should().Be(Path.Combine(Directory.GetCurrentDirectory(), FilePath1));
    }

    [Test]
    public void OpenMultiFile()
    {
        var mainWindow = app.AttachMainWindow();
        var openFilePage = app.AttachOpenFilePage();
        mainWindow.SelectedMenuItem.EmulateChangeSelectedIndex(4);

        openFilePage.InitialDirectory.EmulateChangeText(Directory.GetCurrentDirectory());
        openFilePage.Multiselect.EmulateCheck(true);

        var async = new Async();
        openFilePage.OpenFileCommand.EmulateClick(async);
        var openFileDialog = app.Attach_OpenFileDialog(@"開く");
        openFileDialog.ComboBox_FilePath.EmulateChangeEditText($@"""{FilePath1}"" ""{FilePath2}""");
        openFileDialog.Button_Open.EmulateClick();
        async.WaitForCompletion();

        openFilePage.OpenFileResult.Text.Should().Be("Ok");
        openFilePage.FilePath.Text.Should().Be(
            string.Join(
                "; ",
                Path.Combine(Directory.GetCurrentDirectory(), FilePath1),
                Path.Combine(Directory.GetCurrentDirectory(), FilePath2)));
    }

}