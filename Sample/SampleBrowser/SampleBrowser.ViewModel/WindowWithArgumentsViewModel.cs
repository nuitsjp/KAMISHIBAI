using Kamishibai;

namespace SampleBrowser.ViewModel;

[OpenWindow]
public class WindowWithArgumentsViewModel : ChildViewModel
{
    public WindowWithArgumentsViewModel(
        string windowName,
        [Inject] IPresentationService presentationService) : base(presentationService)
    {
        WindowName = windowName;
    }

}