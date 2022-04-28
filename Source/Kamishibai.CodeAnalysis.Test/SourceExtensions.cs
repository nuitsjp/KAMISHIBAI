namespace Kamishibai.CodeAnalysis.Test;

public static class SourceExtensions
{
    public static SourceCode GenerateSource(this string source) => new(source);
}