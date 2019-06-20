using Interfaces.IServices;
using System.Collections.Generic;

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
            var result = string.Empty;

            for (var y = 0; y < _ys.Length; y++)
            {
                for (var x = 0; x < _xs.Length; x++)
                {
                    result += $" {_xs[x]},{_ys[y]},{_depth}";
                }
            }

            return result;
        }

        public int[] CalculateTriangleIndices()
        {
            var result = new List<int>();

            for (var y = 0; y < _ys.Length - 1; y++)
            {
                var start = y * _xs.Length;
                var end = start + _xs.Length;

                for (var i = start; i < end - 1; i++)
                {

                    var v1 = i;
                    var v2 = i + 1;
                    var v3 = _xs.Length + i;
                    var v4 = _xs.Length + i + 1;

                    result.Add(v1);
                    result.Add(v2);
                    result.Add(v4);
                    result.Add(v4);
                    result.Add(v3);
                    result.Add(v1);
                }
            }

            return result.ToArray();
        }

        public bool CanDraw()
        {
            return _xs?.Length > 0 && _ys?.Length > 0 && (_xs.Length + _ys.Length) > 2;
        }
    }
}
