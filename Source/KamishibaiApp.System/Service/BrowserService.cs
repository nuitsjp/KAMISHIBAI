using System.Diagnostics;
using KamishibaiApp.View;

namespace KamishibaiApp.System.Service
{
    public class BrowserService : IBrowserService
    {
        public BrowserService()
        {
        }

        public void OpenInWebBrowser(string url)
        {
            // For more info see https://github.com/dotnet/corefx/issues/10361
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
