using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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
        Services.AddPresentation<SecondPage, SecondWithOutArgumentsViewModel>();
        var service = BuildService();
        #endregion

        await service.NavigateAsync(typeof(FirstWithOutArgumentsViewModel));
        DefaultFrame.Current.GetType().Should().Be(typeof(FirstPage));
        DefaultFrame.Count.Should().Be(1);

        await service.NavigateAsync(typeof(SecondWithOutArgumentsViewModel));
        DefaultFrame.Current.GetType().Should().Be(typeof(SecondPage));
        DefaultFrame.Count.Should().Be(2);
    }

    [WpfFact]
    public async Task By_ViewModel_generic_type()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstWithOutArgumentsViewModel>();
        Services.AddPresentation<SecondPage, SecondWithOutArgumentsViewModel>();
        var service = BuildService();
        #endregion

        await service.NavigateAsync<FirstWithOutArgumentsViewModel>();
        DefaultFrame.Current.GetType().Should().Be(typeof(FirstPage));
        DefaultFrame.Count.Should().Be(1);

        await service.NavigateAsync<SecondWithOutArgumentsViewModel>();
        DefaultFrame.Current.GetType().Should().Be(typeof(SecondPage));
        DefaultFrame.Count.Should().Be(2);
    }

    [WpfFact]
    public async Task By_ViewModel_instance()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstWithOutArgumentsViewModel>();
        Services.AddPresentation<SecondPage, SecondWithOutArgumentsViewModel>();
        var service = BuildService();
        #endregion

        await service.NavigateAsync(new FirstWithOutArgumentsViewModel());
        DefaultFrame.Current.GetType().Should().Be(typeof(FirstPage));
        DefaultFrame.Count.Should().Be(1);

        await service.NavigateAsync(new SecondWithOutArgumentsViewModel());
        DefaultFrame.Current.GetType().Should().Be(typeof(SecondPage));
        DefaultFrame.Count.Should().Be(2);
    }


    [WpfFact]
    public async Task With_callback_initializer()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstWithOutArgumentsViewModel>();
        Services.AddPresentation<SecondPage, SecondWithOutArgumentsViewModel>();
        var service = BuildService();
        #endregion

        FirstWithOutArgumentsViewModel? firstWithOutArgumentsViewModel = null;
        await service.NavigateAsync<FirstWithOutArgumentsViewModel>(viewModel => firstWithOutArgumentsViewModel = viewModel);
        DefaultFrame.Current.GetType().Should().Be(typeof(FirstPage));
        DefaultFrame.Current.DataContext.Should().Be(firstWithOutArgumentsViewModel);
        DefaultFrame.Count.Should().Be(1);

        SecondWithOutArgumentsViewModel? secondWithOutViewModel = null;
        await service.NavigateAsync<SecondWithOutArgumentsViewModel>(viewModel => secondWithOutViewModel = viewModel);
        DefaultFrame.Current.GetType().Should().Be(typeof(SecondPage));
        DefaultFrame.Current.DataContext.Should().Be(secondWithOutViewModel);
        DefaultFrame.Count.Should().Be(2);
    }

    [WpfFact]
    public async Task With_safe_parameter()
    {
        var messages = new List<string>();
        #region Setup
        Services.AddPresentation<FirstPage, FirstWithArgumentsViewModel>();
        Services.AddPresentation<SecondPage, SecondWithArgumentsViewModel>();
        Services.AddTransient<IPresentationService, PresentationService>();
        Services.AddTransient(_ => new Mock<IWindowService>().Object);
        Services.AddTransient<IList<string>>(_ => messages);
        var service = BuildService();
        #endregion

        int number = 100;
        await service.NavigateToFirstWithArgumentsAsync(number, NamedFrame.FrameName);
        NamedFrame.Current.GetType().Should().Be(typeof(FirstPage));
        NamedFrame.Current.DataContext.As<FirstWithArgumentsViewModel>().Number.Should().Be(number);
        NamedFrame.Current.DataContext.As<FirstWithArgumentsViewModel>().Messages.Should().BeEquivalentTo(messages);
        NamedFrame.Count.Should().Be(1);

        string message = "Hello, KAMISHIBAI!";
        await service.NavigateToSecondWithArgumentsAsync(message, NamedFrame.FrameName);
        NamedFrame.Current.GetType().Should().Be(typeof(SecondPage));
        NamedFrame.Current.DataContext.As<SecondWithArgumentsViewModel>().Message.Should().Be(message);
        NamedFrame.Count.Should().Be(2);
    }

    public class FirstWithOutArgumentsViewModel { }

    [Navigate]
    public class FirstWithArgumentsViewModel
    {
        public FirstWithArgumentsViewModel(
            int number, 
            [Inject] IList<string> messages)
        {
            Number = number;
            Messages = messages;
        }

        public int Number { get; }
        public IList<string> Messages { get; }
    }
    public class FirstPage : UserControl { }
    public class SecondWithOutArgumentsViewModel { }

    [Navigate]
    public class SecondWithArgumentsViewModel
    {
        public SecondWithArgumentsViewModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
    public class SecondPage : UserControl { }

}