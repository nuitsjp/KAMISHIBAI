using System.Threading.Tasks;

namespace Kamishibai.Maui.Mvvm
{
    public interface INavigationRequest
    {
        INavigationAction NavigationAction { set; }

        Task RaiseAsync();
    }

    public interface INavigationRequest<in TParam> : INavigationRequest
    {
        Task RaiseAsync(TParam param);
    }

}