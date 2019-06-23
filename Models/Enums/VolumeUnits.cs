using Models.Attributes;

namespace Models.Enums
{
    public enum VolumeUnits
    {
        [Display("Cubic Meter")]
        [UnitFactor(1)]
        CubicMeter = 0,

        [Display("Cubic Foot")]
        [UnitFactor(0.0283168)]
        CubicFoot = 1,

        // uk barrel
        [Display("UK Barrel")]
        [UnitFactor(0.163659)]
        Barrel = 2
    }
}
