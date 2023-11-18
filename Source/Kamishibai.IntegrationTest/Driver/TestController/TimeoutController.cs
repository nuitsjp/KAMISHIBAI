using Codeer.Friendly.DotNetExecutor;
using Codeer.Friendly.Windows;
using NUnit.Framework;
using System.Diagnostics;

namespace Driver.TestController
{
    internal static class TimeoutController
    {
        static readonly TypeFinder Finder = new();
        static Thread _killer;
        static bool _alive;

        internal static void ResetTimeout(this WindowsAppFriend app)
        {
            app.ClearTimeout();
            if (Debugger.IsAttached) return;

            var type = Finder.GetType(TestContext.CurrentContext.Test.ClassName);
            var method = type.GetMethod(TestContext.CurrentContext.Test.MethodName!)!;
            var attrs = method.GetCustomAttributes(typeof(TimeoutExAttribute), true);
            if (attrs.Length != 1) return;

            var timeout = (TimeoutExAttribute)attrs[0];
            _alive = true;
            _killer = new Thread(() => PollingTimeout(app, timeout));
            _killer.Start();
        }

        static void PollingTimeout(WindowsAppFriend app, TimeoutExAttribute timeout)
        {
            var sw = new Stopwatch();
            sw.Start();
            while (_alive)
            {
                if (timeout.Time < sw.ElapsedMilliseconds)
                {
                    try
                    {
                        Process.GetProcessById(app.ProcessId).Kill();
                    }
                    catch
                    {
                        // ignored
                    }

                    _alive = false;
                    return;
                }
                Thread.Sleep(10);
            }
        }

        internal static void ClearTimeout(this WindowsAppFriend app)
        {
            _alive = false;
            if (_killer != null)
            {
                try
                {
                    _killer.Join();
                }
                catch
                {
                    // ignored
                }
            }
            _killer = null;
        }
    }
}
