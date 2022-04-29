using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Kamishibai.CodeAnalysis.Generator;

public class OpenDialogInfo
{
    public OpenDialogInfo(
        GeneratorExecutionContext context,
        SyntaxNode type,
        ConstructorDeclarationSyntax constructor)
    {
        var model = context.Compilation.GetSemanticModel(type.SyntaxTree);
        var symbol = model.GetDeclaredSymbol(type)!;
        NavigationName = symbol.Name.ToNavigationName();
        ViewModelName = symbol.ToString();
        Parameters = constructor.CreateNavigationParameters(model).ToList();
    }

    public OpenDialogInfo(
        GeneratorExecutionContext context,
        SyntaxNode type)
    {
        var model = context.Compilation.GetSemanticModel(type.SyntaxTree);
        var symbol = model.GetDeclaredSymbol(type)!;
        NavigationName = symbol.Name.ToNavigationName();
        ViewModelName = symbol.ToString();
        Parameters = new List<NavigationParameter>();
    }

    public string NavigationName { get; }
    public string ViewModelName { get; }
    public IList<NavigationParameter> Parameters { get; }

    public string NavigationParameters
    {
        get
        {
            var paramStrings =
                Parameters
                    .Where(x => x.IsInjection is false)
                    .Select(x => $"{x.Type} {x.Name}")
                    .ToList();
            paramStrings.Add("object? owner = null");
            paramStrings.Add("OpenDialogOptions? options = null");
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
}