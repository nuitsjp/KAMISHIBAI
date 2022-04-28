using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Kamishibai.CodeAnalysis.Test;

public class SourceGeneratorTest
{
    [Fact]
    public async Task Should_not_be_generated_methods_When_attribute_not_specified()
    {
        var code = @"
public class Foo
{
}";

        await code.GenerateSource().Should().BeAsync(
            @"using System;
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