using System.Windows;
using Kamishibai.Wpf.View;

namespace Kamishibai.Wpf.Extensions.Hosting;

public interface IApplicationConfigurator<in TApplication, in TShell>
    where TApplication : Application
    where TShell : Window, IShell

{
    void Configure(TApplication application, TShell shell);
}
public class ApplicationConfigurator<TApplication, TShell> : IApplicationConfigurator<TApplication, TShell>
    where TApplication : Application
    where TShell : Window, IShell
{
    private readonly Action<TApplication, TShell> _configureDelegate;

    public ApplicationConfigurator(Action<TApplication, TShell> configureDelegate)
    {
        _configureDelegate = configureDelegate;
    }

    public void Configure(TApplication application, TShell shell)
        => _configureDelegate(application, shell);
}