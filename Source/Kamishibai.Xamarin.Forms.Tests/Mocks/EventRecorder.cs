using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Kamishibai.Xamarin.Forms.Tests.Mocks
{
    public class EventRecorder : List<EventRecord>
    {
        public void Record(object sender, [CallerMemberName] string callerMemberName = null)
        {
            Add(new EventRecord{ Sender = sender, CallerMemberName = callerMemberName});
        }

        public void Record(object sender, object parameter, [CallerMemberName] string callerMemberName = null)
        {
            Add(new EventRecord { Sender = sender, Parameter = parameter, CallerMemberName = callerMemberName });
        }
    }
}
