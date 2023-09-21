using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Kamishibai.View.Test.PresentationServiceTest;

public abstract class PresentationServiceTestBase
{
    protected IServiceCollection Services { get; } = new ServiceCollection();
    protected NavigationFrame DefaultFrame { get; } = new();
    protected NavigationFrame NamedFrame { get; } = new(){FrameName = "Named"};
    protected NavigationFrameProvider NavigationFrameProvider { get; } = new();
    protected Mock<IWindowService> WindowService { get; } = new();
    protected IPresentationService BuildService()
    {
        var serviceProvider = Services.BuildServiceProvider();
        return new PresentationService(
            serviceProvider,
            NavigationFrameProvider,
            new WindowService(serviceProvider));
    }
}