using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms.Mvvm
{
    public class PopToRootAsync : AnimatableNavigation<Page>
    {
        public override Task Navigate<TParam>(TParam parameter = default(TParam))
        {
            return Navigator.PopToRootAsync(Animated);
        }
    }
}
