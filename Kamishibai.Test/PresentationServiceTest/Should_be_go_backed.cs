using System.Threading.Tasks;
using System.Windows.Controls;
using FluentAssertions;
using Xunit;

namespace Kamishibai.Test.PresentationServiceTest;

// ReSharper disable once InconsistentNaming
public class Should_be_go_backed : PresentationServiceTestBase
{
    [WpfFact]
    public async Task When_call()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstViewModel>();
        Services.AddPresentation<SecondPage, SecondViewModel>();
        var service = BuildService();
        #endregion

        await service.NavigateAsync<FirstViewModel>();
        await service.NavigateAsync<SecondViewModel>();
        DefaultFrame.Current.Should().BeOfType<SecondPage>();
        DefaultFrame.Count.Should().Be(2);

        service.GetNavigationFrame().CanGoBack.Should().BeTrue();
        await service.GoBackAsync();
        DefaultFrame.Current.Should().BeOfType<FirstPage>();
        DefaultFrame.Count.Should().Be(1);

        service.GetNavigationFrame().CanGoBack.Should().BeFalse();
        (await service.GoBackAsync()).Should().BeFalse();
    }

    public class FirstViewModel {}
    public class SecondViewModel {}
    public class FirstPage : UserControl { }
    public class SecondPage : UserControl { }

}