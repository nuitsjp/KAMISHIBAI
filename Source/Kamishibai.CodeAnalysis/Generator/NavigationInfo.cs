namespace Kamishibai.CodeAnalysis.Generator;

public class NavigationInfo
{
    public NavigationInfo(string navigationName, string viewModelName)
    {
        NavigationName = navigationName;
        ViewModelName = viewModelName;
    }

    public string NavigationName { get; }
    public string ViewModelName { get; }

    public string NavigationParameters
    {
        get
        {
            var paramStrings =
                Parameters
                    .Where(x => x.IsInjection is false)
                    .Select(x => $"{x.Type} {x.Name}")
                    .ToList();
            if (Parameters.All(x => x.Name != "frameName"))
            {
                paramStrings.Add("string frameName = \"\"");
            }

            return string.Join(", ", paramStrings);
        }
    }

    public string ConstructorParameters
    {
        get
        {
            return string.Join(
                @", 
                ",
                Parameters
                    .Select(x => 
                        x.Type == "INavigationService" 
                            ? "this" 
                            : x.IsInjection
                                ? $"({x.Type})_serviceProvider.GetService(typeof({x.Type}))!"
                                : x.Name)
                    .ToList());
        }
    }

    public IList<NavigationParameter> Parameters { get; } = new List<NavigationParameter>();
}