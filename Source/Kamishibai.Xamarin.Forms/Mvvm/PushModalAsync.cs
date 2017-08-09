using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms.Mvvm
{
    public class PushModalAsync<TPage> : AnimatableNavigation<TPage> where TPage : Page
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
                return Navigator.PushModalAsync(new NavigationPage(ServiceLocator.GetInstance<TPage>()), parameter, Animated);
            }
            else
            {
                return Navigator.PushModalAsync(ServiceLocator.GetInstance<TPage>(), parameter, Animated);
            }
        }
    }
}
