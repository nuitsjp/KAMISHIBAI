using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms.Mvvm
{
    public class PushAsync<TPage> : AnimatableNavigation<TPage> where TPage : Page, new()
    {
        public override Task Navigate<TParam>(TParam parameter = default(TParam))
        {
            return Navigator.PushAsync(new TPage(), parameter, Animated);
        }
    }
}
