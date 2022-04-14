namespace Kamishibai.Wpf.View;

// ReSharper disable once UnusedTypeParameter
public static class ViewTypeCache
{
    private static readonly Dictionary<Type, Type> ViewTypes = new();

    public static Type GetViewType<TViewType>() => GetViewType(typeof(TViewType));

    public static Type GetViewType(Type viewModelType)
    {
        if (ViewTypes.TryGetValue(viewModelType, out var viewType))
        {
            return viewType;
        }

        throw new InvalidOperationException($"View matching the {viewModelType} has not been registered.");
    }


    public static void SetViewType<TView, TViewType>()
    {
        ViewTypes[typeof(TViewType)] = typeof(TView);
    }
}