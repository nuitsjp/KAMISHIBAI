namespace Kamishibai.Xamarin.Forms
{
    public interface IPageLifecycleAware<in TParam>: IPageInitializeAware<TParam>, IPageLoadedAware, IPageUnloadedAware, IPageClosedAware
    {
    }
    
    public interface IPageLifecycleAware: IPageInitializeAware, IPageLoadedAware, IPageUnloadedAware, IPageClosedAware
    {
    }

}