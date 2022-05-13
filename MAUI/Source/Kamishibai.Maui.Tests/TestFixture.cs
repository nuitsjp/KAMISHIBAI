using Kamishibai.Maui.Tests.Mocks;

namespace Kamishibai.Maui.Tests
{
	public abstract class TestFixture
	{
		protected TestFixture()
		{
			Microsoft.Maui.Dispatching.DispatcherProvider.SetCurrent(new DispatcherProviderStub());
		}
	}
}

