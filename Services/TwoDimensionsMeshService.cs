using Interfaces.IServices;

namespace Services
{
    public class TwoDimensionsMeshService : ITwoDimensionsMeshService
    {
        private readonly double[] _xs;
        private readonly double[] _ys;
        private double _depth;

        public TwoDimensionsMeshService(double[] xs, double[] ys, double depth = 0)
        {
            _xs = xs;
            _ys = ys;
            _depth = depth;
        }

        public string CalculatePositions()
        {
            throw new System.NotImplementedException();
        }

        public int[] CalculateTriangleIndices()
        {
            throw new System.NotImplementedException();
        }

        public bool CanDraw()
        {
            throw new System.NotImplementedException();
        }
    }
}
