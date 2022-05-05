using System.Windows.Controls;
using FluentAssertions;
using Xunit;

namespace Kamishibai.View.Test.PresentationServiceTest;

// ReSharper disable once InconsistentNaming
[Collection("KAMISHIBAI")]
public class Should_be_canceled : PresentationServiceTestBase
{
    [WpfFact]
    public async Task By_Pausing()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstViewModel>();
        Services.AddPresentation<SecondPage, SecondViewModel>();
        var service = BuildService();
        #endregion

        await service.NavigateAsync(new FirstViewModel(true));
        DefaultFrame.Count.Should().Be(1);

        await service.NavigateAsync(new SecondViewModel());
        DefaultFrame.Current.GetType().Should().Be(typeof(FirstPage));
        DefaultFrame.Count.Should().Be(1);
    }

    [WpfFact]
    public async Task By_PausingAsync()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstViewModel>();
        Services.AddPresentation<SecondPage, SecondViewModel>();
        var service = BuildService();
        #endregion

        await service.NavigateAsync(new FirstViewModel(cancelPausingAsync: true));
        DefaultFrame.Count.Should().Be(1);

        await service.NavigateAsync(new SecondViewModel());
        DefaultFrame.Current.GetType().Should().Be(typeof(FirstPage));
        DefaultFrame.Count.Should().Be(1);
    }

    [WpfFact]
    public async Task By_Navigating()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstViewModel>();
        Services.AddPresentation<SecondPage, SecondViewModel>();
        var service = BuildService();
        #endregion

        await service.NavigateAsync(new FirstViewModel());
        DefaultFrame.Count.Should().Be(1);

        await service.NavigateAsync(new SecondViewModel(true));
        DefaultFrame.Current.GetType().Should().Be(typeof(FirstPage));
        DefaultFrame.Count.Should().Be(1);
    }


    [WpfFact]
    public async Task By_NavigatingAsync()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstViewModel>();
        Services.AddPresentation<SecondPage, SecondViewModel>();
        var service = BuildService();
        #endregion

        await service.NavigateAsync(new FirstViewModel());
        DefaultFrame.Count.Should().Be(1);

        await service.NavigateAsync(new SecondViewModel(cancelNavigatingAsync:true));
        DefaultFrame.Current.GetType().Should().Be(typeof(FirstPage));
        DefaultFrame.Count.Should().Be(1);
    }

    [WpfFact]
    public async Task By_Disposing()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstViewModel>();
        Services.AddPresentation<SecondPage, SecondViewModel>();
        var service = BuildService();
        #endregion

        await service.NavigateAsync(new FirstViewModel());
        await service.NavigateAsync(new SecondViewModel(cancelDisposing:true));
        DefaultFrame.Count.Should().Be(2);

        await service.GoBackAsync();
        DefaultFrame.Current.Should().BeOfType<SecondPage>();
        DefaultFrame.Count.Should().Be(2);

    }

    [WpfFact]
    public async Task By_DisposingAsync()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstViewModel>();
        Services.AddPresentation<SecondPage, SecondViewModel>();
        var service = BuildService();
        #endregion

        await service.NavigateAsync(new FirstViewModel());
        await service.NavigateAsync(new SecondViewModel(cancelDisposingAsync: true));
        DefaultFrame.Count.Should().Be(2);

        await service.GoBackAsync();
        DefaultFrame.Current.Should().BeOfType<SecondPage>();
        DefaultFrame.Count.Should().Be(2);

    }

    [WpfFact]
    public async Task By_Resuming()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstViewModel>();
        Services.AddPresentation<SecondPage, SecondViewModel>();
        var service = BuildService();
        #endregion

        await service.NavigateAsync(new FirstViewModel(cancelResuming:true));
        await service.NavigateAsync(new SecondViewModel());
        DefaultFrame.Count.Should().Be(2);

        await service.GoBackAsync();
        DefaultFrame.Current.Should().BeOfType<SecondPage>();
        DefaultFrame.Count.Should().Be(2);

    }

    [WpfFact]
    public async Task By_ResumingAsync()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstViewModel>();
        Services.AddPresentation<SecondPage, SecondViewModel>();
        var service = BuildService();
        #endregion

        await service.NavigateAsync(new FirstViewModel(cancelResumingAsync:true));
        await service.NavigateAsync(new SecondViewModel());
        DefaultFrame.Count.Should().Be(2);

        await service.GoBackAsync();
        DefaultFrame.Current.Should().BeOfType<SecondPage>();
        DefaultFrame.Count.Should().Be(2);

    }

    public class FirstViewModel : IPausingAware, IPausingAsyncAware, IResumingAware, IResumingAsyncAware
    {
        private readonly bool _cancelPausing;
        private readonly bool _cancelPausingAsync;
        private readonly bool _cancelResuming;
        private readonly bool _cancelResumingAsync;

        public FirstViewModel(bool cancelPausing = false, bool cancelPausingAsync = false, bool cancelResuming = false, bool cancelResumingAsync = false)
        {
            _cancelPausing = cancelPausing;
            _cancelPausingAsync = cancelPausingAsync;
            _cancelResuming = cancelResuming;
            _cancelResumingAsync = cancelResumingAsync;
        }

        public void OnPausing(PreForwardEventArgs args)
        {
            args.Cancel = _cancelPausing;
        }

        public Task OnPausingAsync(PreForwardEventArgs args)
        {
            args.Cancel = _cancelPausingAsync;
            return Task.CompletedTask;
        }

        public void OnResuming(PreBackwardEventArgs args)
        {
            args.Cancel = _cancelResuming;
        }

        public Task OnResumingAsync(PreBackwardEventArgs args)
        {
            args.Cancel = _cancelResumingAsync;
            return Task.CompletedTask;
        }
    }
    public class SecondViewModel : INavigatingAware, INavigatingAsyncAware, IDisposingAware, IDisposingAsyncAware
    {
        private readonly bool _cancelNavigating;
        private readonly bool _cancelNavigatingAsync;
        private readonly bool _cancelDisposing;
        private readonly bool _cancelDisposingAsync;

        public SecondViewModel(bool cancelNavigating = false, bool cancelNavigatingAsync = false, bool cancelDisposing = false, bool cancelDisposingAsync = false)
        {
            _cancelNavigating = cancelNavigating;
            _cancelNavigatingAsync = cancelNavigatingAsync;
            _cancelDisposing = cancelDisposing;
            _cancelDisposingAsync = cancelDisposingAsync;
        }

        public void OnNavigating(PreForwardEventArgs args)
        {
            args.Cancel = _cancelNavigating;
        }

        public Task OnNavigatingAsync(PreForwardEventArgs args)
        {
            args.Cancel = _cancelNavigatingAsync;
            return Task.CompletedTask;
        }

        public void OnDisposing(PreBackwardEventArgs args)
        {
            args.Cancel = _cancelDisposing;
        }

        public Task OnDisposingAsync(PreBackwardEventArgs args)
        {
            args.Cancel = _cancelDisposingAsync;
            return Task.CompletedTask;
        }
    }
    public class FirstPage : UserControl { }
    public class SecondPage : UserControl { }

}