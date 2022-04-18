using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms.Mvvm
{
    public class PopModalAsync : AnimatableNavigation<Page>
    {
        public override Task Navigate<TParam>(TParam parameter = default(TParam))
        {
            return Navigator.PopModalAsync(Animated);
        }
    }
}
