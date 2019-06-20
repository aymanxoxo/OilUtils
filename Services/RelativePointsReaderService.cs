using Interfaces.IServices;

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
            throw new System.NotImplementedException();
        }
    }
}
