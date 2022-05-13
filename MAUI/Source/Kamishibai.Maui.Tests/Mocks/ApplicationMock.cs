using System.Collections.Generic;
using Moq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;

namespace Kamishibai.Maui.Tests.Mocks
{
    public class ApplicationMock : Application
    {
        static ApplicationMock()
        {
            DependencyService.Register<ISystemResourcesProvider, SystemResourcesProvider>();
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class SystemResourcesProvider : ISystemResourcesProvider
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            readonly IList<KeyValuePair<string, object>> _keyValuePairs = new List<KeyValuePair<string, object>>();
            public IResourceDictionary GetSystemResources()
            {
                var resourceDictionaryMock = new Mock<IResourceDictionary>();
	            resourceDictionaryMock.Setup(m => m.GetEnumerator()).Returns(() => _keyValuePairs.GetEnumerator());
                return resourceDictionaryMock.Object;
            }
        }

    }
}
