using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms.Tests.Mocks
{
    public class ContentPageMock : ContentPage, IPageLifecycleAware, IApplicationLifecycleAware, IPageInitializeAware<object>
    {
        public readonly EventRecorder _eventRecorder;

        public ContentPageMock(EventRecorder eventRecorder)
        {
            _eventRecorder = eventRecorder;
        }

        public void OnInitialize()
        {
            _eventRecorder.Record(this);
        }
        public void OnInitialize(object parameter)
        {
            _eventRecorder.Record(this, parameter);
        }
        public void OnLoaded()
        {
            _eventRecorder.Record(this);
        }

        public void OnUnloaded()
        {
            _eventRecorder.Record(this);
        }

        public void OnClosed()
        {
            _eventRecorder.Record(this);
        }

        public void OnResume()
        {
            _eventRecorder.Record(this);
        }

        public void OnSleep()
        {
            _eventRecorder.Record(this);
        }
    }
}
