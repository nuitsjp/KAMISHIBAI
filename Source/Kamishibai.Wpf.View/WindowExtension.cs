using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Kamishibai.Wpf.View;

[MarkupExtensionReturnType(typeof(Window))]
public class WindowExtension : MarkupExtension
{
    private readonly Binding _binding = new()
    {
        RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(Window), 1)
    };

    public override object? ProvideValue(IServiceProvider serviceProvider)
        => _binding.ProvideValue(serviceProvider);
}