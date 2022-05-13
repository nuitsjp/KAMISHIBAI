using System.Threading.Tasks;

namespace Kamishibai.Maui.Mvvm
{
    public interface INavigationAction
    {
        Task Navigate<TParam>(TParam parameter = default(TParam));
    }
}