using Interfaces.IServices;
using Interfaces.IFactories;
using Models;

namespace Services.Factories
{
    public class IntervalReaderServiceFactory : IReaderFactory<IntervalReaderSettings>
    {
        public ILayerReaderService GetReaderService(IntervalReaderSettings settings)
        {
            return new IntervalReaderService(settings.Interval, settings.PointsCount, settings.StartPoint);
        }
    }
}
