using System;
using System.Windows.Data;
using Swagometer.Lib.Interfaces;

namespace Swagometer.Converters
{
    public class SwagToEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
            {
                throw new InvalidOperationException("The target must be a bool");
            }
            return value is SwagBase;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
