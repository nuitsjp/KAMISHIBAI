using Kamishibai;

namespace SampleBrowser.ViewModel;

[OpenWindow]
public class WindowWithPrimaryConstructorViewModel(string argument, [Inject] IPresentationService presentationService)
    : ChildViewModel(presentationService)
{
    private readonly string argument = argument;
}
