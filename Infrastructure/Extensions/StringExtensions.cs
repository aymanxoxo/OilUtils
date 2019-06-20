using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Extensions
{
    // Just for simplifying things, should be moved to string utilities service
    public static class StringExtensions
    {
        public static double[] ExtractDoubles(this string self, char separator = ',', Dictionary<string, string> replaceChars = null)
        {
            if (string.IsNullOrEmpty(self))
            {
                return new double[] { };
            }

            if (replaceChars?.Count > 0)
            {
                foreach(var key in replaceChars.Keys)
                {
                    self.Replace(key, replaceChars[key]);
                }
            }

            var strArr = self.Split(separator);

            if (!strArr.Any())
            {
                return new double[] { };
            }

            var result = strArr.Select(s =>
            {
                if (!double.TryParse(s?.Trim(), out var d))
                {
                    throw new ArgumentException("String Can not be converted to double");
                }

                return d;
            }).ToArray();

            return result;
        }
    }
}
