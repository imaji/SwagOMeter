using System;
using System.Windows.Data;

namespace Swagometer.Converters
{
    internal class AttendeeToEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
            {
                throw new InvalidOperationException("The target must be a bool");
            }

            var isEnabled = false;

            if (value != null &&
                value is IAttendee)
            {
                isEnabled = true;
            }

            return isEnabled;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
