using System;
using System.Collections.Generic;

namespace Kamishibai.Wpf
{
    public static class ViewLocator
    {
        private static readonly Dictionary<Type, Type> _viewDictionary = new Dictionary<Type, Type>();

        private static Func<Type, Type> _viewProvider = Resolve;

        public static void SetViewProvider(Func<Type, Type> provider) => _viewProvider = provider;

        public static void Register<TViewMode, TView>() => _viewDictionary[typeof(TViewMode)] = typeof(TView);

        public static object GetInstance<TViewModel>()
        {
            if (!_viewDictionary.TryGetValue(typeof(TViewModel), out var viewType))
            {
                viewType = _viewProvider(typeof(TViewModel));

                if (viewType == null) throw new ArgumentException($"Cannot find View that matches {typeof(TViewModel)}.");
            }

            return Activator.CreateInstance(viewType);
        }

        private static Type Resolve(Type viewModelType)
        {
            var viewModelName = viewModelType.FullName;
            // ReSharper disable once PossibleNullReferenceException
            var viewName = viewModelName.Substring(0, viewModelName.Length - "ViewModel".Length);

            var viewType = viewModelType.Assembly.GetType(viewName);
            if (viewType == null) throw new ArgumentException($"No matching type found for {viewModelType}");
            return viewType;
        }

    }
}