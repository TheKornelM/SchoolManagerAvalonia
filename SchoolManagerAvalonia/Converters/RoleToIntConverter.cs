using System;
using SchoolManagerModel.Entities;
using Avalonia.Data.Converters;

namespace SchoolManagerAvalonia.Converters;

public class RoleToIntConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return (int)value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return (Role)value;
    }
}