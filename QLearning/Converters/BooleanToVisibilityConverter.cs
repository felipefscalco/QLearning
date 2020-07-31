using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace QLearning.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                if (boolValue)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }

            throw new ArgumentException("Value not expected");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}