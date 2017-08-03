namespace Kamishibai.Xamarin.Forms
{
    public interface IPageLifecycleAware<in TParam>: IPageInilializeAware<TParam>, IPageLoadedAware, IPageUnloadedAware, IPageClosedAware
    {
    }
    
    public interface IPageLifecycleAware: IPageInilializeAware<object>, IPageLoadedAware, IPageUnloadedAware, IPageClosedAware
    {
    }

}