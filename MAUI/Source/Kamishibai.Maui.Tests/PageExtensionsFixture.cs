using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform;
using Xunit;

namespace Kamishibai.Maui.Tests
{
	public class PageExtensionsFixture
	{
		[Fact]
		public void GetParentPage()
		{
			var contentPage = new ContentPage();
			var navigationPage = new NavigationPage(contentPage);
			
			Assert.Equal(navigationPage, contentPage.GetParentPage());
		}

		[Fact]
		public void GetParentPage_WhenParentIsNull()
		{
			var contentPage = new ContentPage();

			Assert.Null(contentPage.GetParentPage());
		}

	    [Fact]
	    public void GetParentPage_WhenPageIsNull()
	    {
	        Assert.Null(PageExtensions.GetParentPage(null));
	    }

        [Fact]
		public void GetParentPage_WithTypeParameter()
		{
			var contentPage = new ContentPage();
			var navigationPage = new NavigationPage(contentPage);

			Assert.Equal(navigationPage, contentPage.GetParentPage<NavigationPage>());

		}

		[Fact]
		public void GetParentPage_WithTypeParameter_WhenIncludeCurrent()
		{
			var contentPage = new ContentPage();

			Assert.Equal(contentPage, contentPage.GetParentPage<ContentPage>(true));

		    var navigationPage = new NavigationPage(contentPage);

		    Assert.Equal(navigationPage, contentPage.GetParentPage<NavigationPage>(true));
        }
    }
}
