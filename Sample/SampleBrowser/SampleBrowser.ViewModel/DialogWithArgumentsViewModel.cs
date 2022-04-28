using Kamishibai;

namespace SampleBrowser.ViewModel;

[OpenDialog]
public class DialogWithArgumentsViewModel : ChildViewModel
{
    public DialogWithArgumentsViewModel(
        string windowName,
        [Inject] IPresentationService presentationService) : base(presentationService)
    {
        WindowName = windowName;
    }

}