using System;
using System.Globalization;
using System.Windows.Data;

namespace QLearning.Converters
{
    public class ReverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                if (boolValue)
                    return false;
                else
                    return true;
            }

            throw new ArgumentException("Value not expected");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}