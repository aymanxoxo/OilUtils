using System;

namespace Models.Attributes
{
    public class UnitFactorAttribute : Attribute
    {
        public double Value { get; set; }

        public UnitFactorAttribute(double value)
        {
            Value = value;
        }
    }
}
