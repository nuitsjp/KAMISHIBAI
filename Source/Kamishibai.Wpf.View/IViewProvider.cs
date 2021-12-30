using System.Windows;
using Kamishibai.Wpf.ViewModel;

namespace Kamishibai.Wpf.View;

public interface IViewProvider
{
    Application Application { get; }

    INavigationService BuildNavigationService();

    void AddPresentation<TView, TViewModel>()
        where TView : FrameworkElement
        where TViewModel : class;

    TViewModel Resolve<TViewModel>() where TViewModel : class;
    FrameworkElement ResolvePresentation<TViewModel>() where TViewModel : class;

    Window ResolveWindow<TViewModel>() where TViewModel : class, IWindowViewModel;
}