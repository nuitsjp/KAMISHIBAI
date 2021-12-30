using System.Windows;
using System.Windows.Threading;
using Kamishibai.Wpf.View;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kamishibai.Wpf.Extensions.Hosting;

public class WpfHostedService : IHostedService
{
    private readonly Application _application;
    private readonly IShell _shell;

    public WpfHostedService(Application application, IShell shell)
    {
        _application = application;
        _shell = shell;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.CompletedTask;
        }

        _application.Run((Window)_shell);
        _application.Exit += (_, _) =>
        {
            StopAsync(cancellationToken);
        };
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}