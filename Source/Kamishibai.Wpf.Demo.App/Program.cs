using Kamishibai.Wpf.Demo.App;
using Kamishibai.Wpf.Demo.View;
using Kamishibai.Wpf.Demo.ViewModel;
using Kamishibai.Wpf.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = KamishibaiApplication<App, MainWindow>.CreateBuilder();
builder.Services.AddTransient<MainWindowViewModel>();
builder.Services.AddPresentation<ContentPage, ContentPageViewModel>();

var app = builder.Build();
app.RunAsync();

var typeArguments = string.Join(", ", Enumerable.Range(1, 2).Select(x => $"in T{x}"));