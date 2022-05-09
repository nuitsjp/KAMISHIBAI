using System;
using System.Collections.Generic;
using System.Text;

namespace Kamishibai.Maui.Mvvm
{
    public static class ServiceLocator
    {
        public static readonly Func<Type, object> DefaultLocator = type => Activator.CreateInstance(type);

        private static Func<Type, object> _locator = DefaultLocator;

        public static T GetInstance<T>()
        {
            return (T) _locator(typeof(T));
        }

        public static void SetLocator(Func<Type, object> resolver)
        {
            _locator = resolver;
        }
    }
}
