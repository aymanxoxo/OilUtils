using Interfaces.IServices;
using System;

namespace Services
{
    public class IntervalReaderService : ILayerReaderService
    {
        private readonly double _interval;
        private readonly int _pointsCount;
        private readonly double _startPoint;

        public IntervalReaderService(double interval, int pointsCount, double startPoint = 0)
        {
            _interval = interval;
            _pointsCount = pointsCount;
            _startPoint = startPoint;
        }

        public double[] ReadPoints()
        {
            if (_pointsCount <= 0)
            {
                throw new ArgumentException("Points count must be greater than zero");
            }

            var result = new double[_pointsCount];

            for(var i = 0; i < _pointsCount; i++)
            {
                result[i] = _startPoint + (i * _interval);
            }

            return result;
        }
    }
}
