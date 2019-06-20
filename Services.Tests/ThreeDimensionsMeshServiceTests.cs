using NUnit.Framework;

namespace Services.Tests
{
    [TestFixture]
    public class ThreeDimensionsMeshServiceTests
    {
        private ThreeDimensionsMeshService service;

        [Test]
        [TestCase(new double[] { 0, 1 }, new double[] { 0 }, new double [] { 0 }, new double[] { 1 }, false, TestName = "Unsufficient points to create 3d mesh")]
        [TestCase(new double[] { 0, 1 }, new double[] { 0, 1 }, new double[] { 0, 0, 0 }, new double [] { 1, 1, 2 }, false, TestName = "Unsufficient depths points")]
        [TestCase(new double[] { 0, 1 }, new double[] { 0, 1 }, new double[] { 0, 0, 0 }, new double [] { 1, 1, 2, 3 }, false, TestName = "Upper and lower z points are not equal")]
        [TestCase(new double[] { 0, 1 }, new double[] { 0, 1 }, new double[] { 0, 0, 0, 0 }, new double [] { 1, 1, 1, 1 }, true, TestName = "Cube")]
        public void CanDraw(double[] x, double[] y, double[] z1, double[] z2, bool expected)
        {
            // arrange
            service = new ThreeDimensionsMeshService(x, y, z1, z2);

            // act
            var result = service.CanDraw();

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CalculateCubePoints_ReturnEightCubePoints()
        {
            // arrange
            service = new ThreeDimensionsMeshService(new double[] { 0, 1 }, new double[] { 0, 1 }, new double[] { 0, 0, 0, 0 }, new double[] { 1, 1, 1, 1});

            // act
            var result = service.CalculatePositions();

            // assert
            Assert.AreEqual("0,0,0 0,0,1 1,0,0 1,0,1 0,1,0 0,1,1 1,1,0 1,1,1", result);
        }

        [Test]
        public void CalculateTrianleIndices_ReturnSixFacesIndices()
        {
            // arrange
            service = new ThreeDimensionsMeshService(new double[] { 0, 1 }, new double[] { 0, 1 }, new double[] { 0, 0, 0, 0 }, new double[] { 1, 1, 1, 1 });

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

        [Test]
        public void Volume_OneLengthCube_ReturnCubeVolume()
        {
            // arrange
            service = new ThreeDimensionsMeshService(new double[] { 0, 1 }, new double[] { 0, 1 }, new double[] { 0, 0, 0, 0 }, new double[] { 1, 1, 1, 1 });

            // act
            var result = service.Volume();

            // assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Volume_OneLengthTwoCube_ReturnCubeVolume()
        {
            // arrange
            service = new ThreeDimensionsMeshService(new double[] { 0, 1, 2 }, new double[] { 0, 1 }, new double[] { 0, 0, 0, 0, 0, 0 }, new double[] { 1, 1, 1, 1, 1, 1 });

            // act
            var result = service.Volume();

            // assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Volume_TwoFacelengthCube_ReturnCubeVolume()
        {
            // arrange
            service = new ThreeDimensionsMeshService(new double[] { 0, 2 }, new double[] { 0, 2 }, new double[] { 0, 0, 0, 0 }, new double[] { 2, 2, 2, 2 });

            // act
            var result = service.Volume();

            // assert
            Assert.AreEqual(8, result);
        }
    }
}
