namespace Kamishibai;

// ReSharper disable once UnusedTypeParameter
public static class ViewTypeCache
{
    private static readonly Dictionary<Type, Type> ViewTypes = new();

    public static Type GetViewType(Type viewModelType)
    {
        if (ViewTypes.TryGetValue(viewModelType, out var viewType))
        {
            return viewType;
        }

        throw new InvalidOperationException($"View matching the {viewModelType} has not been registered.");
    }

    public static bool TryGetViewModelType(Type viewType, out Type viewModelType)
    {
        foreach (var pair in ViewTypes)
        {
            if (pair.Value == viewType)
            {
                viewModelType = pair.Key;
                return true;
            }
        }

        viewModelType = typeof(Type);
        return false;
    }


    public static void SetViewType<TView, TViewModel>()
    {
        ViewTypes[typeof(TViewModel)] = typeof(TView);
    }
}