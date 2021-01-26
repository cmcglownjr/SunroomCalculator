using System;
using System.Collections.Generic;
using System.Data;
using Xunit;
using SL = SunroomLib;

namespace SunroomLibTest
{
    public class TestUtilites
    {
        [Fact]
        public void TestPitchCheck()
        {
            double expected = SL.Utilities.PitchCheck(Math.Atan(5.0 / 12.0));
            double actual = expected;
            Assert.Throws<DataException>(() => SL.Utilities.PitchCheck(Math.Atan(3.9 / 12.0)));
            Assert.Throws<DataException>(() => SL.Utilities.PitchCheck(Math.Atan(9.1 / 12.0)));
            Assert.Equal(expected, actual);
        }
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

        [Fact]
        public void TestPitchInput()
        {
            SL.EngineeringUnits length = new SL.EngineeringUnits("6in", "length");
            SL.EngineeringUnits radians = new SL.EngineeringUnits("26.565deg", "angle");
            double actual1 = Math.Atan(6.0 / 12.0);
            double actual2 = 26.565 * (Math.PI / 180);
            Assert.Equal(SL.Utilities.PitchInput(length), actual1);
            Assert.Equal(SL.Utilities.PitchInput(radians), actual2);
        }

        [Fact]
        public void TestRoundUpToNearest()
        {
            double number = 10.36;
            Assert.Equal(10.5, SL.Utilities.RoundUpToNearest(number, 0.5));
            Assert.Equal(10.4, SL.Utilities.RoundUpToNearest(number, 0.2));
        }

        [Fact]
        public void TestNiceFraction()
        {
            double number = 10.46875;
            Assert.Equal(10.5, SL.Utilities.NiceFraction(number, 16.0));
        }

        [Fact]
        public void TestCalculateDripEdge()
        {
            double soffitHeight = 95.0;
            double pitch = Math.Atan(6.0 / 12.0);
            double thickness = 12.0;
            string endCut1 = "PlumCut";
            string endCut2 = "SquareCut";
            double expected1 = 95.0 + 12.0 * (Math.Sin(Math.PI / 2) / Math.Sin(Math.PI / 2 - pitch));
            double expected2 = 95.0 + 12.0 * Math.Cos(pitch);
            Assert.Equal(expected1, SL.Utilities.CalculateDripEdge(soffitHeight, pitch, thickness, endCut1));
            Assert.Equal(expected2, SL.Utilities.CalculateDripEdge(soffitHeight, pitch, thickness, endCut2));
        }

        [Fact]
        public void TestEstimateDripFromAttached()
        {
            double attachedHeight = 143.0;
            double pitchedWallLength = 144.0;
            double overhang = 12.0;
            double thickness = 12.0;
            string endCut = "PlumCut";
            double estimatePitch = Math.Atan(6.0 / 12.0);
            double wallHeight = 71.0;
            double soffitHeight = 65.0;
            double expected = SL.Utilities.CalculateDripEdge(soffitHeight, estimatePitch, thickness, endCut);
            double actual = SL.Utilities.EstimateDripFromAttached(attachedHeight, estimatePitch, pitchedWallLength,
                overhang, thickness, endCut);
            Assert.Equal(expected, actual);
        }
    }
}