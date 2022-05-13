using System.Windows;
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

    public AndConstraint<NavigationFrameDriverAssertions> BeOfPage<TPage>(
        string because = "", 
        params object[] becauseArgs)
        where TPage : FrameworkElement
    {
        // Wait for animation to end.
        Thread.Sleep(TimeSpan.FromSeconds(0.3));

        Subject.Current.ToString().Should().Be(typeof(TPage).FullName, because, becauseArgs);
        return new AndConstraint<NavigationFrameDriverAssertions>(this);
    }
}