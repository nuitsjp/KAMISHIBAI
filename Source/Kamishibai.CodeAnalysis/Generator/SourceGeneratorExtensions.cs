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

    public static bool HasPrimaryConstructor(
        this ClassDeclarationSyntax classDeclarationSyntax)
        => classDeclarationSyntax.ParameterList is not null;

    public static bool HasConstructors(
        this TypeDeclarationSyntax typeDeclarationSyntax)
    {
        return typeDeclarationSyntax
            .Members
            .OfType<ConstructorDeclarationSyntax>()
            .Any();
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
        => constructorDeclarationSyntax.ParameterList.Parameters.Select(p => CreateNavigationParameter(p, semanticModel));

    public static IEnumerable<NavigationParameter> CreateNavigationParameters(this ClassDeclarationSyntax classDeclarationSyntax, SemanticModel semanticModel)
    {
        var parameters = classDeclarationSyntax.ParameterList?.Parameters ?? throw new ArgumentException($"Class '{classDeclarationSyntax.Identifier.ValueText}' does not have a primary constructor");
        return parameters.Select(p => CreateNavigationParameter(p, semanticModel));
    }

    private static NavigationParameter CreateNavigationParameter(this ParameterSyntax parameterSyntax, SemanticModel semanticModel)
    {
        var info = semanticModel.GetSymbolInfo(parameterSyntax.Type!).Symbol;
        var typeName = info?.ToString() ?? ((IdentifierNameSyntax)parameterSyntax.Type!).Identifier.Text;
        var isInjection = parameterSyntax.AttributeLists
            .SelectMany(x => x.Attributes)
            .Any(x => x.Name.ToString() is "Inject" or "InjectAttribute");
        return new NavigationParameter(typeName, parameterSyntax.Identifier.Text, isInjection);
    }

    public static bool Empty<T>(this IEnumerable<T> enumerable) => enumerable.Any() is false;
}