using System;
using System.Collections.Generic;
using Xunit;
using SL = SunroomLib;

namespace SunroomLibTest
{
    public class TestUtilites
    {
        [Fact]
        public void TestAngled()
        {
            double result = SL.Utilities.Angled(pitch: Math.Atan(5.0 / 12.0), thickness: 10.25);
            double answer = 10.25 * (Math.Sin(Math.PI / 2) / Math.Sin(Math.PI / 2 - Math.Atan(5.0 / 12.0)));
            Assert.Equal(expected: result, actual: answer);
        }

        [Theory]
        [InlineData("5ft")]
        [InlineData("5feet")]
        [InlineData("5'")]
        [InlineData("5 ft")]
        [InlineData("5 feet")]
        [InlineData("5in")]
        [InlineData("5inch")]
        [InlineData("5\"")]
        [InlineData("5 in")]
        [InlineData("5 inch")]
        public void TestAssumeUnitsGiven(string input)
        {
            Assert.Equal(SL.Utilities.AssumeUnits(stringIn:input, assume:"ft"), actual:input);
        }
    }
}