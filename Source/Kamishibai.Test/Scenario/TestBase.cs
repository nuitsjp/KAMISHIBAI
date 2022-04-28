using Codeer.Friendly.Windows;
using Driver.TestController;
using NUnit.Framework;

namespace Scenario;

public class TestBase
{
    // ReSharper disable once InconsistentNaming
    protected WindowsAppFriend _app;

    [SetUp]
    public void TestInitialize() => _app = ProcessController.Start();

    [TearDown]
    public void TestCleanup() => _app.Kill();

}