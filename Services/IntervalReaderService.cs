using Interfaces.IServices;
using Models;
using System;

namespace Services
{
    public class IntervalReaderService : ILayerReaderService<IntervalReaderSettings>
    {
        public double[] ReadPoints(IntervalReaderSettings settings)
        {
            if (settings.PointsCount <= 0)
            {
                throw new ArgumentException("Points count must be greater than zero");
            }

            var result = new double[settings.PointsCount];

            for(var i = 0; i < settings.PointsCount; i++)
            {
                result[i] = settings.StartPoint + (i * settings.Interval);
            }

            return result;
        }
    }
}
