namespace Kamishibai.Xamarin.Forms
{
    public interface IPageInitializeAware<in TParam>
    {
        void OnInitialize(TParam parameter);
    }

    public interface IPageInitializeAware
    {
        void OnInitialize();
    }
}