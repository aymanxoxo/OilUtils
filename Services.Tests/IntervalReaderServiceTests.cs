﻿using Models;
using NUnit.Framework;
using System;

namespace Services.Tests
{
    [TestFixture]
    public class IntervalReaderServiceTests
    {
        private IntervalReaderService service;

        [Test]
        public void ReadPoints_ZeroInterval()
        {
            // arrange
            int pointsCount = 5;
            double startPoint = 0;
            service = new IntervalReaderService();

            // act
            var result = service.ReadPoints(new IntervalReaderSettings
            {
                Interval = 0,
                PointsCount = pointsCount,
                StartPoint = startPoint
            });

            // assert
            Assert.AreEqual(result.Length, pointsCount);
            foreach(var r in result)
            {
                Assert.AreEqual(r, startPoint);
            }
        }

        [Test]
        [TestCase(10, 1, 0, TestName = "Positive Interval: One point with an interval and 0 start point")]
        [TestCase(10, 2, 0, TestName = "Positive Interval: Two points with an interval 0 start point")]
        [TestCase(10, 10, 0, TestName = "Positive Interval: Multiple points with an interval 0 start point")]
        [TestCase(10, 10, 0, TestName = "Positive Interval: Multiple points with an interval greater than 0 start point")]
        [TestCase(-10, 1, 0, TestName = "Negative Interval: One point with an interval and 0 start point")]
        [TestCase(-10, 2, 0, TestName = "Negative Interval: Two points with an interval 0 start point")]
        [TestCase(-10, 10, 0, TestName = "Negative Interval: Multiple points with an interval 0 start point")]
        [TestCase(-10, 10, 0, TestName = "Negative Interval: Multiple points with an interval greater than 0 start point")]
        public void ReadPoints_PositiveInterval(double interval, int pointsCount, double startPoint)
        {
            // arrange
            service = new IntervalReaderService();

            // act
            var result = service.ReadPoints(new IntervalReaderSettings
            {
                Interval = interval,
                PointsCount = pointsCount,
                StartPoint = startPoint
            });

            // assert
            Assert.AreEqual(result.Length, pointsCount);
            Assert.AreEqual(result[0], startPoint);
            Assert.AreEqual(result[result.Length - 1], startPoint + ((pointsCount - 1) * interval));
        }

        [Test]
        [TestCase(0, 0, 0, TestName = "Zero number of points")]
        [TestCase(0, -1, 0, TestName = "negative number of points")]
        public void ReadPoints_ZeroOrLessNumberOfPoints_ThrowsArgumentException(double interval, int pointsCount, double startPoint)
        {
            // arrange
            Exception exception = null;
            service = new IntervalReaderService();

            // act
            try
            {
                service.ReadPoints(new IntervalReaderSettings
                {
                    Interval = interval,
                    PointsCount = pointsCount,
                    StartPoint = startPoint
                });
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
