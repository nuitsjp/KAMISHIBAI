namespace Kamishibai.CodeAnalysis.Test;

public static class SourceCodeExtensions
{
    public static GenerateSourceAssertions Should(this SourceCode instance)
    {
        return new GenerateSourceAssertions(instance);
    }
}