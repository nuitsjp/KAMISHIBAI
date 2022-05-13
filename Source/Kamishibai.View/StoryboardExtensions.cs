using System.Windows.Media.Animation;

namespace Kamishibai;

internal static class StoryboardExtensions
{
    public static Task BeginAsync(this Storyboard storyboard)
    {
        TaskCompletionSource<bool> taskCompletionSource = new();
        void OnComplete(object? s, object e)
        {
            storyboard.Completed -= OnComplete;
            taskCompletionSource.SetResult(true);
        }

        storyboard.Completed += OnComplete;
        storyboard.Begin();
        return taskCompletionSource.Task;
    }
}