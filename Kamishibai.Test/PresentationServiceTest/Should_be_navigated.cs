using System.Threading.Tasks;
using System.Windows.Controls;
using FluentAssertions;
using Xunit;

namespace Kamishibai.Test.PresentationServiceTest;

// ReSharper disable once InconsistentNaming
[Collection("KAMISHIBAI")]
public class Should_be_navigated : PresentationServiceTestBase
{
    [WpfFact]
    public async Task By_ViewModel_type()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstWithOutArgumentsViewModel>();
        Services.AddPresentation<SecondPage, SecondWithOutViewModel>();
        var service = BuildService();
        #endregion

        await service.NavigateAsync(typeof(FirstWithOutArgumentsViewModel));
        DefaultFrame.Current.GetType().Should().Be(typeof(FirstPage));
        DefaultFrame.Count.Should().Be(1);

        await service.NavigateAsync(typeof(SecondWithOutViewModel));
        DefaultFrame.Current.GetType().Should().Be(typeof(SecondPage));
        DefaultFrame.Count.Should().Be(2);
    }

    [WpfFact]
    public async Task By_ViewModel_generic_type()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstWithOutArgumentsViewModel>();
        Services.AddPresentation<SecondPage, SecondWithOutViewModel>();
        var service = BuildService();
        #endregion

        await service.NavigateAsync<FirstWithOutArgumentsViewModel>();
        DefaultFrame.Current.GetType().Should().Be(typeof(FirstPage));
        DefaultFrame.Count.Should().Be(1);

        await service.NavigateAsync<SecondWithOutViewModel>();
        DefaultFrame.Current.GetType().Should().Be(typeof(SecondPage));
        DefaultFrame.Count.Should().Be(2);
    }

    [WpfFact]
    public async Task By_ViewModel_instance()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstWithOutArgumentsViewModel>();
        Services.AddPresentation<SecondPage, SecondWithOutViewModel>();
        var service = BuildService();
        #endregion

        await service.NavigateAsync(new FirstWithOutArgumentsViewModel());
        DefaultFrame.Current.GetType().Should().Be(typeof(FirstPage));
        DefaultFrame.Count.Should().Be(1);

        await service.NavigateAsync(new SecondWithOutViewModel());
        DefaultFrame.Current.GetType().Should().Be(typeof(SecondPage));
        DefaultFrame.Count.Should().Be(2);
    }


    public class FirstWithOutArgumentsViewModel { }
    public class FirstPage : UserControl { }
    public class SecondWithOutViewModel { }
    public class SecondPage : UserControl { }

}