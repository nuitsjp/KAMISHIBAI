using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Kamishibai.Maui.Mvvm
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
