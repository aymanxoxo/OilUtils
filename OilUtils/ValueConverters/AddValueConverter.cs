using System;
using System.Globalization;
using System.Windows.Data;

namespace OilUtils.ValueConverters
{
    public class AddValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (double.TryParse(value.ToString(), out var val) && double.TryParse(parameter.ToString(), out var param))
            {
                return val + param;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
