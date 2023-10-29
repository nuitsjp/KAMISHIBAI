using System.Windows;
using System.Windows.Controls;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.Logging;
using Xunit;

namespace Kamishibai.View.Test.PresentationServiceTest;

// ReSharper disable once InconsistentNaming
[Collection("KAMISHIBAI")]
public class Should_be_notified : PresentationServiceTestBase
{
    private static readonly List<(string? Frame, Type sender, Type? Source, Type? Destination, string Event)> Logs = new();

    private static void AddLog(string? frame, object sender, object? source, object? destination, string @event) => 
        Logs.Add((frame, sender.GetType(), source?.GetType(), destination?.GetType(), @event));

    public Should_be_notified() => Logs.Clear();

    [WpfFact]
    public async Task When_navigating()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstViewModel>();
        Services.AddPresentation<SecondPage, SecondViewModel>();
        DefaultFrame.Pausing += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Pausing));
        DefaultFrame.Navigating += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Navigating));
        DefaultFrame.Navigated += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Navigated));
        DefaultFrame.Paused += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Paused));
        DefaultFrame.Disposing += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Disposing));
        DefaultFrame.Resuming += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Resuming));
        DefaultFrame.Resumed += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Resumed));
        DefaultFrame.Disposed += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Disposed));
        NamedFrame.Pausing += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Pausing));
        NamedFrame.Navigating += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Navigating));
        NamedFrame.Navigated += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Navigated));
        NamedFrame.Paused += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Paused));
        NamedFrame.Disposing += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Disposing));
        NamedFrame.Resuming += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Resuming));
        NamedFrame.Resumed += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Resumed));
        NamedFrame.Disposed += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Disposed));
        var service = BuildService();
        #endregion


        await service.NavigateAsync<FirstViewModel>(NamedFrame.FrameName);

        // Navigating
        Logs[0].Should().Be((NamedFrame.FrameName, typeof(FirstViewModel), null, typeof(FirstViewModel), nameof(INavigatingAsyncAware.OnNavigatingAsync)));
        Logs[1].Should().Be((NamedFrame.FrameName, typeof(FirstViewModel), null, typeof(FirstViewModel), nameof(INavigatingAware.OnNavigating)));
        Logs[2].Should().Be((NamedFrame.FrameName, typeof(NavigationFrame), null, typeof(FirstViewModel), nameof(INavigationFrame.Navigating)));
        // Navigated
        Logs[3].Should().Be((NamedFrame.FrameName, typeof(FirstViewModel), null, typeof(FirstViewModel), nameof(INavigatedAsyncAware.OnNavigatedAsync)));
        Logs[4].Should().Be((NamedFrame.FrameName, typeof(FirstViewModel), null, typeof(FirstViewModel), nameof(INavigatedAware.OnNavigated)));
        Logs[5].Should().Be((NamedFrame.FrameName, typeof(NavigationFrame), null, typeof(FirstViewModel), nameof(INavigationFrame.Navigated)));

        Logs.Clear();
        await service.NavigateAsync<SecondViewModel>(NamedFrame.FrameName);

        // Pausing
        Logs[0].Should().Be((NamedFrame.FrameName, typeof(FirstViewModel), typeof(FirstViewModel), typeof(SecondViewModel), nameof(IPausingAsyncAware.OnPausingAsync)));
        Logs[1].Should().Be((NamedFrame.FrameName, typeof(FirstViewModel), typeof(FirstViewModel), typeof(SecondViewModel), nameof(IPausingAware.OnPausing)));
        Logs[2].Should().Be((NamedFrame.FrameName, typeof(NavigationFrame), typeof(FirstViewModel), typeof(SecondViewModel), nameof(INavigationFrame.Pausing)));
        // Navigating
        Logs[3].Should().Be((NamedFrame.FrameName, typeof(SecondViewModel), typeof(FirstViewModel), typeof(SecondViewModel), nameof(INavigatingAsyncAware.OnNavigatingAsync)));
        Logs[4].Should().Be((NamedFrame.FrameName, typeof(SecondViewModel), typeof(FirstViewModel), typeof(SecondViewModel), nameof(INavigatingAware.OnNavigating)));
        Logs[5].Should().Be((NamedFrame.FrameName, typeof(NavigationFrame), typeof(FirstViewModel), typeof(SecondViewModel), nameof(INavigationFrame.Navigating)));
        // Navigating
        Logs[6].Should().Be((NamedFrame.FrameName, typeof(SecondViewModel), typeof(FirstViewModel), typeof(SecondViewModel), nameof(INavigatedAsyncAware.OnNavigatedAsync)));
        Logs[7].Should().Be((NamedFrame.FrameName, typeof(SecondViewModel), typeof(FirstViewModel), typeof(SecondViewModel), nameof(INavigatedAware.OnNavigated)));
        Logs[8].Should().Be((NamedFrame.FrameName, typeof(NavigationFrame), typeof(FirstViewModel), typeof(SecondViewModel), nameof(INavigationFrame.Navigated)));
        // Paused
        Logs[9].Should().Be((NamedFrame.FrameName, typeof(FirstViewModel), typeof(FirstViewModel), typeof(SecondViewModel), nameof(IPausedAsyncAware.OnPausedAsync)));
        Logs[10].Should().Be((NamedFrame.FrameName, typeof(FirstViewModel), typeof(FirstViewModel), typeof(SecondViewModel), nameof(IPausedAware.OnPaused)));
        Logs[11].Should().Be((NamedFrame.FrameName, typeof(NavigationFrame), typeof(FirstViewModel), typeof(SecondViewModel), nameof(INavigationFrame.Paused)));
    }

    [WpfFact]
    public async Task When_go_back()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstViewModel>();
        Services.AddPresentation<SecondPage, SecondViewModel>();
        DefaultFrame.Pausing += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Pausing));
        DefaultFrame.Navigating += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Navigating));
        DefaultFrame.Navigated += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Navigated));
        DefaultFrame.Paused += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Paused));
        DefaultFrame.Disposing += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Disposing));
        DefaultFrame.Resuming += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Resuming));
        DefaultFrame.Resumed += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Resumed));
        DefaultFrame.Disposed += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Disposed));
        NamedFrame.Pausing += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Pausing));
        NamedFrame.Navigating += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Navigating));
        NamedFrame.Navigated += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Navigated));
        NamedFrame.Paused += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Paused));
        NamedFrame.Disposing += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Disposing));
        NamedFrame.Resuming += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Resuming));
        NamedFrame.Resumed += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Resumed));
        NamedFrame.Disposed += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Disposed));
        var service = BuildService();
        #endregion


        await service.NavigateAsync<FirstViewModel>(NamedFrame.FrameName);
        await service.NavigateAsync<SecondViewModel>(NamedFrame.FrameName);
        Logs.Clear();
        await service.GoBackAsync(NamedFrame.FrameName);

        // Disposing
        Logs[0].Should().Be((NamedFrame.FrameName, typeof(SecondViewModel), typeof(SecondViewModel), typeof(FirstViewModel), nameof(IDisposingAsyncAware.OnDisposingAsync)));
        Logs[1].Should().Be((NamedFrame.FrameName, typeof(SecondViewModel), typeof(SecondViewModel), typeof(FirstViewModel), nameof(IDisposingAware.OnDisposing)));
        Logs[2].Should().Be((NamedFrame.FrameName, typeof(NavigationFrame), typeof(SecondViewModel), typeof(FirstViewModel), nameof(INavigationFrame.Disposing)));
        // Resuming
        Logs[3].Should().Be((NamedFrame.FrameName, typeof(FirstViewModel), typeof(SecondViewModel), typeof(FirstViewModel), nameof(IResumingAsyncAware.OnResumingAsync)));
        Logs[4].Should().Be((NamedFrame.FrameName, typeof(FirstViewModel), typeof(SecondViewModel), typeof(FirstViewModel), nameof(IResumingAware.OnResuming)));
        Logs[5].Should().Be((NamedFrame.FrameName, typeof(NavigationFrame), typeof(SecondViewModel), typeof(FirstViewModel), nameof(INavigationFrame.Resuming)));
        // Resumed
        Logs[6].Should().Be((NamedFrame.FrameName, typeof(FirstViewModel), typeof(SecondViewModel), typeof(FirstViewModel), nameof(IResumedAsyncAware.OnResumedAsync)));
        Logs[7].Should().Be((NamedFrame.FrameName, typeof(FirstViewModel), typeof(SecondViewModel), typeof(FirstViewModel), nameof(IResumedAware.OnResumed)));
        Logs[8].Should().Be((NamedFrame.FrameName, typeof(NavigationFrame), typeof(SecondViewModel), typeof(FirstViewModel), nameof(INavigationFrame.Resumed)));
        // Disposed
        Logs[9].Should().Be((NamedFrame.FrameName, typeof(SecondViewModel), typeof(SecondViewModel), typeof(FirstViewModel), nameof(IDisposedAsyncAware.OnDisposedAsync)));
        Logs[10].Should().Be((NamedFrame.FrameName, typeof(SecondViewModel), typeof(SecondViewModel), typeof(FirstViewModel), nameof(IDisposedAware.OnDisposed)));
        Logs[11].Should().Be((null, typeof(SecondViewModel), null, null, nameof(IDisposable.Dispose)));
        Logs[12].Should().Be((NamedFrame.FrameName, typeof(NavigationFrame), typeof(SecondViewModel), typeof(FirstViewModel), nameof(INavigationFrame.Disposed)));
    }

    [WpfFact]
    public async Task When_close_window()
    {
        #region Setup

        Services.AddSingleton(DefaultFrame);
        Services.AddPresentation<FirstPage, FirstViewModel>();
        Services.AddPresentation<SecondPage, SecondViewModel>();
        Services.AddPresentation<MainWindow, MainWindowViewModel>();
        DefaultFrame.Pausing += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Pausing));
        DefaultFrame.Navigating += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Navigating));
        DefaultFrame.Navigated += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Navigated));
        DefaultFrame.Paused += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Paused));
        DefaultFrame.Disposing += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Disposing));
        DefaultFrame.Resuming += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Resuming));
        DefaultFrame.Resumed += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Resumed));
        DefaultFrame.Disposed += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Disposed));
        NamedFrame.Pausing += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Pausing));
        NamedFrame.Navigating += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Navigating));
        NamedFrame.Navigated += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Navigated));
        NamedFrame.Paused += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Paused));
        NamedFrame.Disposing += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Disposing));
        NamedFrame.Resuming += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Resuming));
        NamedFrame.Resumed += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Resumed));
        NamedFrame.Disposed += (sender, args) => AddLog(args.FrameName, sender!, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Disposed));
        var service = BuildService();
        #endregion


        await service.OpenWindowAsync<MainWindowViewModel>();
        await service.NavigateAsync<FirstViewModel>();
        await service.NavigateAsync<SecondViewModel>();
        Logs.Should().NotBeEmpty();
        Logs.Clear();

        MainWindow.Instance.Should().NotBeNull();

        //If await, the Closed event will be called after the test case ends, so do not await, but wait in Sleep.
        await service.CloseWindowAsync(MainWindow.Instance);
        Logs.Should().NotBeEmpty();

        //await service.GoBackAsync(NamedFrame.FrameName);

        await Task.Delay(TimeSpan.FromMilliseconds(1000));
        //Thread.Sleep(TimeSpan.FromMilliseconds(5000));
        // Disposing
        var index = -1;
        Logs[++index].Should().Be((null, typeof(SecondViewModel), typeof(SecondViewModel), null, nameof(IDisposingAsyncAware.OnDisposingAsync)));
        Logs[++index].Should().Be((null, typeof(SecondViewModel), typeof(SecondViewModel), null, nameof(IDisposingAware.OnDisposing)));

        Logs[++index].Should().Be((null, typeof(MainWindowViewModel), typeof(MainWindowViewModel), null, nameof(IDisposingAsyncAware.OnDisposingAsync)));
        Logs[++index].Should().Be((null, typeof(MainWindowViewModel), typeof(MainWindowViewModel), null, nameof(IDisposingAware.OnDisposing)));

        // Disposed
        Logs[++index].Should().Be((null, typeof(SecondViewModel), typeof(SecondViewModel), null, nameof(IDisposedAsyncAware.OnDisposedAsync)));
        Logs[++index].Should().Be((null, typeof(SecondViewModel), null, null, nameof(IDisposable.Dispose)));

        Logs[++index].Should().Be((null, typeof(MainWindowViewModel), typeof(MainWindowViewModel), null, nameof(IDisposedAsyncAware.OnDisposedAsync)));
        Logs[++index].Should().Be((null, typeof(MainWindowViewModel), null, null, nameof(IDisposable.Dispose)));
    }

    public class MainWindowViewModel : NotificationViewModel
    {
        protected override void OnNotify(IForwardEventArgs args, string memberName = "")
            => AddLog(args.FrameName, this, args.SourceViewModel, args.DestinationViewModel, memberName);

        protected override void OnNotify(IBackwardEventArgs args, string memberName = "")
            => AddLog(args.FrameName, this, args.SourceViewModel, args.DestinationViewModel, memberName);
    }
    public class MainWindow : Window
    {
        public static MainWindow? Instance { get; private set; }

        public MainWindow(
            NavigationFrame navigationFrame)
        {
            Content = navigationFrame;
            Instance = this;
            Closed += (sender, args) =>
            {

            };
        }
    }

    public class FirstViewModel : NotificationViewModel
    {
        protected override void OnNotify(IForwardEventArgs args, string memberName = "")
            => AddLog(args.FrameName, this, args.SourceViewModel, args.DestinationViewModel, memberName);

        protected override void OnNotify(IBackwardEventArgs args, string memberName = "")
            => AddLog(args.FrameName, this, args.SourceViewModel, args.DestinationViewModel, memberName);
    }
    public class FirstPage : UserControl { }

    public class SecondViewModel : NotificationViewModel
    {
        protected override void OnNotify(IForwardEventArgs args, string memberName = "")
            => AddLog(args.FrameName, this, args.SourceViewModel, args.DestinationViewModel, memberName);
        protected override void OnNotify(IBackwardEventArgs args, string memberName = "")
            => AddLog(args.FrameName, this, args.SourceViewModel, args.DestinationViewModel, memberName);
    }
    public class SecondPage : UserControl { }
}