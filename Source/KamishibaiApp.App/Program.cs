using Kamishibai.Wpf.Extensions.Hosting;
using KamishibaiApp;
using KamishibaiApp.Repository;
using KamishibaiApp.View;
using KamishibaiApp.ViewModel;
using MvvmApp.Views;

var builder = KamishibaiApplication<App, ShellWindow>.CreateBuilder();
builder.Services.AddTransient<ShellWindowViewModel>();
builder.Services.AddTransient<INavigationService, NavigationService>();

builder.Services.AddPresentation<MainPage, MainViewModel>();
builder.Services.AddPresentation<ListDetailsPage, ListDetailsViewModel>();
builder.Services.AddPresentation<ContentGridPage, ContentGridViewModel>();
builder.Services.AddPresentation<ContentGridDetailPage, ContentGridDetailViewModel>();

builder.Services.AddTransient<ISampleDataRepository, SampleDataRepository>();



var app = builder.Build();
app.RunAsync();
