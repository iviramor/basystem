using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace bas.program.Infrastructure.Converters
{
    /// <summary>
    /// Класс конвертирует bool занчения в пол
    /// True - мужской, False - женский
    /// </summary>
    public class InverseSexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if ((bool)value)
            {
                return "Мужской";
            }

            return "Женский";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value;
        }
    }
}
