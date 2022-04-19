namespace Kamishibai.CodeAnalysis.Generator;

public partial class PresentationServiceTemplate
{
    public PresentationServiceTemplate(string ns, List<NavigationInfo> navigationInfos, List<OpenWindowInfo> openWindowInfos, List<OpenDialogInfo> openDialogInfos)
    {
        Namespace = ns;
        NavigationInfos = navigationInfos;
        OpenWindowInfos = openWindowInfos;
        OpenDialogInfos = openDialogInfos;
    }

    public string Namespace { get; }

    public List<NavigationInfo> NavigationInfos { get; }
    public List<OpenWindowInfo> OpenWindowInfos { get; }
    public List<OpenDialogInfo> OpenDialogInfos { get; }
}