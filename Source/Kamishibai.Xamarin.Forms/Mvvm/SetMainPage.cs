using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms.Mvvm
{
    public class SetMainPage<TPage> : NavigationBehavior<TPage> where TPage : Page
    {
        public override Task Navigate<TParam>(TParam parameter = default(TParam))
        {
            ApplicationService.SetMainPage(ServiceLocator.GetInstance<TPage>(), parameter);
            return Task.FromResult(true);
        }
    }
}
