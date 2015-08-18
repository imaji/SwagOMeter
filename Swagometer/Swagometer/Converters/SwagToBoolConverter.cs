using System;
using System.Globalization;
using System.Windows.Data;

namespace Swagometer.Converters
{
    public class SwagToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var theString = (string)value;
            return theString == Properties.Resources.SwagEm;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
