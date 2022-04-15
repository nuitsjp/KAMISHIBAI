using System;
using System.Globalization;
using System.Windows.Data;

namespace KamishibaiApp.View.Converters
{
    public class EnumToBooleanConverter : IValueConverter
    {
        public Type? EnumType { get; set; }

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (EnumType is null) throw new InvalidOperationException("EnumType is null.");
            if (parameter is not string enumString) return false;
            if (!Enum.IsDefined(EnumType, value)) return false;

            var enumValue = Enum.Parse(EnumType, enumString);
            return enumValue.Equals(value);

        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (EnumType is null) throw new InvalidOperationException("EnumType is null.");
            if (parameter is string enumString)
            {
                return Enum.Parse(EnumType, enumString);
            }

            return null;
        }
    }
}
