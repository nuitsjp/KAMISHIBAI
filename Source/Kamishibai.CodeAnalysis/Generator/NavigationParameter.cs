namespace Kamishibai.CodeAnalysis.Generator;

public class NavigationParameter
{
    public NavigationParameter(string type, string name, bool isInjection)
    {
        Type = type;
        Name = name;
        IsInjection = isInjection;
    }
    public string Type { get; }

    public string Name { get; }
    public bool IsInjection { get; }
}