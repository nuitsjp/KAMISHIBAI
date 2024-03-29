﻿using Microsoft.CodeAnalysis;
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
        if (syntaxReceiver.Navigates.Empty()
            && syntaxReceiver.OpenDialogs.Empty()
            && syntaxReceiver.OpenWindows.Empty())
        {
            return;
        }

        var navigationInfos =
            syntaxReceiver
                .Navigates
                .SelectMany(type => type.HasConstructors()
                    ? type.GetConstructors().Select(constructor => new NavigationInfo(context, type, constructor))
                    : new []{ new NavigationInfo(context, type)})
                .ToList();

        var openWindowInfos =
            syntaxReceiver
                .OpenWindows
                .SelectMany(type => type.HasConstructors()
                    ? type.GetConstructors().Select(constructor => new OpenWindowInfo(context, type, constructor))
                    : new[] { new OpenWindowInfo(context, type) })
                .ToList();

        var openDialogInfos =
            syntaxReceiver
                .OpenDialogs
                .SelectMany(type => type.HasConstructors()
                    ? type.GetConstructors().Select(constructor => new OpenDialogInfo(context, type, constructor))
                    : new[] { new OpenDialogInfo(context, type) })
                .ToList();

        var source =
            new PresentationServiceTemplate(
                context.Compilation.AssemblyName!,
                navigationInfos,
                openWindowInfos,
                openDialogInfos
            ).TransformText();
        context.AddSource("IPresentationService.cs", source);
    }

    class SyntaxReceiver : ISyntaxReceiver
    {
        public List<TypeDeclarationSyntax> Navigates { get; } = new();
        public List<TypeDeclarationSyntax> OpenWindows { get; } = new();
        public List<TypeDeclarationSyntax> OpenDialogs { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is TypeDeclarationSyntax {AttributeLists.Count: > 0} typeDeclarationSyntax)
            {
                // Comparable is not declared.
                var navigates =
                    typeDeclarationSyntax
                        .AttributeLists
                        .SelectMany(x => x.Attributes)
                        .FirstOrDefault(x => x.Name.ToString() is "Navigate" or "NavigateAttribute");
                if (navigates is not null) Navigates.Add(typeDeclarationSyntax);

                var openWindows =
                    typeDeclarationSyntax
                        .AttributeLists
                        .SelectMany(x => x.Attributes)
                        .FirstOrDefault(x => x.Name.ToString() is "OpenWindow" or "OpenWindowAttribute");
                if (openWindows is not null) OpenWindows.Add(typeDeclarationSyntax);

                var openDialogs =
                    typeDeclarationSyntax
                        .AttributeLists
                        .SelectMany(x => x.Attributes)
                        .FirstOrDefault(x => x.Name.ToString() is "OpenDialog" or "OpenDialogAttribute");
                if (openDialogs is not null) OpenDialogs.Add(typeDeclarationSyntax);
            }
        }
    }
}