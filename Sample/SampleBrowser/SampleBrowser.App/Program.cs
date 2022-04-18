using Kamishibai;
using SampleBrowser.View;
using SampleBrowser.View.Page;
using SampleBrowser.ViewModel;
using SampleBrowser.ViewModel.Page;

var builder = KamishibaiApplication<App, MainWindow>.CreateBuilder();
builder.Services.AddPresentation<MainWindow, MainViewModel>();
builder.Services.AddPresentation<NavigationMenuPage, NavigationMenuViewModel>();

builder.Services.AddTransient<IPresentationService, PresentationService>();

var app = builder.Build();
app.RunAsync();
