using System.Threading.Tasks;
using Xunit;

namespace Kamishibai.CodeAnalysis.Test;

public class GenerateNavigateTest
{
    [Fact]
    public async Task When_constructor_missing()
    {
        var code = @"
using Kamishibai;

[Navigate]
public class Foo
{
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
    public async Task When_Navigate_specified()
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
            @"#nullable enable
using System;
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
    public async Task When_NavigateAttribute_specified()
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
            @"#nullable enable
using System;
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
            @"#nullable enable
using System;
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

    [Fact]
    public async Task When_navigation_with_arguments_include_frameName()
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
        public Bar(int number, string frameName, Argument argument)
        {
        }
    }
}
";

        await code.GenerateSource().Should().BeAsync(
            @"#nullable enable
using System;
using System.Threading.Tasks;
using Kamishibai;

namespace TestProject
{
    public partial interface IPresentationService : IPresentationServiceBase
    {
        Task<bool> NavigateToBarAsync(int number, string frameName, Foo.Argument argument);
    }

    public class PresentationService : PresentationServiceBase, IPresentationService
    {
        private readonly IServiceProvider _serviceProvider;

        public PresentationService(IServiceProvider serviceProvider, INavigationFrameProvider navigationFrameProvider, IWindowService windowService)
            : base (serviceProvider, navigationFrameProvider, windowService)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<bool> NavigateToBarAsync(int number, string frameName, Foo.Argument argument)
        {
            return NavigateAsync(
                new Foo.Bar(
                    number,
                    frameName,
                    argument
                ), 
                frameName);
        }
    }
}");
    }

    [Fact]
    public async Task When_navigation_with_arguments_include_Inject()
    {
        var code = @"
using System.Collections.Generic;
using Kamishibai;

namespace Foo
{
    public class Argument
    {
    }

    [Navigate]
    public class Bar
    {
        public Bar(int number, [Inject]IList<string> items, Argument argument)
        {
        }
    }
}
";

        await code.GenerateSource().Should().BeAsync(
            @"#nullable enable
using System;
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
                    (System.Collections.Generic.IList<string>)_serviceProvider.GetService(typeof(System.Collections.Generic.IList<string>))!,
                    argument
                ), 
                frameName);
        }
    }
}");
    }

    [Fact]
    public async Task When_navigation_with_nullable_arguments()
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
        public Bar(int? number, Argument argument)
        {
        }
    }
}
";

        await code.GenerateSource().Should().BeAsync(
            @"#nullable enable
using System;
using System.Threading.Tasks;
using Kamishibai;

namespace TestProject
{
    public partial interface IPresentationService : IPresentationServiceBase
    {
        Task<bool> NavigateToBarAsync(int? number, Foo.Argument argument, string frameName = """");
    }

    public class PresentationService : PresentationServiceBase, IPresentationService
    {
        private readonly IServiceProvider _serviceProvider;

        public PresentationService(IServiceProvider serviceProvider, INavigationFrameProvider navigationFrameProvider, IWindowService windowService)
            : base (serviceProvider, navigationFrameProvider, windowService)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<bool> NavigateToBarAsync(int? number, Foo.Argument argument, string frameName = """")
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