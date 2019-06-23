using Infrastructure.Extensions;
using Interfaces.IServices;
using System;

namespace Services
{
    public class UnitConverterService : IUnitConverter
    {
        public double Convert(double value, Enum from, Enum to)
        {
            if (from.GetType() != to.GetType())
            {
                throw new ArgumentException();
            }

            var type = from.GetType();

            if ((int)Enum.Parse(type, from.ToString()) == 0)
            {
                return value / to.GetConvertFactor();
            }
            else if ((int)Enum.Parse(type, to.ToString()) == 0)
            {
                return value * from.GetConvertFactor();
            }
            else
            {
                var fromToBase = value * from.GetConvertFactor();

                return fromToBase / to.GetConvertFactor();
            }
        }
    }
}
