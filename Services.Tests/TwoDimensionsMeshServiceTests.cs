﻿using NUnit.Framework;

namespace Services.Tests
{
    [TestFixture]
    public class TwoDimensionsMeshServiceTests
    {
        private TwoDimensionsMeshService service;

        [Test]
        [TestCase(new double[] { 1 }, new double[] { 1 }, false)]
        [TestCase(new double[] { 1 }, new double[] { 1, 2 }, true)]
        public void CanDrawTests(double[] x, double[] y, bool expected)
        {
            // arrange
            service = new TwoDimensionsMeshService(x, y);

            // act
            var result = service.CanDraw();

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CalculatePositions_SquarePoints_ReturnFourPoints()
        {
            // arrange
            service = new TwoDimensionsMeshService(new double[] { 0, 1 }, new double[] { 0, 1 });

            // act
            var result = service.CalculatePositions();

            // assert
            Assert.AreEqual(" 0,0,0 1,0,0 0,1,0 1,1,0", result);
        }

        [Test]
        public void CalculateTriangleIndices_SquarePoints_ReturnTwoFacesIndices()
        {
            // arrange
            service = new TwoDimensionsMeshService(new double[] { 0, 1 }, new double[] { 0, 1 });

            // act
            var result = service.CalculateTriangleIndices();

            // assert
            Assert.AreEqual(new int[] { 0, 1, 3, 3, 2, 0 }, result);
        }
    }
}