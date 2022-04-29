using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using FluentAssertions;
using Microsoft.VisualBasic.Logging;
using Xunit;

namespace Kamishibai.Test.PresentationServiceTest;

// ReSharper disable once InconsistentNaming
[Collection("KAMISHIBAI")]
public class Should_be_notified : PresentationServiceTestBase
{
    private static readonly List<(string Frame, Type? Source, Type? Destination, string Event)> Logs = new();

    private static void AddLog(Type type, string @event) => Logs.Add((string.Empty, type, null, @event));
    private static void AddLog(string frame, object? source, object destination, string @event) => Logs.Add((frame, source?.GetType(), destination.GetType(), @event));

    public Should_be_notified() => Logs.Clear();

    [WpfFact]
    public async Task When_navigating()
    {
        #region Setup
        Services.AddPresentation<FirstPage, FirstViewModel>();
        Services.AddPresentation<SecondPage, SecondViewModel>();
        DefaultFrame.Pausing += (sender, args) => AddLog(args.FrameName, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Pausing));
        DefaultFrame.Navigating += (sender, args) => AddLog(args.FrameName, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Navigating));
        DefaultFrame.Navigated += (sender, args) => AddLog(args.FrameName, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Navigated));
        DefaultFrame.Paused += (sender, args) => AddLog(args.FrameName, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Paused));
        DefaultFrame.Disposing += (sender, args) => AddLog(args.FrameName, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Disposing));
        DefaultFrame.Resuming += (sender, args) => AddLog(args.FrameName, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Resuming));
        DefaultFrame.Resumed += (sender, args) => AddLog(args.FrameName, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Resumed));
        DefaultFrame.Disposed += (sender, args) => AddLog(args.FrameName, args.SourceViewModel, args.DestinationViewModel, nameof(INavigationFrame.Disposed));
        var service = BuildService();
        #endregion


        await service.NavigateAsync<FirstViewModel>(NamedFrame.Name);
        DefaultFrame.Current.GetType().Should().Be(typeof(FirstPage));
        DefaultFrame.Count.Should().Be(1);

        // Navigating
        Logs[0].Should().Be((string.Empty, typeof(FirstViewModel), null, nameof(INavigatingAsyncAware.OnNavigatingAsync)));
        Logs[1].Should().Be((string.Empty, typeof(FirstViewModel), null, nameof(INavigatingAware.OnNavigating)));
        Logs[2].Should().Be((string.Empty, null, typeof(FirstViewModel), nameof(INavigationFrame.Navigating)));
        // Navigated
        Logs[3].Should().Be((string.Empty, typeof(FirstViewModel), null, nameof(INavigatedAsyncAware.OnNavigatedAsync)));
        Logs[4].Should().Be((string.Empty, typeof(FirstViewModel), null, nameof(INavigatedAware.OnNavigated)));
        Logs[5].Should().Be((string.Empty, null, typeof(FirstViewModel), nameof(INavigationFrame.Navigated)));

        Logs.Clear();
        await service.NavigateAsync<SecondViewModel>(NamedFrame.Name);
        DefaultFrame.Current.GetType().Should().Be(typeof(SecondPage));
        DefaultFrame.Count.Should().Be(2);

        // Pausing
        Logs[0].Should().Be((string.Empty, typeof(FirstViewModel), null, nameof(IPausingAsyncAware.OnPausingAsync)));
        Logs[1].Should().Be((string.Empty, typeof(FirstViewModel), null, nameof(IPausingAware.OnPausing)));
        Logs[2].Should().Be((string.Empty, typeof(FirstViewModel), typeof(SecondViewModel), nameof(INavigationFrame.Pausing)));
        // Navigating
        Logs[3].Should().Be((string.Empty, typeof(SecondViewModel), null, nameof(INavigatingAsyncAware.OnNavigatingAsync)));
        Logs[4].Should().Be((string.Empty, typeof(SecondViewModel), null, nameof(INavigatingAware.OnNavigating)));
        Logs[5].Should().Be((string.Empty, typeof(FirstViewModel), typeof(SecondViewModel), nameof(INavigationFrame.Navigating)));
        // Navigated
        Logs[6].Should().Be((string.Empty, typeof(SecondViewModel), null, nameof(INavigatedAsyncAware.OnNavigatedAsync)));
        Logs[7].Should().Be((string.Empty, typeof(SecondViewModel), null, nameof(INavigatedAware.OnNavigated)));
        Logs[8].Should().Be((string.Empty, typeof(FirstViewModel), typeof(SecondViewModel), nameof(INavigationFrame.Navigated)));
        // Paused
        Logs[9].Should().Be((string.Empty, typeof(FirstViewModel), null, nameof(IPausedAsyncAware.OnPausedAsync)));
        Logs[10].Should().Be((string.Empty, typeof(FirstViewModel), null, nameof(IPausedAware.OnPaused)));
        Logs[11].Should().Be((string.Empty, typeof(FirstViewModel), typeof(SecondViewModel), nameof(INavigationFrame.Paused)));
    }

    public class FirstViewModel : NotificationViewModel
    {
        protected override void OnNotify(string memberName = "") =>
            Should_be_notified.AddLog(typeof(FirstViewModel), memberName);
    }
    public class FirstPage : UserControl { }

    public class SecondViewModel : NotificationViewModel
    {
        protected override void OnNotify(string memberName = "") =>
            Should_be_notified.AddLog(typeof(SecondViewModel), memberName);
    }
    public class SecondPage : UserControl { }
}