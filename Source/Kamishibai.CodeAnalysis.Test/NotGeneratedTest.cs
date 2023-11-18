using Xunit;

namespace Kamishibai.CodeAnalysis.Test;

public class NotGeneratedTest
{
    [Fact]
    public async Task When_attribute_not_specified()
    {
        var code = @"
public class Foo
{
    public Foo()
    {
    }
}";

        await code.GenerateSource().Should().BeNotGenerated();
    }

}