using System.Windows;
using Wpf.Extensions.Hosting;

namespace Kamishibai;

public class KamishibaiApplication<TApplication, TWindow>
    where TApplication : Application
    where TWindow : Window
{
    public static IWpfApplicationBuilder<TApplication, TWindow> CreateBuilder()
    {
        return new KamishibaiApplicationBuilder<TApplication, TWindow>(WpfApplication<TApplication, TWindow>.CreateBuilder());
    }
}