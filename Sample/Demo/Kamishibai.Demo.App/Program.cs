using Kamishibai.Wpf.Demo;
using Kamishibai.Wpf.Demo.App;
using Kamishibai.Wpf.Demo.Repository;
using Kamishibai.Wpf.Demo.View;
using Kamishibai.Wpf.Demo.ViewModel;
using Kamishibai.Wpf.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = KamishibaiApplication<App, MainWindow>.CreateBuilder();
builder.Services.AddTransient<MainWindowViewModel>();

builder.Services.AddPresentation<ContentPage, ContentPageViewModel>();
builder.Services.AddPresentation<ChildWindow, ChildWindowViewModel>();

builder.Services.AddTransient<IPresentationService, PresentationService>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

var app = builder.Build();
app.RunAsync();
