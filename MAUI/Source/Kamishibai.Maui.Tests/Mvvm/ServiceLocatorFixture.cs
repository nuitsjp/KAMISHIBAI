using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kamishibai.Maui.Mvvm;
using Xunit;

namespace Kamishibai.Maui.Tests.Mvvm
{
    public class ServiceLocatorFixture
    {
        [Fact]
        public void SetLocator()
        {
            ServiceLocator.SetLocator(type =>
            {
                if (type == typeof(Mock))
                {
                    return new Mock();
                }
                else
                {
                    return ServiceLocator.DefaultLocator(type);
                }
            });
        }

        private class Mock
        {
            
        }
    }
}
