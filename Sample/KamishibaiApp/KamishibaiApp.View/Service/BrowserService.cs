﻿using System.Diagnostics;
using KamishibaiApp.ViewModel.Service;

namespace KamishibaiApp.View.Service
{
    public class BrowserService : IBrowserService
    {
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
