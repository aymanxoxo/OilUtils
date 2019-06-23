using Models.Attributes;
using System;
using System.Linq;

namespace Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum self)
        {
            var attribute = GetAttributes(self,typeof(DisplayAttribute)).OfType<DisplayAttribute>().FirstOrDefault();

            return attribute?.Name;
        }

        public static double GetConvertFactor(this Enum self)
        {
            var attribute = GetAttributes(self, typeof(UnitFactorAttribute)).OfType<UnitFactorAttribute>().FirstOrDefault();

            return attribute?.Value ?? 1;
        }

        private static object[] GetAttributes(Enum enumMem, Type attributeType)
        {
            var type = enumMem.GetType();
            var member = type.GetMember(enumMem.ToString()).FirstOrDefault(m => m.DeclaringType == type);
            return member.GetCustomAttributes(attributeType, false);
        }
    }
}
