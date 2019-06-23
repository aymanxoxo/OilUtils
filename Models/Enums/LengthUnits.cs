using Models.Attributes;

namespace Models.Enums
{
    public enum LengthUnits
    {
        [Display("Meter")]
        [UnitFactor(1)]
        Meter = 0,

        [Display("Foot")]
        [UnitFactor(0.3048)]
        Foot = 1
    }
}
