using System.Collections.Generic;
using Moq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Kamishibai.Xamarin.Forms.Tests.Mocks
{
    public class ApplicationMock : Application
    {
        static ApplicationMock()
        {
            var platformService = new Mock<IPlatformServices>();
            Device.PlatformServices = platformService.Object;

            DependencyService.Register<ISystemResourcesProvider, SystemResourcesProvider>();
        }

        private class SystemResourcesProvider : ISystemResourcesProvider
        {
	        IList<KeyValuePair<string, object>> KeyValuePairs = new List<KeyValuePair<string, object>>();
            public IResourceDictionary GetSystemResources()
            {
                var resourceDictionaryMock = new Mock<IResourceDictionary>();
	            resourceDictionaryMock.Setup(m => m.GetEnumerator()).Returns(() => KeyValuePairs.GetEnumerator());
                return resourceDictionaryMock.Object;
            }
        }

    }
}
