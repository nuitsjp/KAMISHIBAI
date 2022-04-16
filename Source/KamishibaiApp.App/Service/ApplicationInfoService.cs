using System;
using System.Diagnostics;
using System.Reflection;
using Windows.ApplicationModel;
using KamishibaiApp.ViewModel.Service;
using OSVersionHelper;

namespace KamishibaiApp.App.Service
{
    public class ApplicationInfoService : IApplicationInfoService
    {
        public Version GetVersion()
        {
            if (WindowsVersionHelper.HasPackageIdentity)
            {
                // Packaged application
                // Set the app version in KamishibaiApp.Packaging > Package.appxmanifest > Packaging > PackageVersion
                var packageVersion = Package.Current.Id.Version;
                return new Version(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
            }

            // Set the app version in KamishibaiApp > Properties > Package > PackageVersion
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var version = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion!;
            return new Version(version);
        }
    }
}
