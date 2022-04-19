using Kamishibai;

namespace SampleBrowser.ViewModel;

[OpenWindow]
[OpenDialog]
public class ChildMessageViewModel : ChildViewModel
{
    public ChildMessageViewModel(
        string windowName, 
        [Inject] IPresentationService presentationService) : base(presentationService)
    {
        WindowName = windowName;
    }
}