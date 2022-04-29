using System.Threading.Tasks;
using Xunit;

namespace Kamishibai.CodeAnalysis.Test;

public class NotGeneratedTest
{
    [Fact]
    public async Task When_attribute_not_specified()
    {
        var code = @"
public class Foo
{
    public Foo()
    {
    }
}";

        await code.GenerateSource().Should().BeAsync(
            @"#nullable enable
using System;
using System.Threading.Tasks;
using Kamishibai;

namespace TestProject
{
    public partial interface IPresentationService : IPresentationServiceBase
    {
    }

    public class PresentationService : PresentationServiceBase, IPresentationService
    {
        private readonly IServiceProvider _serviceProvider;

        public PresentationService(IServiceProvider serviceProvider, INavigationFrameProvider navigationFrameProvider, IWindowService windowService)
            : base (serviceProvider, navigationFrameProvider, windowService)
        {
            _serviceProvider = serviceProvider;
        }

    }
}");
    }

}