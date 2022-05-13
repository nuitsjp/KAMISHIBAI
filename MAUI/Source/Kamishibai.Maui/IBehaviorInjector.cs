using Microsoft.Maui.Controls;

namespace Kamishibai.Maui
{
    public interface IBehaviorInjector
    {
        void Inject(Page page);
    }
}