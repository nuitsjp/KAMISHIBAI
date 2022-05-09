using Microsoft.Maui.Controls;

namespace Kamishibai.Maui
{
    public interface ILifecycleNotifier<in TParam>
    {
        void Notify(Page page, TParam parameter);
    }
}