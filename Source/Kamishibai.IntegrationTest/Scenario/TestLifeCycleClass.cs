using Codeer.Friendly.Windows;
using Driver.TestController;
using NUnit.Framework;
using System.Threading;

namespace Scenario
{
    [TestFixture]
    public class TestLifeCycleClass
    {
        // ReSharper disable once ArrangeTypeMemberModifiers
        // ReSharper disable once NotAccessedField.Local
        WindowsAppFriend _app;

        [SetUp]
        public void SetUp() => _app = LifeCycleClassTest.Start();

        [TearDown]
        public void TearDown() => LifeCycleClassTest.Clear();

        [OneTimeTearDown]
        public void OneTimeTearDown() => LifeCycleClassTest.End();

        [TestCase, Apartment(ApartmentState.STA), TimeoutEx(1000 * 60)]
        public void TestMethod1()
        {

        }

        [TestCase, Apartment(ApartmentState.STA), TimeoutEx(1000 * 60)]
        public void TestMethod2()
        {

        }
    }
}
