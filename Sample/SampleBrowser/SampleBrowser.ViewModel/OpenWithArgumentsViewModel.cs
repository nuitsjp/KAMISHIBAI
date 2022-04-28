using Kamishibai;

namespace SampleBrowser.ViewModel;

[OpenWindow]
public class OpenWithArgumentsViewModel : ChildViewModel
{
    public OpenWithArgumentsViewModel(
        string windowName,
        [Inject] IPresentationService presentationService) : base(presentationService)
    {
        WindowName = windowName;
    }

}