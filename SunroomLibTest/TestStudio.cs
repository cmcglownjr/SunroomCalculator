using System;
using Xunit;
using SL = SunroomLib;

namespace SunroomLibTest
{
    public class TestStudio
    {
        private readonly SL.Studio _sut;

        public TestStudio()
        {
            _sut = new SL.Studio(120.0, 144.0, 120.0, 12.0, 12.0, "PlumCut", "36in");
        }

        [Fact]
        public void TestWallHeightPitch()
        {
            double pitch = Math.Atan(5.5 / 12.0);
            double wallHeight = 95.0;
            _sut.WallHeightPitch(pitch, wallHeight);
            Assert.False(_sut.PanelCut);
            Assert.Equal(pitch, _sut.Pitch);
            Assert.Equal(156, _sut.RoofPanelLength);
            Assert.Equal(192, _sut.PanelType);
            Assert.Equal(156, _sut.RakeLength);
            Assert.Equal(0, _sut.NumPanelCuts);
            Assert.Equal(150, _sut.AttachedHeight);
            Assert.Equal(163.4, SL.Utilities.RoundUpToNearest(_sut.MaxHeight, 0.2));
            Assert.Equal(95, _sut.SoffitWallHeight);
            Assert.Equal(89.5, _sut.SoffitHeight);
            Assert.Equal(102.8, SL.Utilities.RoundUpToNearest(_sut.DripEdge, 0.2));
            Assert.Equal(28080, _sut.RoofArea);
        }
        [Fact]
        public void TestWallHeightAttachedHeight()
        {
            double pitch = Math.Atan(5.5 / 12.0);
            _sut.WallHeightAttachedHeight(95, 150);
            Assert.False(_sut.PanelCut);
            Assert.Equal(pitch, _sut.Pitch);
            Assert.Equal(156, _sut.RoofPanelLength);
            Assert.Equal(192, _sut.PanelType);
            Assert.Equal(156, _sut.RakeLength);
            Assert.Equal(0, _sut.NumPanelCuts);
            Assert.Equal(150, _sut.AttachedHeight);
            Assert.Equal(163.4, SL.Utilities.RoundUpToNearest(_sut.MaxHeight, 0.2));
            Assert.Equal(95, _sut.SoffitWallHeight);
            Assert.Equal(89.5, _sut.SoffitHeight);
            Assert.Equal(102.8, SL.Utilities.RoundUpToNearest(_sut.DripEdge, 0.2));
            Assert.Equal(28080, _sut.RoofArea);
        }

        [Fact]
        public void TestMaxHeightPitch()
        {
            double pitch = Math.Atan(5.5 / 12.0);
            _sut.MaxHeightPitch(pitch, 163.4);
            Assert.False(_sut.PanelCut);
            Assert.Equal(pitch, _sut.Pitch);
            Assert.Equal(156, _sut.RoofPanelLength);
            Assert.Equal(192, _sut.PanelType);
            Assert.Equal(156, _sut.RakeLength);
            Assert.Equal(0, _sut.NumPanelCuts);
            Assert.Equal(150.2, SL.Utilities.RoundUpToNearest(_sut.AttachedHeight, 0.2));
            Assert.Equal(163.4, SL.Utilities.RoundUpToNearest(_sut.MaxHeight, 0.2));
            Assert.Equal(95.2, SL.Utilities.RoundUpToNearest(_sut.SoffitWallHeight, 0.2));
            Assert.Equal(89.8, SL.Utilities.RoundUpToNearest(_sut.SoffitHeight, 0.2));
            Assert.Equal(103, SL.Utilities.RoundUpToNearest(_sut.DripEdge, 0.2));
            Assert.Equal(28080, _sut.RoofArea);
        }

        [Fact]
        public void TestSoffitHeightAttachedHeight()
        {
            double pitch = Math.Atan(5.5 / 12.0);
            _sut.SoffitHeightAttachedHeight(90, 150);
            double calculatedPitch = SL.Utilities.RoundUpToNearest(Math.Tan(_sut.Pitch) * 12, 0.5);
            Assert.False(_sut.PanelCut);
            Assert.Equal(pitch, Math.Atan(calculatedPitch/12));
            Assert.Equal(156, _sut.RoofPanelLength);
            Assert.Equal(192, _sut.PanelType);
            Assert.Equal(156, _sut.RakeLength);
            Assert.Equal(0, _sut.NumPanelCuts);
            Assert.Equal(150, SL.Utilities.RoundUpToNearest(_sut.AttachedHeight, 0.2));
            Assert.Equal(163.2, SL.Utilities.RoundUpToNearest(_sut.MaxHeight, 0.2));
            Assert.Equal(95.6, SL.Utilities.RoundUpToNearest(_sut.SoffitWallHeight, 0.2));
            Assert.Equal(90, SL.Utilities.RoundUpToNearest(_sut.SoffitHeight, 0.2));
            Assert.Equal(103.2, SL.Utilities.RoundUpToNearest(_sut.DripEdge, 0.2));
            Assert.Equal(28080, _sut.RoofArea);
        }

        [Fact]
        public void TestSoffitHeightPitch()
        {
            double pitch = Math.Atan(5.5 / 12.0);
            _sut.SoffitHeightPitch(pitch, 90);
            Assert.False(_sut.PanelCut);
            Assert.Equal(pitch, _sut.Pitch);
            Assert.Equal(156, _sut.RoofPanelLength);
            Assert.Equal(192, _sut.PanelType);
            Assert.Equal(156, _sut.RakeLength);
            Assert.Equal(0, _sut.NumPanelCuts);
            Assert.Equal(150.5, SL.Utilities.RoundUpToNearest(_sut.AttachedHeight, 0.1));
            Assert.Equal(163.8, SL.Utilities.RoundUpToNearest(_sut.MaxHeight, 0.2));
            Assert.Equal(95.5, SL.Utilities.RoundUpToNearest(_sut.SoffitWallHeight, 0.1));
            Assert.Equal(90, SL.Utilities.RoundUpToNearest(_sut.SoffitHeight, 0.2));
            Assert.Equal(103.4, SL.Utilities.RoundUpToNearest(_sut.DripEdge, 0.2));
            Assert.Equal(28080, _sut.RoofArea);
        }

        [Fact]
        public void TestDripEdgeAttachedHeight()
        {
            double pitch = Math.Atan(5.5 / 12.0);
            _sut.DripEdgeAttachedHeight(103.2, 150.5);
            double calculatedPitch = SL.Utilities.RoundUpToNearest(Math.Tan(_sut.Pitch) * 12, 0.5);
            Assert.False(_sut.PanelCut);
            Assert.Equal(pitch, Math.Atan(calculatedPitch/12));
            Assert.Equal(156, _sut.RoofPanelLength);
            Assert.Equal(192, _sut.PanelType);
            Assert.Equal(156, _sut.RakeLength);
            Assert.Equal(0, _sut.NumPanelCuts);
            Assert.Equal(150.5, SL.Utilities.RoundUpToNearest(_sut.AttachedHeight, 0.1));
            Assert.Equal(163.8, SL.Utilities.RoundUpToNearest(_sut.MaxHeight, 0.2));
            Assert.Equal(95.6, SL.Utilities.RoundUpToNearest(_sut.SoffitWallHeight, 0.1));
            Assert.Equal(90.1, SL.Utilities.RoundUpToNearest(_sut.SoffitHeight, 0.1));
            Assert.Equal(103.2, SL.Utilities.RoundUpToNearest(_sut.DripEdge, 0.2));
            Assert.Equal(28080, _sut.RoofArea);
        }

        [Fact]
        public void TestDripEdgePitch()
        {
            double pitch = Math.Atan(5.5 / 12.0);
            _sut.DripEdgePitch(103.2, pitch);
            Assert.False(_sut.PanelCut);
            Assert.Equal(pitch, _sut.Pitch);
            Assert.Equal(156, _sut.RoofPanelLength);
            Assert.Equal(192, _sut.PanelType);
            Assert.Equal(156, _sut.RakeLength);
            Assert.Equal(0, _sut.NumPanelCuts);
            Assert.Equal(150.5, SL.Utilities.RoundUpToNearest(_sut.AttachedHeight, 0.1));
            Assert.Equal(163.7, _sut.MaxHeight);
            Assert.Equal(95.5, SL.Utilities.RoundUpToNearest(_sut.SoffitWallHeight, 0.1));
            Assert.Equal(90, SL.Utilities.RoundUpToNearest(_sut.SoffitHeight, 0.1));
            Assert.Equal(103.2, SL.Utilities.RoundUpToNearest(_sut.DripEdge, 0.2));
            Assert.Equal(28080, _sut.RoofArea);
        }
    }
}