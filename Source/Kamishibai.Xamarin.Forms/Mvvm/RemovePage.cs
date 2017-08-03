using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms.Mvvm
{
    public class RemovePage<TPage> : NavigationBehavior<TPage> where TPage : Page, new()
    {
        public override Task Navigate<TParam>(TParam parameter = default(TParam))
        {
            var target = CurrentPage.Navigation.NavigationStack.FirstOrDefault(x => x.GetType() == typeof(TPage));
            if (target != null)
            {
                Navigator.RemovePage(target);
            }
            return Task.FromResult(true);
        }
    }
}
