namespace Kamishibai.Wpf;

public interface INavigationHandler
{
    public void OnPausing(object? sourceViewModel, object destinationViewModel);
    public void OnNavigating(object? sourceViewModel, object destinationViewModel);
    public void OnNavigated(object? sourceViewModel, object destinationViewModel);
    public void OnPaused(object? sourceViewModel, object destinationViewModel);
    public void OnDisposing(object sourceViewModel, object destinationViewModel);
    public void OnResuming(object sourceViewModel, object destinationViewModel);
    public void OnResumed(object sourceViewModel, object destinationViewModel);
    public void OnDisposed(object sourceViewModel, object destinationViewModel);
}