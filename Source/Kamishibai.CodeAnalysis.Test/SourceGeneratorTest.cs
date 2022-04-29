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

    [Fact]
    public async Task Should_be_generated_methods_When_navigate_attribute_specified()
    {
        var code = @"
using Kamishibai;

[Navigate]
public class Foo
{
    public Foo()
    {
    }
}";

        await code.GenerateSource().Should().BeAsync(
            @"using System;
using System.Threading.Tasks;
using Kamishibai;

namespace TestProject
{
    public partial interface IPresentationService : IPresentationServiceBase
    {
        Task<bool> NavigateToFooAsync(string frameName = """");

    }

    public class PresentationService : PresentationServiceBase, IPresentationService
    {
        private readonly IServiceProvider _serviceProvider;

        public PresentationService(IServiceProvider serviceProvider, INavigationFrameProvider navigationFrameProvider, IWindowService windowService)
            : base (serviceProvider, navigationFrameProvider, windowService)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<bool> NavigateToFooAsync(string frameName = """")
        {
            return NavigateAsync(
                new Foo(
                    
                ), 
                frameName);
        }
    }
}");
    }
}