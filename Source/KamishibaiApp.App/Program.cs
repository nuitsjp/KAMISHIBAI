using System.Windows.Navigation;
using Kamishibai.Wpf.Extensions.Hosting;
using KamishibaiApp.View;
using KamishibaiApp.ViewModel;

var builder = KamishibaiApplication<App, ShellWindow>.CreateBuilder();
builder.Services.AddTransient<ShellWindowViewModel>();


var app = builder.Build();
app.RunAsync();
