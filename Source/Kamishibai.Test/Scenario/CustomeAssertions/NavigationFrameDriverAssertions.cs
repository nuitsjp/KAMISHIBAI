using Driver.Windows;
using FluentAssertions;
using FluentAssertions.Primitives;

// ReSharper disable once CheckNamespace
namespace Scenario;

public class NavigationFrameDriverAssertions :
    ReferenceTypeAssertions<NavigationFrameDriver, NavigationFrameDriverAssertions>
{
    public NavigationFrameDriverAssertions(NavigationFrameDriver subject) : base(subject)
    {
    }

    protected override string Identifier => "navigationFrame";

    public AndConstraint<NavigationFrameDriverAssertions> BeOfPage(string page, string because = "", params object[] becauseArgs)
    {
        Subject.Current.ToString().Should().Be(page);
        return new AndConstraint<NavigationFrameDriverAssertions>(this);
    }
}