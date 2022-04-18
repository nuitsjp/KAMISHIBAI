using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms
{
    public interface IBehaviorInjector
    {
        void Inject(Page page);
    }
}