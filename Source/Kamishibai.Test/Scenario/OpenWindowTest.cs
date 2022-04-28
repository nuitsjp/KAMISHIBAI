using Codeer.Friendly.Windows;
using Driver.TestController;
using NUnit.Framework;

namespace Scenario;

[TestFixture]

public class OpenWindowTest
{
    WindowsAppFriend _app;

    [SetUp]
    public void TestInitialize() => _app = ProcessController.Start();

    [TearDown]
    public void TestCleanup() => _app.Kill();

}