using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms.Mvvm
{
    public class PushAsync<TPage> : AnimatableNavigation<TPage> where TPage : Page
    {
        public override Task Navigate<TParam>(TParam parameter = default(TParam))
        {
            return Navigator.PushAsync(ServiceLocator.GetInstance<TPage>(), parameter, Animated);
        }
    }
}
