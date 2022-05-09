using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Kamishibai.Maui.Mvvm
{
    public class PopAsync : AnimatableNavigation<Page>
    {
        public override Task Navigate<TParam>(TParam parameter = default(TParam))
        {
            return Navigator.PopAsync(Animated);
        }
    }
}
