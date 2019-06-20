using Interfaces.IServices;

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
            throw new System.NotImplementedException();
        }
    }
}
