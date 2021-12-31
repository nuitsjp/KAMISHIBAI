using System.Windows;
using System.Windows.Threading;
using Kamishibai.Wpf.View;
using Kamishibai.Wpf.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kamishibai.Wpf.Extensions.Hosting;

public class WpfHostedService<TApplication, TShell> : IHostedService
    where TApplication : Application
    where TShell : Window, IShell
{
    private readonly TApplication _application;
    private readonly TShell _shell;
    private readonly IApplicationConfigurator<TApplication, TShell> _applicationConfigurator;


    public WpfHostedService(TApplication application, TShell shell, IApplicationConfigurator<TApplication, TShell> applicationConfigurator)
    {
        _application = application;
        _application.DispatcherUnhandledException += (sender, args) =>
        {

        };
        _shell = shell;
        _applicationConfigurator = applicationConfigurator;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.CompletedTask;
        }

        _applicationConfigurator.Configure(_application, _shell);

        _application.Run(_shell);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}