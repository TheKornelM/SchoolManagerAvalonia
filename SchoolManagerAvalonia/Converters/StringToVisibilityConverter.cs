using System;
using System.Globalization;
using System.Windows;
using Avalonia.Data.Converters;

namespace SchoolManagerAvalonia.Converters;

public class StringToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is string str && !string.IsNullOrWhiteSpace(str);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // ConvertBack is not implemented because it’s not needed for one-way binding
        throw new NotSupportedException("StringToVisibilityConverter does not support ConvertBack.");
    }
}
