using Microsoft.Extensions.DependencyInjection;

namespace Kamishibai;

public static class WindowsApiCodePackServiceCollectionExtensions
{
    public static IServiceCollection AddWindowsApiCodePackFileDialogs(this IServiceCollection services)
    {
        services.AddSingleton<IFileDialogService, WindowsApiCodePackFileDialogService>();
        return services;
    }
}
