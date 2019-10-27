using System;

namespace Kamishibai.Wpf
{
    public static class ViewProvider
    {
        private static Func<Type, object> _viewProvider = Resolve;

        public static void SetViewProvider(Func<Type, object> provider) => _viewProvider = provider;

        public static object Resolve<TViewModel>() => _viewProvider(typeof(TViewModel));

        private static object Resolve(Type viewModelType)
        {
            var viewModelName = viewModelType.FullName;
            var viewName = viewModelName.Substring(0, viewModelName.Length - "ViewModel".Length);

            var viewType = viewModelType.Assembly.GetType(viewName);
            if (viewType == null) throw new ArgumentException($"No matching type found for {viewModelType}");

            return Activator.CreateInstance(viewType);
        }

    }
}