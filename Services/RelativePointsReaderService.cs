using Infrastructure.Extensions;
using Interfaces.IServices;
using Models;
using System;
using System.Collections.Generic;

namespace Services
{
    public class RelativePointsReaderService : ILayerReaderService<RelativePointsReaderSettings>
    {
        public double[] ReadPoints(RelativePointsReaderSettings settings)
        {
            if (settings.DependentPoints == null || settings.DependentPoints.Length == 0
                || string.IsNullOrEmpty(settings.DiffValues))
            {
                throw new ArgumentException();
            }

            var diffValues = GetDoubleDiffValues(settings);

            if (settings.DependentPoints.Length != diffValues.Length)
            {
                throw new ArgumentException();
            }

            var result = new double[settings.DependentPoints.Length];

            for(var i = 0; i < settings.DependentPoints.Length; i++)
            {
                result[i] = settings.DependentPoints[i] + diffValues[i];
            }

            return result;
        }

        private double[] GetDoubleDiffValues(RelativePointsReaderSettings settings)
        {
            var ignoreChars = new Dictionary<string, string>();
            ignoreChars.Add("\r\n", " ");

            return settings.DiffValues.ExtractDoubles();
        }
    }
}
