using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using Avalonia.Data.Converters;

namespace SchoolManagerAvalonia.Converters;


public class CollectionCountToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ICollection collection) // Check for any collection type
        {
            return collection.Count > 0;
        }

        return false; // Safeguard for null or non-collection types
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
