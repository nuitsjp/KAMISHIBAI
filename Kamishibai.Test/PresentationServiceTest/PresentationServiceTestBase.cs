using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Kamishibai.Test.PresentationServiceTest;

public abstract class PresentationServiceTestBase
{
    protected IServiceCollection Services { get; } = new ServiceCollection();
    protected NavigationFrame DefaultFrame { get; } = new();
    protected NavigationFrame NamedFrame { get; } = new(){FrameName = "Named"};
    protected NavigationFrameProvider NavigationFrameProvider { get; } = new();
    protected Mock<IWindowService> WindowService { get; } = new();
    protected IPresentationServiceBase BuildService()
    {
        return new PresentationService(
            Services.BuildServiceProvider(),
            NavigationFrameProvider,
            WindowService.Object);
    }

    private class PresentationService : PresentationServiceBase
    {
        public PresentationService(IServiceProvider serviceProvider, INavigationFrameProvider navigationFrameProvider, IWindowService windowService)
            : base(serviceProvider, navigationFrameProvider, windowService)
        {
        }
    }
}