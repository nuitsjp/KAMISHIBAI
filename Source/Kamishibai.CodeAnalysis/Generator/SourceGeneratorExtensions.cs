using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Kamishibai.CodeAnalysis.Generator;

public static class SourceGeneratorExtensions
{
    public static string RemoveSuffix(this string value, string suffix)
    {
        var index = value.LastIndexOf("ViewModel", StringComparison.Ordinal);
        return 0 <= index ? value.Substring(0, index) : value;
    }

    public static IEnumerable<ConstructorDeclarationSyntax> GetConstructors(
        this TypeDeclarationSyntax typeDeclarationSyntax)
    {
        return typeDeclarationSyntax
            .Members
            .OfType<ConstructorDeclarationSyntax>();
    }

    public static string ToNavigationName(this string viewModelName)
    {
        return viewModelName
            .RemoveSuffix("ViewModel")
            .RemoveSuffix("Page")
            .RemoveSuffix("Window")
            .RemoveSuffix("Dialog");
    }

    public static IEnumerable<NavigationParameter> CreateNavigationParameters(this ConstructorDeclarationSyntax constructorDeclarationSyntax, SemanticModel semanticModel)
    {
        foreach (var parameterSyntax in constructorDeclarationSyntax.ParameterList.Parameters)
        {
            var info = semanticModel.GetSymbolInfo(parameterSyntax.Type!).Symbol;
            var typeName = info?.ToString() ?? ((IdentifierNameSyntax)parameterSyntax.Type!).Identifier.Text;
            var isInjection = parameterSyntax.AttributeLists
                .SelectMany(x => x.Attributes)
                .Any(x => x.Name.ToString() is "Inject" or "InjectAttribute");
            yield return new NavigationParameter(typeName, parameterSyntax.Identifier.Text, isInjection);
        }
    }
}