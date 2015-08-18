using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Swagometer.Converters
{
    public class SwagToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var theString = (string)value;
            return theString == Properties.Resources.SwagEm ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
