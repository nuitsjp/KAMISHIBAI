using Xamarin.Forms;

namespace Kamishibai.Xamarin.Forms.Tests.Mocks
{
    public class TabbedPageMock : TabbedPage, IPageLifecycleAware<string>, IApplicationLifecycleAware
    {
        private readonly EventRecorder _eventRecorder;

        public TabbedPageMock(EventRecorder eventRecorder)
        {
            _eventRecorder = eventRecorder;
        }

        public void OnInitialize(string param)
        {
            _eventRecorder.Record(this, parameter: param);
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
