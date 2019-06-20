using Infrastructure.Extensions;
using Interfaces.IServices;
using System;
using System.Collections.Generic;

namespace Services
{
    public class RelativePointsReaderService : ILayerReaderService
    {
        private readonly double[] _dependentPoints;
        private string _diffValues;

        public RelativePointsReaderService(double[] dependentPoints, string diffValues)
        {
            _dependentPoints = dependentPoints;
            _diffValues = diffValues;
        }

        public double[] ReadPoints()
        {
            if (_dependentPoints == null || _dependentPoints.Length == 0
                || string.IsNullOrEmpty(_diffValues))
            {
                throw new ArgumentException();
            }

            var diffValues = GetDoubleDiffValues();

            if (_dependentPoints.Length != diffValues.Length)
            {
                throw new ArgumentException();
            }

            var result = new double[_dependentPoints.Length];

            for(var i = 0; i < _dependentPoints.Length; i++)
            {
                result[i] = _dependentPoints[i] + diffValues[i];
            }

            return result;
        }

        private double[] GetDoubleDiffValues()
        {
            var ignoreChars = new Dictionary<string, string>();
            ignoreChars.Add("\r\n", " ");

            return _diffValues.ExtractDoubles();
        }
    }
}
