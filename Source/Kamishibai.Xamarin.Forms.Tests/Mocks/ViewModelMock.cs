namespace Kamishibai.Xamarin.Forms.Tests.Mocks
{
    public class ViewModelMock : IPageLifecycleAware, IApplicationLifecycleAware, IPageInitializeAware<string>
    {
        private readonly EventRecorder _eventRecorder = new EventRecorder();

        public ViewModelMock(EventRecorder eventRecorder)
        {
            _eventRecorder = eventRecorder;
        }

        public void OnInitialize()
        {
            _eventRecorder.Record(this);
        }

        public void OnInitialize(string param)
        {
            _eventRecorder.Record(this, parameter:param);
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
