using Models.Enums;
using NUnit.Framework;
using System;

namespace Services.Tests
{
    [TestFixture]
    public class UnitConverterTests
    {

        private UnitConverterService _service;

        [SetUp]
        public void Setup()
        {
            _service = new UnitConverterService();
        }

        [Test]
        [TestCase(1, LengthUnits.Meter, LengthUnits.Foot, 3.28)]
        [TestCase(1, LengthUnits.Foot, LengthUnits.Meter, 0.30)]
        [TestCase(1, VolumeUnits.CubicMeter, VolumeUnits.CubicFoot, 35.31)]
        [TestCase(1, VolumeUnits.CubicFoot, VolumeUnits.CubicMeter, 0.03)]
        [TestCase(1, VolumeUnits.CubicMeter, VolumeUnits.Barrel, 6.11)]
        [TestCase(1, VolumeUnits.Barrel, VolumeUnits.CubicMeter, 0.16)]
        [TestCase(1, VolumeUnits.CubicFoot, VolumeUnits.Barrel, 0.17)]
        [TestCase(1, VolumeUnits.Barrel, VolumeUnits.CubicFoot, 5.78)]
        public void Convert(double value, Enum from, Enum to, double expected)
        {
            // act
            var result = _service.Convert(value, from, to);

            // assert
            Assert.AreEqual(expected, Math.Round(result, 2));
        }
    }
}
