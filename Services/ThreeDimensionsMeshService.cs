using Infrastructure.Extensions;
using Interfaces.IServices;
using System;
using System.Collections.Generic;

namespace Services
{
    public class ThreeDimensionsMeshService : IThreeDimensionsMeshService
    {
        private readonly double[] _xs;
        private readonly double[] _ys;
        private readonly double[] _z1s;
        private readonly double[] _z2s;

        private string _positions;

        private List<int> _trianglesIndices;

        public ThreeDimensionsMeshService(double[] xs, double[] ys, double[] z1s, double[] z2s)
        {
            _xs = xs;
            _ys = ys;
            _z1s = z1s;
            _z2s = z2s;

            _positions = string.Empty;
            _trianglesIndices = new List<int>();
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

            for (var y = 0; y < _ys.Length; y++)
            {
                for (var x = 0; x < _xs.Length; x++)
                {
                    var flatIndex = (y * _xs.Length) + x;

                    _positions += $" {_xs[x]},{_ys[y]},{_z1s[flatIndex]}";
                    _positions += $" {_xs[x]},{_ys[y]},{_z2s[flatIndex]}";
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

            var m = _ys.Length;
            var n = _xs.Length;

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
            return (_xs.Length + _ys.Length) > 2 && _xs.Length > 0 && _ys.Length > 0 && _z1s.Length == _z2s.Length && _z1s.Length == (_xs.Length * _ys.Length); 
        }

        public double Volume()
        {
            double volume = 0;
            var triangles = CalculateTriangleIndices();
            var positionsArray = CalculatePositions().Split(' ');

            for (var i = 0; i < triangles.Length; i+=3)
            {
                var v1 = positionsArray[triangles[i]].ExtractDoubles();
                var v2 = positionsArray[triangles[i + 1]].ExtractDoubles();
                var v3 = positionsArray[triangles[i + 2]].ExtractDoubles();

                volume += (((v2[1] - v1[1]) * (v3[2] - v1[2]) - (v2[2] - v1[2]) * (v3[1] - v1[1])) * (v1[0] + v2[0] + v3[0])) / 6;
            }

            return Math.Abs(volume);
        }
    }
}
