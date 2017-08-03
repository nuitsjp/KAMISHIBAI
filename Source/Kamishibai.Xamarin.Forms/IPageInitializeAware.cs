namespace Kamishibai.Xamarin.Forms
{
    public interface IPageInilializeAware<in TParam>
    {
        void OnInitialize(TParam parameter);
    }

    public interface IPageInitializeAware : IPageInilializeAware<object>
    {
        
    }
}