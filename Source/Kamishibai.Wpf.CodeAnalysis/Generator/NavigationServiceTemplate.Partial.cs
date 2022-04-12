namespace Kamishibai.Wpf.CodeAnalysis.Generator;

public partial class NavigationServiceTemplate
{
    public NavigationServiceTemplate(string ns, List<NavigationInfo> navigationInfos)
    {
        Namespace = ns;
        NavigationInfos = navigationInfos;
    }

    public string Namespace { get; }

    public List<NavigationInfo> NavigationInfos { get; }
}