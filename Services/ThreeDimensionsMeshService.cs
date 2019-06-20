using Interfaces.IServices;

namespace Services
{
    public class ThreeDimensionsMeshService : IThreeDimensionsMeshService
    {
        private readonly double[] _xs;
        private readonly double[] _ys;
        private readonly double[] _zs;

        public ThreeDimensionsMeshService(double[] xs, double[] ys, double[] zs)
        {
            _xs = xs;
            _ys = ys;
            _zs = zs;
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

        public double Volume()
        {
            throw new System.NotImplementedException();
        }
    }
}
