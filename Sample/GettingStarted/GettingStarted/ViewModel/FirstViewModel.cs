using Kamishibai;

namespace GettingStarted.ViewModel;

[Navigate]
public class FirstViewModel
{
    public FirstViewModel(string message)
    {
        Message = message;
    }

    public string Message { get; }

}