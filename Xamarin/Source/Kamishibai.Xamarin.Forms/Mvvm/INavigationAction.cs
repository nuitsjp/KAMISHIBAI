using System.Threading.Tasks;

namespace Kamishibai.Xamarin.Forms.Mvvm
{
    public interface INavigationAction
    {
        Task Navigate<TParam>(TParam parameter = default(TParam));
    }
}