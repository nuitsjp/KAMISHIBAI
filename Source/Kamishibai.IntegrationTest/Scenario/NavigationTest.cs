﻿using NUnit.Framework;
using Driver.Windows;
using FluentAssertions;
using SampleBrowser.View.Page;

namespace Scenario;

[TestFixture]
public class NavigationTest : TestBase
{
    [Test]
    public void NavigateByType()
    {
        var mainWindow = app.AttachMainWindow();
        var contentPage = app.AttachContentPage();
        var navigationMenuPage = app.AttachNavigationMenuPage();

        // Navigate to ContentPage
        navigationMenuPage.NavigateByTypeCommand.EmulateClick();

        mainWindow.NavigationFrame.Should().BeOfPage<WithoutArgumentsPage>();
        contentPage.Message.Text.Should().Be("Default WindowName");

        // Go back
        contentPage.GoBackCommand.EmulateClick();
        mainWindow.NavigationFrame.Should().BeOfPage<NavigationMenuPage>();
    }

    [Test]
    public void NavigateByGenericType()
    {
        var mainWindow = app.AttachMainWindow();
        var contentPage = app.AttachContentPage();
        var navigationMenuPage = app.AttachNavigationMenuPage();

        // Navigate to ContentPage
        navigationMenuPage.NavigateByGenericTypeCommand.EmulateClick();

        mainWindow.NavigationFrame.Should().BeOfPage<WithoutArgumentsPage>();
        contentPage.Message.Text.Should().Be("Default WindowName");

        // Go back
        contentPage.GoBackCommand.EmulateClick();
        mainWindow.NavigationFrame.Should().BeOfPage<NavigationMenuPage>();
    }

    [Test]
    public void NavigateByViewModelInstance()
    {
        var mainWindow = app.AttachMainWindow();
        var contentPage = app.AttachContentPage();
        var navigationMenuPage = app.AttachNavigationMenuPage();

        // Navigate to ContentPage
        const string message = "Hello, Navigate!";
        navigationMenuPage.Message1.EmulateChangeText(message);
        navigationMenuPage.NavigateByInstanceCommand.EmulateClick();

        mainWindow.NavigationFrame.Should().BeOfPage<WithArgumentsPage>();
        contentPage.Message.Text.Should().Be(message);

        // Go back
        contentPage.GoBackCommand.EmulateClick();
        mainWindow.NavigationFrame.Should().BeOfPage<NavigationMenuPage>();
    }

    [Test]
    public void NavigateWithCallbackInitializer()
    {
        var mainWindow = app.AttachMainWindow();
        var contentPage = app.AttachContentPage();
        var navigationMenuPage = app.AttachNavigationMenuPage();

        // Navigate to ContentPage
        const string message = "Hello, Navigate!";
        navigationMenuPage.Message2.EmulateChangeText(message);
        navigationMenuPage.NavigateWithCallbackCommand.EmulateClick();

        mainWindow.NavigationFrame.Should().BeOfPage<WithoutArgumentsPage>();
        contentPage.Message.Text.Should().Be(message);

        // Go back
        contentPage.GoBackCommand.EmulateClick();
        mainWindow.NavigationFrame.Should().BeOfPage<NavigationMenuPage>();
    }

    [Test]
    public void NavigateWithSafeParameter()
    {
        var mainWindow = app.AttachMainWindow();
        var contentPage = app.AttachContentPage();
        var navigationMenuPage = app.AttachNavigationMenuPage();

        // Navigate to ContentPage
        const string message = "Hello, Navigate!";
        navigationMenuPage.Message3.EmulateChangeText(message);
        navigationMenuPage.NavigateWithSafeParameterCommand.EmulateClick();

        mainWindow.NavigationFrame.Should().BeOfPage<WithArgumentsPage>();
        contentPage.Message.Text.Should().Be(message);

        // Go back
        contentPage.GoBackCommand.EmulateClick();
        mainWindow.NavigationFrame.Should().BeOfPage<NavigationMenuPage>();
    }

}