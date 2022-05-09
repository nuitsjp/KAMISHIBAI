using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Kamishibai.Maui.Mvvm
{
    public class PopToRootAsync : AnimatableNavigation<Page>
    {
        public override Task Navigate<TParam>(TParam parameter = default(TParam))
        {
            return Navigator.PopToRootAsync(Animated);
        }
    }
}
