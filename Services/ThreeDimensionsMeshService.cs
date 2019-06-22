using Infrastructure.Extensions;
using Interfaces.IServices;
using System;
using System.Collections.Generic;

namespace Services
{
    public class ThreeDimensionsMeshService : IThreeDimensionsMeshService
    {
        public double[] Xs { get; set; }
        public double[] Ys { get; set; }
        public double[] Z1s { get; set; }
        public double[] Z2s { get; set; }

        private string _positions;

        private List<int> _trianglesIndices;

        private double? _volume;

        public ThreeDimensionsMeshService()
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
                return _positions.Trim();
            }

            _positions = string.Empty;

            for (var y = 0; y < Ys.Length; y++)
            {
                for (var x = 0; x < Xs.Length; x++)
                {
                    var flatIndex = (y * Xs.Length) + x;

                    _positions += $" {Xs[x]},{Ys[y]},{Z1s[flatIndex]}";
                    _positions += $" {Xs[x]},{Ys[y]},{Z2s[flatIndex]}";
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

            if (_trianglesIndices.Count > 0)
            {
                return _trianglesIndices.ToArray();
            }

            var m = Ys.Length;
            var n = Xs.Length;

            for (var y = 0; y < m - 1; y++)
            {
                var start = y * n * 2;
                var end = start + (n * 2);
                for (var x = start; x < end - 2; x += 2)
                {
                    var u1 = x;
                    var l1 = u1 + 1;
                    var u2 = x + (n * 2);
                    var l2 = u2 + 1;
                    var u3 = x + (n * 2) + 2;
                    var l3 = u3 + 1;
                    var u4 = x + 2;
                    var l4 = u4 + 1;

                    // u1 u4 u3     u3 u2 u1
                    _trianglesIndices.Add(u1);
                    _trianglesIndices.Add(u4);
                    _trianglesIndices.Add(u3);
                    _trianglesIndices.Add(u3);
                    _trianglesIndices.Add(u2);
                    _trianglesIndices.Add(u1);

                    // l3 l4 l1     l1 l2 l3
                    _trianglesIndices.Add(l3);
                    _trianglesIndices.Add(l4);
                    _trianglesIndices.Add(l1);
                    _trianglesIndices.Add(l1);
                    _trianglesIndices.Add(l2);
                    _trianglesIndices.Add(l3);

                    // u1 l1 l4     l4 u4 u1
                    _trianglesIndices.Add(u1);
                    _trianglesIndices.Add(l1);
                    _trianglesIndices.Add(l4);
                    _trianglesIndices.Add(l4);
                    _trianglesIndices.Add(u4);
                    _trianglesIndices.Add(u1);

                    // u3 l3 l2     l2 u2 u3
                    _trianglesIndices.Add(u3);
                    _trianglesIndices.Add(l3);
                    _trianglesIndices.Add(l2);
                    _trianglesIndices.Add(l2);
                    _trianglesIndices.Add(u2);
                    _trianglesIndices.Add(u3);

                    // u2 l2 l1     l1 u1 u2
                    _trianglesIndices.Add(u2);
                    _trianglesIndices.Add(l2);
                    _trianglesIndices.Add(l1);
                    _trianglesIndices.Add(l1);
                    _trianglesIndices.Add(u1);
                    _trianglesIndices.Add(u2);

                    // u4 l4 l3     l3 u3 u4
                    _trianglesIndices.Add(u4);
                    _trianglesIndices.Add(l4);
                    _trianglesIndices.Add(l3);
                    _trianglesIndices.Add(l3);
                    _trianglesIndices.Add(u3);
                    _trianglesIndices.Add(u4);
                }
            }

            return _trianglesIndices.ToArray();
        }

        public bool CanDraw()
        {
            return (Xs.Length + Ys.Length) > 2 && Xs.Length > 0 && Ys.Length > 0 && Z1s.Length == Z2s.Length && Z1s.Length == (Xs.Length * Ys.Length);
        }

        public void Reset()
        {
            _positions = string.Empty;
            _trianglesIndices = new List<int>();
            _volume = null;
        }

        public double Volume()
        {
            if (!CanDraw())
            {
                throw new ArgumentException();
            }

            if (_volume.HasValue)
            {
                return _volume.Value;
            }

            _volume = 0;
            var triangles = CalculateTriangleIndices();
            var positionsArray = CalculatePositions().Split(' ');

            for (var i = 0; i < triangles.Length; i += 3)
            {
                var v1 = positionsArray[triangles[i]].ExtractDoubles();
                var v2 = positionsArray[triangles[i + 1]].ExtractDoubles();
                var v3 = positionsArray[triangles[i + 2]].ExtractDoubles();

                _volume += (((v2[1] - v1[1]) * (v3[2] - v1[2]) - (v2[2] - v1[2]) * (v3[1] - v1[1])) * (v1[0] + v2[0] + v3[0])) / 6;
            }

            return Math.Abs(_volume.Value);
        }
    }
}
