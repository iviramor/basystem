using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace bas.program.Infrastructure.Converters
{
    /// <summary>
    /// Класс конвертирует значения в противоположную
    /// True в False
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool) value;
        }
    }
}
