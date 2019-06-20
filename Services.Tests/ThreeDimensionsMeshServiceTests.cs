using NUnit.Framework;

namespace Services.Tests
{
    [TestFixture]
    public class ThreeDimensionsMeshServiceTests
    {
        private ThreeDimensionsMeshService service;

        [Test]
        [TestCase(new double[] { 0, 1 }, new double[] { 0 }, new double [] { 0 }, false, TestName = "Unsufficient points to create 3d mesh")]
        [TestCase(new double[] { 0, 1 }, new double[] { 0, 1 }, new double [] { 1, 1, 2 }, false, TestName = "Unsufficient depths points")]
        [TestCase(new double[] { 0, 1 }, new double[] { 0, 1 }, new double [] { 1, 1, 1, 1 }, false, TestName = "Cube")]
        public void CanDraw(double[] x, double[] y, double[] z, bool expected)
        {
            // arrange
            service = new ThreeDimensionsMeshService(x, y, z);

            // act
            var result = service.CanDraw();

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CalculateCubePoints_ReturnEightCubePoints()
        {
            // arrange
            service = new ThreeDimensionsMeshService(new double[] { 0, 1 }, new double[] { 0, 1 }, new double[] { 1, 1, 1, 1 });

            // act
            var result = service.CalculatePositions();

            // assert
            Assert.AreEqual("0,0,0 0,0,1 1,0,0 1,0,1 0,1,0 0,1,1 1,1,0 1,1,1", result);
        }

        [Test]
        public void CalculateTrianleIndices_ReturnSixFacesIndices()
        {
            // arrange
            service = new ThreeDimensionsMeshService(new double[] { 0, 1 }, new double[] { 0, 1 }, new double[] { 1, 1, 1, 1 });

            // act
            var result = service.CalculateTriangleIndices();

            // assert
            Assert.AreEqual(new int[] { 0, 2, 6, 6, 4, 0,
                                        7, 3, 1, 1, 5, 7,
                                        0, 1, 3, 3, 2, 0,
                                        6, 7, 5, 5, 4, 6,
                                        4, 5, 1, 1, 0, 4,
                                        2, 3, 7, 7, 6, 2}, result);
        }
    }
}
