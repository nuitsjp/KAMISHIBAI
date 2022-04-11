namespace Kamishibai.Wpf.CodeAnalysis.Generator;

public partial class NavigationServiceTemplate
{
    public NavigationServiceTemplate(string ns)
    {
        Namespace = ns;
    }

    public string Namespace { get; }
}