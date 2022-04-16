using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Toolkit.Mvvm.Input;

namespace Kamishibai.Wpf.Demo.ViewModel;

[Navigatable]
public class ContentPageViewModel :
    IPausingAsyncAware,
    IPausingAware,
    IPausedAsyncAware,
    IPausedAware,
    INavigatingAsyncAware,
    INavigatingAware,
    INavigatedAsyncAware,
    INavigatedAware,
    IResumingAsyncAware,
    IResumingAware,
    IResumedAsyncAware,
    IResumedAware,
    IDisposingAsyncAware,
    IDisposingAware,
    IDisposedAsyncAware,
    IDisposable
{
    private readonly IPresentationService _presentationService;

    public ContentPageViewModel(
        int count, 
        string frameName,
        // ReSharper disable once UnusedParameter.Local
        [Inject] IEmployeeRepository employeeRepository,
        [Inject] IPresentationService presentationService)
    {
        Count = count;
        FrameName = frameName;
        _presentationService = presentationService;
    }

    public int Count { get; }
    public string FrameName { get; }
    public bool CanNavigateAsync { get; set; } = true;
    public bool CanNavigate { get; set; } = true;
    public bool CanGoBackAsync { get; set; } = true;
    public bool CanGoBack { get; set; } = true;

    public AsyncRelayCommand NavigateNextCommand => new(OnNavigateNextAsync);
    public AsyncRelayCommand GoBackCommand => new(OnGoBackAsync);
    public AsyncRelayCommand OpenWindowCommand => new(OnOpenWindowAsync);
    public AsyncRelayCommand OpenWindowWithParameterCommand => new(OnOpenWindowWithParameterAsync);
    public AsyncRelayCommand OpenWindowWithCallbackCommand => new(OnOpenWindowWithCallbackAsync);
    public AsyncRelayCommand OpenDialogCommand => new(OnOpenDialogAsync);
    public AsyncRelayCommand OpenDialogWithParameterCommand => new(OnOpenDialogWithParameterAsync);
    public AsyncRelayCommand OpenDialogWithCallbackCommand => new(OnOpenDialogWithCallbackAsync);

    private Task OnNavigateNextAsync()
    {
        return _presentationService.NavigateToContentPageAsync(Count + 1, FrameName);
    }

    private Task OnGoBackAsync()
    {
        return _presentationService.GoBackAsync(FrameName);
    }

    private Task OnOpenWindowAsync()
    {
        return _presentationService.OpenWindowAsync(typeof(ChildWindowViewModel));
    }

    private Task OnOpenWindowWithParameterAsync()
    {
        return _presentationService.OpenWindowAsync(new ChildWindowViewModel(_presentationService){Message = "Hello, OpenWindow with parameter."});
    }

    private Task OnOpenWindowWithCallbackAsync()
    {
        return _presentationService.OpenWindowAsync<ChildWindowViewModel>(viewModel => viewModel.Message = "Hello, OpenWindow with callback.", null);
    }

    private Task OnOpenDialogAsync()
    {
        return _presentationService.OpenDialogAsync(typeof(ChildWindowViewModel));
    }

    private Task OnOpenDialogWithParameterAsync()
    {
        return _presentationService.OpenDialogAsync(new ChildWindowViewModel(_presentationService) { Message = "Hello, OpenWindow with parameter." });
    }

    private Task OnOpenDialogWithCallbackAsync()
    {
        return _presentationService.OpenDialogAsync(new ChildWindowViewModel(_presentationService) { Message = "Hello, OpenWindow with callback." });
    }

    public async Task<bool> OnPausingAsync()
    {
        await WriteLogAsync();
        return CanNavigateAsync;
    }

    public bool OnPausing()
    {
        WriteLog();
        return CanNavigate;
    }

    public Task OnPausedAsync() => WriteLogAsync();
    public void OnPaused() => WriteLog();
    public Task OnNavigatingAsync() => WriteLogAsync();
    public void OnNavigating() => WriteLog();
    public Task OnNavigatedAsync() => WriteLogAsync();
    public void OnNavigated() => WriteLog();
    public Task OnResumingAsync() => WriteLogAsync();
    public void OnResuming() => WriteLog();
    public Task OnResumedAsync() => WriteLogAsync();
    public void OnResumed() => WriteLog();
    public async Task<bool> OnDisposingAsync()
    {
        await WriteLogAsync();
        return CanGoBackAsync;
    }

    public bool OnDisposing()
    {
        WriteLog();
        return CanGoBack;
    }

    public Task OnDisposedAsync() => WriteLogAsync();
    public void Dispose() => WriteLog();

    private Task WriteLogAsync([CallerMemberName] string member = "")
    {
        Debug.WriteLine($"{nameof(ContentPageViewModel)}#{member} Count:{Count}");
        return Task.CompletedTask;
    }

    private void WriteLog([CallerMemberName] string member = "")
    {
        Debug.WriteLine($"{nameof(ContentPageViewModel)}#{member} Count:{Count}");
    }
}
