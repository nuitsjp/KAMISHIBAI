using Codeer.Friendly.Windows;
using System.Diagnostics;
using System.IO;

namespace Driver.TestController
{
    public static class ProcessController
    {
        public static WindowsAppFriend Start()
        {
            //target path
            var targetPath = @"..\..\..\..\..\..\Sample\SampleBrowser\SampleBrowser.Hosting\bin\Debug\net6.0-windows\SampleBrowser.Hosting.exe";
            var info = new ProcessStartInfo(targetPath) { WorkingDirectory = Path.GetDirectoryName(targetPath)! };
            var app = new WindowsAppFriend(Process.Start(info));
            app.ResetTimeout();
            return app;
        }

        public static void Kill(this WindowsAppFriend app)
        {
            if (app == null) return;

            app.ClearTimeout();
            try
            {
                Process.GetProcessById(app.ProcessId).Kill();
            }
            catch
            {
                // ignored
            }
        }
    }
}
