using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms
{
    public interface INavigator
    {
        void InsertPageBefore(Page page, Page before);
        void InsertPageBefore<TParam>(Page page, Page before, TParam parameter = default(TParam));
        Task<Page> PopAsync(bool animated = true);
        Task<Page> PopModalAsync(bool animated = true);
        Task PopToRootAsync(bool animated = true);
        Task PushAsync(Page page, bool animated = true);
        Task PushAsync<TParam>(Page page, TParam parameter = default(TParam), bool animated = true);
        Task PushModalAsync(Page page, bool animated = true);
        Task PushModalAsync<TParam>(Page page, TParam parameter = default(TParam), bool animated = true);
        void RemovePage(Page page);
    }
}