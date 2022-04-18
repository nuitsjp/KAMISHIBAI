using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Kamishibai;

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