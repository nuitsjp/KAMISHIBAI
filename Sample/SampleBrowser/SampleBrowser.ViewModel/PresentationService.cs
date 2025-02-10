using Kamishibai;

namespace SampleBrowser.ViewModel;

partial interface IPresentationService
{
    Task<IWindow> OpenSingletonWindowWithArgumentsWindowAsync(string windowName, object? owner = null, OpenWindowOptions? options = null);
}


partial class PresentationService
{
    private IWindow? singletonWindow;

    public async Task<IWindow> OpenSingletonWindowWithArgumentsWindowAsync(string windowName, object? owner = null, OpenWindowOptions? options = null)
    {
        if (this.singletonWindow is null || this.singletonWindow.IsClosed)
        {
            return this.singletonWindow = await OpenWindowWithArgumentsWindowAsync(windowName, owner, options);
        }
        this.singletonWindow.Activate();
        return this.singletonWindow;
    }
}
