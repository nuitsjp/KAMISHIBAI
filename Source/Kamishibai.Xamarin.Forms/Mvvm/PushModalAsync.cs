using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms.Mvvm
{
    public class PushModalAsync<TPage> : AnimatableNavigation<TPage> where TPage : Page, new()
    {
        public static readonly BindableProperty WithNavigationPageProperty =
            BindableProperty.Create(nameof(WithNavigationPage), typeof(bool), typeof(PushModalAsync<TPage>), false);

        public bool WithNavigationPage
        {
            get => (bool)GetValue(WithNavigationPageProperty);
            set => SetValue(WithNavigationPageProperty, value);
        }
        
        public override Task Navigate<TParam>(TParam parameter = default(TParam))
        {
            if (WithNavigationPage)
            {
                return Navigator.PushModalAsync(new NavigationPage(new TPage()), parameter, Animated);
            }
            else
            {
                return Navigator.PushModalAsync(new TPage(), parameter, Animated);
            }
        }
    }
}
