using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Kamishibai.View.Test;

public class FileDialogServiceTest
{
    [Fact]
    public void OpenFile_delegates_to_the_configured_service()
    {
        var context = new OpenFileDialogContext();
        var fileDialogService = new Mock<IFileDialogService>();
        fileDialogService.Setup(x => x.OpenFile(context)).Returns(DialogResult.Ok);
        var service = new WindowService(Mock.Of<IServiceProvider>(), fileDialogService.Object);

        var result = service.OpenFile(context);

        result.Should().Be(DialogResult.Ok);
        fileDialogService.Verify(x => x.OpenFile(context), Times.Once);
    }

    [Fact]
    public void SaveFile_delegates_to_the_configured_service()
    {
        var context = new SaveFileDialogContext();
        var fileDialogService = new Mock<IFileDialogService>();
        fileDialogService.Setup(x => x.SaveFile(context)).Returns(DialogResult.Cancel);
        var service = new WindowService(Mock.Of<IServiceProvider>(), fileDialogService.Object);

        var result = service.SaveFile(context);

        result.Should().Be(DialogResult.Cancel);
        fileDialogService.Verify(x => x.SaveFile(context), Times.Once);
    }

    [Fact]
    public void Windows_API_Code_Pack_implementation_can_be_registered()
    {
        using var serviceProvider = new ServiceCollection()
            .AddWindowsApiCodePackFileDialogs()
            .BuildServiceProvider();

        serviceProvider.GetRequiredService<IFileDialogService>()
            .Should().BeOfType<WindowsApiCodePackFileDialogService>();
    }
}
