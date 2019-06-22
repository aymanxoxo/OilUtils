using Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class TwoDimensionsMeshService : ITwoDimensionsMeshService
    {
        private string _positions;

        private List<int> _trianglesIndices;

        public double[] Xs { get; set; }

        public double[] Ys { get; set; }

        public double[] Depths { get; set; }

        public TwoDimensionsMeshService()
        {
            Reset();
        }

        public string CalculatePositions()
        {
            if (!CanDraw())
            {
                throw new ArgumentException();
            }

            if (!string.IsNullOrEmpty(_positions))
            {
                return _positions;
            }

            _positions = string.Empty;

            if (Depths == null || !Depths.Any())
            {
                var tempDepts = new List<double>();
                for(var i = 0; i < Xs.Length * Ys.Length; i++)
                {
                    tempDepts.Add(0);
                }

                Depths = tempDepts.ToArray();
            }

            for (var y = 0; y < Ys.Length; y++)
            {
                for (var x = 0; x < Xs.Length; x++)
                {
                    var flatIndex = (y * Xs.Length) + x;

                    _positions += $" {Xs[x]},{Ys[y]},{Depths[flatIndex]}";
                }
            }

            return _positions.Trim();
        }

        public int[] CalculateTriangleIndices()
        {
            if (!CanDraw())
            {
                throw new ArgumentException();
            }

            if (_trianglesIndices?.Count > 0)
            {
                return _trianglesIndices.ToArray();
            }

            for (var y = 0; y < Ys.Length - 1; y++)
            {
                var start = y * Xs.Length;
                var end = start + Xs.Length;

                for (var i = start; i < end - 1; i++)
                {

                    var v1 = i;
                    var v2 = i + 1;
                    var v3 = Xs.Length + i;
                    var v4 = Xs.Length + i + 1;

                    _trianglesIndices.Add(v1);
                    _trianglesIndices.Add(v2);
                    _trianglesIndices.Add(v4);
                    _trianglesIndices.Add(v4);
                    _trianglesIndices.Add(v3);
                    _trianglesIndices.Add(v1);
                }
            }

            return _trianglesIndices.ToArray();
        }

        public bool CanDraw()
        {
            return Xs?.Length > 0 && Ys?.Length > 0 && (Xs.Length + Ys.Length) > 2;
        }

        public void Reset()
        {
            _positions = string.Empty;
            _trianglesIndices = new List<int>();
        }
    }
}
