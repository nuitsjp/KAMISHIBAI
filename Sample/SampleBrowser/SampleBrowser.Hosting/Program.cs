using System.Reflection;
using System.Windows.Automation;
using Kamishibai;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleBrowser.View;
using SampleBrowser.View.Page;
using SampleBrowser.ViewModel;
using SampleBrowser.ViewModel.Page;

var builder = KamishibaiApplication<App, MainWindow>.CreateBuilder();

builder.Services.AddPresentation<MainWindow, MainViewModel>();
builder.Services.AddPresentation<ChildWindow, WindowWithArgumentsViewModel>();
builder.Services.AddPresentation<ChildWindow, WindowWithoutArgumentsViewModel>();
builder.Services.AddPresentation<ChildWindow, DialogWithArgumentsViewModel>();
builder.Services.AddPresentation<ChildWindow, DialogWithoutArgumentsViewModel>();

builder.Services.AddPresentation<NavigationMenuPage, NavigationMenuViewModel>();
builder.Services.AddPresentation<WithoutArgumentsPage, WithoutArgumentsViewModel>();
builder.Services.AddPresentation<WithArgumentsPage, WithArgumentsViewModel>();

builder.Services.AddPresentation<OpenWindowPage, OpenWindowViewModel>();
builder.Services.AddPresentation<OpenDialogPage, OpenDialogViewModel>();
builder.Services.AddPresentation<ShowMessagePage, ShowMessageViewModel>();
builder.Services.AddPresentation<OpenFilePage, OpenFileViewModel>();
builder.Services.AddPresentation<SaveFilePage, SaveFileViewModel>();

var app = builder.Build();
await app.RunAsync();
