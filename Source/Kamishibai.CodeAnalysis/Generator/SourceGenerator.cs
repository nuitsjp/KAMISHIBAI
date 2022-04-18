using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Kamishibai.CodeAnalysis.Generator;

[Generator]
public class SourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
#if DEBUG
        if (!System.Diagnostics.Debugger.IsAttached)
        {
            //System.Diagnostics.Debugger.Launch();
        }
#endif

        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var syntaxReceiver = (SyntaxReceiver)context.SyntaxReceiver!;
        if (!syntaxReceiver.Targets.Any()) return;

        List<NavigationInfo> navigationInfos = new();

        foreach (var typeDeclarationSyntax in syntaxReceiver.Targets)
        {
            var model = context.Compilation.GetSemanticModel(typeDeclarationSyntax.SyntaxTree);
            var typeSymbol = model.GetDeclaredSymbol(typeDeclarationSyntax)!;
            var constructors = typeDeclarationSyntax
                .Members
                .OfType<ConstructorDeclarationSyntax>();
            foreach (var constructorDeclarationSyntax in constructors)
            {
                NavigationInfo navigationInfo = new(ToNavigationName(typeSymbol.Name), typeSymbol.Name);
                navigationInfos.Add(navigationInfo);
                foreach (var parameterSyntax in constructorDeclarationSyntax.ParameterList.Parameters)
                {
                    var info = model.GetSymbolInfo(parameterSyntax.Type!).Symbol;
                    var typeName = info?.ToString() ?? ((IdentifierNameSyntax)parameterSyntax.Type!).Identifier.Text;
                    var isInjection = parameterSyntax.AttributeLists
                        .SelectMany(x => x.Attributes)
                        .Any(x => x.Name.ToString() is "Inject" or "InjectAttribute");
                    navigationInfo.Parameters.Add(new NavigationParameter(typeName, parameterSyntax.Identifier.Text, isInjection));
                }
            }
        }

        var source =
            new PresentationServiceTemplate(
                context.Compilation.AssemblyName!,
                navigationInfos
            ).TransformText();
        context.AddSource((string) "INavigationService.cs", (string) source);
    }

    private static string ToNavigationName(string viewModelName)
    {
        return viewModelName.EndsWith("ViewModel")
            ? viewModelName.Substring(0, viewModelName.LastIndexOf("ViewModel", StringComparison.Ordinal))
            : viewModelName;
    }

    class SyntaxReceiver : ISyntaxReceiver
    {
        public List<TypeDeclarationSyntax> Targets { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is TypeDeclarationSyntax {AttributeLists.Count: > 0} typeDeclarationSyntax)
            {
                // Comparable is not declared.
                var attr =
                    typeDeclarationSyntax
                        .AttributeLists
                        .SelectMany(x => x.Attributes)
                        .FirstOrDefault(x => x.Name.ToString() is "Navigatable" or "NavigatableAttribute");
                if (attr == null) return;

                Targets.Add(typeDeclarationSyntax);
            }
        }
    }

}