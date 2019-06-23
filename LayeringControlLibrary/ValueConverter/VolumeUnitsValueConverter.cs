using CommonServiceLocator;
using Interfaces.IServices;
using Models.Enums;
using System;
using System.Globalization;
using System.Windows.Data;
using Unity;

namespace LayeringControlLibrary.ValueConverter
{
    public class VolumeUnitsValueConverter : IMultiValueConverter
    {
        private readonly IUnitConverter _converter;

        public VolumeUnitsValueConverter()
        {
            _converter = ServiceLocator.Current.GetInstance<IUnitConverter>();
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(values[0].ToString(), out var val) && Enum.TryParse<VolumeUnits>(values[1].ToString(), out var unit))
            {
                return Math.Round(_converter.Convert(val, VolumeUnits.CubicMeter, unit), 0);
            }

            return "NaN";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
