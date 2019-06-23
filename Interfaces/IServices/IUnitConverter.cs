using System;

namespace Interfaces.IServices
{
    public interface IUnitConverter
    {
        double Convert(double value, Enum from, Enum to);
    }
}
