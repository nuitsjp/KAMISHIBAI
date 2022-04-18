using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms
{
    public interface ILifecycleNotifier<in TParam>
    {
        void Notify(Page page, TParam parameter);
    }
}