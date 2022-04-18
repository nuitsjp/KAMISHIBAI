namespace Kamishibai.CodeAnalysis.Generator;

public partial class PresentationServiceTemplate
{
    public PresentationServiceTemplate(string ns, List<NavigationInfo> navigationInfos)
    {
        Namespace = ns;
        NavigationInfos = navigationInfos;
    }

    public string Namespace { get; }

    public List<NavigationInfo> NavigationInfos { get; }
}