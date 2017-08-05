using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kamishibai.Xamarin.Forms.Tests.Mocks;
using Xamarin.Forms;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests.Lifecycle
{
    public class CommonFixture
    {
        [Fact]
        public void NotNotifiedBindingContext()
        {
            var eventReporder = new EventRecorder();
            var contentPage = new ContentPageMock(eventReporder) { BindingContext = new object() };

            LifecycleNoticeService.OnInitialize(contentPage);

            Assert.Equal(1, eventReporder.Count);
            Assert.Equal(contentPage, eventReporder[0].Sender);
            Assert.Equal("OnInitialize", eventReporder[0].CallerMemberName);
            Assert.Null(eventReporder[0].Parameter);
        }
    }
}
