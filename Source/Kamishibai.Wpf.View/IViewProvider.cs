using System.Windows;
using Kamishibai.Wpf.ViewModel;

namespace Kamishibai.Wpf.View;

public interface IViewProvider
{
    Application Application { get; }

    FrameworkElement ResolvePresentation<TViewModel>() where TViewModel : class;

    Window ResolveWindow<TViewModel>() where TViewModel : class, IWindowViewModel;
}