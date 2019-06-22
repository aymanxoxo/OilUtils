using Interfaces.IFactories;
using Interfaces.IServices;
using Models;

namespace Services.Factories
{
    public class RelativePointsReaderServiceFactory : IReaderFactory<RelativePointsReaderSettings>
    {
        public ILayerReaderService GetReaderService(RelativePointsReaderSettings settings)
        {
            return new RelativePointsReaderService(settings.DependentPoints, settings.DiffValues);   
        }
    }
}
