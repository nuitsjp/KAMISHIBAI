namespace Kamishibai.Xamarin.Forms.Tests.Mocks
{
    public class ViewModelMock : IPageLifecycleAware, IApplicationLifecycleAware
    {
        private readonly EventRecorder _eventRecorder = new EventRecorder();

        public ViewModelMock(EventRecorder eventRecorder)
        {
            _eventRecorder = eventRecorder;
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
