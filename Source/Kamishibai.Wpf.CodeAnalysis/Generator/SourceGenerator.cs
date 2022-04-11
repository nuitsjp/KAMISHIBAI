using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Kamishibai.Wpf.CodeAnalysis.Generator
{
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
            context.AddSource(
                "INavigationService.cs", 
                new NavigationServiceTemplate(
                    "Kamishibai.Wpf.Demo.ViewModel"
                    ).TransformText());
        }

        class SyntaxReceiver : ISyntaxReceiver
        {
            public List<TypeDeclarationSyntax> Targets { get; } = new();

            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                if (syntaxNode is TypeDeclarationSyntax typeDeclarationSyntax && typeDeclarationSyntax.AttributeLists.Count > 0)
                {
                    // Comparable is not declared.
                    var attr =
                        typeDeclarationSyntax
                            .AttributeLists
                            .SelectMany(x => x.Attributes)
                            .FirstOrDefault(x => x.Name.ToString() is "Comparable" or "ComparableAttribute");
                    if (attr == null) return;

                    Targets.Add(typeDeclarationSyntax);
                }
            }
        }

    }
}
