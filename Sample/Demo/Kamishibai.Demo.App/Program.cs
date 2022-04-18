using Kamishibai;
using Kamishibai.Demo;
using Kamishibai.Demo.App;
using Kamishibai.Demo.Repository;
using Kamishibai.Demo.View;
using Kamishibai.Demo.ViewModel;
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
