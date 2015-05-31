﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Swagometer.Converters
{
    public class InvertedBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (bool)value;
            return visibility ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility)value;
            return (visibility != Visibility.Visible);
        }
    }
}
