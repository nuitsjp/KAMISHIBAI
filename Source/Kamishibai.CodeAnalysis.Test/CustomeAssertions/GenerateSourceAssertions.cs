using System.Text;
using System.Threading.Tasks;
using FluentAssertions.Primitives;
using Kamishibai.CodeAnalysis.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

// ReSharper disable once CheckNamespace
namespace Kamishibai.CodeAnalysis.Test;

public class GenerateSourceAssertions :
    ReferenceTypeAssertions<SourceCode, GenerateSourceAssertions>
{
    public GenerateSourceAssertions(SourceCode subject) : base(subject)
    {
    }

    protected override string Identifier => "navigationFrame";

    public async Task BeAsync(string expected, string because = "", params object[] becauseArgs)
    {
        await new CSharpSourceGeneratorVerifier<SourceGenerator>.Test
        {
            TestState =
            {
                Sources = { Subject.Text },
                GeneratedSources =
                {
                    (typeof(SourceGenerator), "IPresentationService.cs", SourceText.From(expected, Encoding.UTF8)),
                },
                AdditionalReferences =
                {
                    MetadataReference.CreateFromFile(typeof(OpenDialogAttribute).Assembly.Location)
                },
            },
        }.RunAsync();
    }
}