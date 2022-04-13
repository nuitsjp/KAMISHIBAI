using System.Windows.Navigation;
using Kamishibai.Wpf.Extensions.Hosting;
using KamishibaiApp.View;
using KamishibaiApp.ViewModel;
using NavigationService = KamishibaiApp.ViewModel.NavigationService;

var builder = KamishibaiApplication<App, ShellWindow>.CreateBuilder();
builder.Services.AddTransient<ShellWindowViewModel>();
builder.Services.AddTransient<INavigationService, NavigationService>();


var app = builder.Build();
app.RunAsync();
