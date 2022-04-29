using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Kamishibai.CodeAnalysis.Test;

public class GenerateNavigateTest
{

    [Fact]
    public async Task When_navigate_specified()
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

    [Fact]
    public async Task When_navigate_attribute_specified()
    {
        var code = @"
using Kamishibai;

[NavigateAttribute]
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

    [Fact]
    public async Task When_navigation_with_arguments()
    {
        var code = @"
using Kamishibai;

namespace Foo
{
    public class Argument
    {
    }

    [Navigate]
    public class Bar
    {
        public Bar(int number, Argument argument)
        {
        }
    }
}
";

        await code.GenerateSource().Should().BeAsync(
            @"using System;
using System.Threading.Tasks;
using Kamishibai;

namespace TestProject
{
    public partial interface IPresentationService : IPresentationServiceBase
    {
        Task<bool> NavigateToBarAsync(int number, Foo.Argument argument, string frameName = """");

    }

    public class PresentationService : PresentationServiceBase, IPresentationService
    {
        private readonly IServiceProvider _serviceProvider;

        public PresentationService(IServiceProvider serviceProvider, INavigationFrameProvider navigationFrameProvider, IWindowService windowService)
            : base (serviceProvider, navigationFrameProvider, windowService)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<bool> NavigateToBarAsync(int number, Foo.Argument argument, string frameName = """")
        {
            return NavigateAsync(
                new Foo.Bar(
                    number, 
                    argument
                ), 
                frameName);
        }
    }
}");
    }


}