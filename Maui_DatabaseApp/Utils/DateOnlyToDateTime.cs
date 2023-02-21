using System.Globalization;

namespace Maui_DatabaseApp.Utils;

public class DateOnlyToDateTime : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        DateOnly v = (DateOnly)value;
        return new DateTime(v.Year, v.Month, v.Day);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        DateTime v= (DateTime)value;
        return DateOnly.FromDateTime(v);
    }
}
