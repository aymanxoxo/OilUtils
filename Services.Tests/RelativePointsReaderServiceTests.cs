using NUnit.Framework;
using System;

namespace Services.Tests
{
    [TestFixture]
    public class RelativePointsReaderServiceTests
    {
        private RelativePointsReaderService service;

        [Test]
        public void ReadPoints_SomeDependentPointsAndDiffValues_ReturnSameLengthOfDependentPointsWithNewValues()
        {
            // arrange
            var dependentPoints = new double[] { 1, 2, 3, 4 };
            service = new RelativePointsReaderService(dependentPoints, "4, 3, 2, 1");

            // act
            var result = service.ReadPoints();

            // assert
            Assert.AreEqual(dependentPoints.Length, result.Length);
            for(var i = 0; i < dependentPoints.Length; i++)
            {
                Assert.AreEqual(result[i], 5);
            }
        }

        [Test]
        public void ReadPoints_SomeDependentNegativePointsAndDiffValues_ReturnSameLengthOfDependentPointsWithNewValues()
        {
            // arrange
            var dependentPoints = new double[] { 1, 2, 3, 4 };
            service = new RelativePointsReaderService(dependentPoints, "-1, -2, -3, -4");

            // act
            var result = service.ReadPoints();

            // assert
            Assert.AreEqual(dependentPoints.Length, result.Length);
            for (var i = 0; i < dependentPoints.Length; i++)
            {
                Assert.AreEqual(result[i], 0);
            }
        }

        // should be separated when create new custom exceptions for each case individually
        [Test]
        [TestCase(null, "4,3,2,1", TestName = "Empty or null Dependent Points")]
        [TestCase(new double[] { 1, 2, 3, 4 }, null, TestName = "Empty diff points")]
        [TestCase(new double[] { 1, 2, 3, 4 }, "1, 2", TestName = "Not equal no of depenedent points and diff points")]
        public void ReadPoints_InvalidCases_ThrowsArgumentException(double[] dependentPoints, string diffPoints)
        {
            // arrange
            Exception exception = null;
            service = new RelativePointsReaderService(dependentPoints, diffPoints);

            // act
            try
            {
                service.ReadPoints();
            }
            catch(Exception ex)
            {
                exception = ex;
            }

            // assert
            Assert.NotNull(exception);
            Assert.IsInstanceOf<ArgumentException>(exception);
        }
    }
}
