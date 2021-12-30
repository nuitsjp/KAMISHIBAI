using System.Windows;
using System.Windows.Threading;
using Kamishibai.Wpf.View;
using Kamishibai.Wpf.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kamishibai.Wpf.Extensions.Hosting;

public class WpfHostedService : IHostedService
{
    private readonly Application _application;
    private readonly Window _mainWindow;

    public WpfHostedService(Application application, IShell shell)
    {
        _application = application;
        _mainWindow = (Window)shell;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.CompletedTask;
        }

        if (_mainWindow.DataContext is INavigationAware navigationAware)
        {
            _mainWindow.Loaded += (_, _) =>
            {
                navigationAware.OnEntryAsync();
            };
        }
        _application.Run(_mainWindow);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}