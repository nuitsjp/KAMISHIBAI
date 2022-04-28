using Codeer.Friendly.Windows;
using Driver.TestController;
using NUnit.Framework;

namespace Scenario;

public class TestBase
{
    // ReSharper disable once InconsistentNaming
    protected WindowsAppFriend app;

    [SetUp]
    public void TestInitialize() => app = ProcessController.Start();

    [TearDown]
    public void TestCleanup() => app.Kill();

}