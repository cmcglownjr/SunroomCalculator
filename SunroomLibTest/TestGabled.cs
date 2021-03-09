using System;
using System.Collections.Generic;
using Xunit;
using SL = SunroomLib;
using static SunroomLib.Utilities;

namespace SunroomLibTest
{
    public class TestGabled
    {
        private readonly SL.Gabled _sut;
        private static double pitch = Math.Atan(5.5 / 12.0);
        private static List<double> pitchList = new() {pitch, pitch};
        private static List<double> soffitWallList = new() {95.0, 95.0};
        private static List<double> soffitHeightList = new() {89.5, 89.5};

        public TestGabled()
        {
            _sut = new SL.Gabled(144.0, 192.0, 144.0, 12.0, 12.0, "PlumCut", "36in");
        }

        [Fact]
        public void TestWallHeightPitch()
        {
            _sut.WallHeightPitch(pitchList, soffitWallList);
            Assert.False(_sut.LeftPanelCut);
            Assert.False(_sut.RightPanelCut);
            Assert.Equal(pitch, _sut.LeftPitch);
            Assert.Equal(pitch, _sut.RightPitch);
            Assert.Equal(132, _sut.LeftRoofPanelLength);
            Assert.Equal(132, _sut.RightRoofPanelLength);
            Assert.Equal(144, _sut.LeftPanelType);
            Assert.Equal(144, _sut.RightPanelType);
            Assert.Equal(139, _sut.AttachedHeight);
            Assert.Equal(153, NiceFraction(_sut.MaxHeight, 16));
            Assert.Equal(95, _sut.LeftSoffitWallHeight);
            Assert.Equal(95, _sut.RightSoffitWallHeight);
            Assert.Equal(89.5, _sut.LeftSoffitHeight);
            Assert.Equal(89.5, _sut.RightSoffitHeight);
            Assert.Equal(102.6875, NiceFraction(_sut.LeftDripEdge, 16));
            Assert.Equal(102.6875, NiceFraction(_sut.RightDripEdge, 16));
            Assert.Equal(38016, _sut.RoofArea);
        }

        [Fact]
        public void TestWallHeightAttachedHeight()
        {
            _sut.WallHeightAttachedHeight(soffitWallList, 139);
            Assert.False(_sut.LeftPanelCut);
            Assert.False(_sut.RightPanelCut);
            Assert.Equal(pitch, _sut.LeftPitch);
            Assert.Equal(pitch, _sut.RightPitch);
            Assert.Equal(132, _sut.LeftRoofPanelLength);
            Assert.Equal(132, _sut.RightRoofPanelLength);
            Assert.Equal(144, _sut.LeftPanelType);
            Assert.Equal(144, _sut.RightPanelType);
            Assert.Equal(139, NiceFraction(_sut.AttachedHeight, 16));
            Assert.Equal(153, NiceFraction(_sut.MaxHeight, 16));
            Assert.Equal(95, NiceFraction(_sut.LeftSoffitWallHeight, 16));
            Assert.Equal(95, NiceFraction(_sut.RightSoffitWallHeight, 16));
            Assert.Equal(89.5, NiceFraction(_sut.LeftSoffitHeight, 16));
            Assert.Equal(89.5, NiceFraction(_sut.RightSoffitHeight, 16));
            Assert.Equal(102.6875, NiceFraction(_sut.LeftDripEdge, 16));
            Assert.Equal(102.6875, NiceFraction(_sut.RightDripEdge, 16));
            Assert.Equal(38016, _sut.RoofArea);
        }

        [Fact]
        public void TestMaxHeightPitch()
        {
            _sut.MaxHeightPitch(pitchList, 153);
            Assert.False(_sut.LeftPanelCut);
            Assert.False(_sut.RightPanelCut);
            Assert.Equal(pitch, _sut.LeftPitch);
            Assert.Equal(pitch, _sut.RightPitch);
            Assert.Equal(132, _sut.LeftRoofPanelLength);
            Assert.Equal(132, _sut.RightRoofPanelLength);
            Assert.Equal(144, _sut.LeftPanelType);
            Assert.Equal(144, _sut.RightPanelType);
            Assert.Equal(139.8125, NiceFraction(_sut.AttachedHeight, 16));
            Assert.Equal(153, NiceFraction(_sut.MaxHeight, 16));
            Assert.Equal(95.8125, NiceFraction(_sut.LeftSoffitWallHeight, 16));
            Assert.Equal(95.8125, NiceFraction(_sut.RightSoffitWallHeight, 16));
            Assert.Equal(90.3125, NiceFraction(_sut.LeftSoffitHeight, 16));
            Assert.Equal(90.3125, NiceFraction(_sut.RightSoffitHeight, 16));
            Assert.Equal(103.5, NiceFraction(_sut.LeftDripEdge, 16));
            Assert.Equal(103.5, NiceFraction(_sut.RightDripEdge, 16));
            Assert.Equal(38016, _sut.RoofArea);
        }

        [Fact]
        public void TestSoffitHeightAttachedHeight()
        {
            _sut.SoffitHeightAttachedHeight(soffitHeightList, 139);
            Assert.False(_sut.LeftPanelCut);
            Assert.False(_sut.RightPanelCut);
            Assert.Equal(pitch, _sut.LeftPitch);
            Assert.Equal(pitch, _sut.RightPitch);
            Assert.Equal(132, _sut.LeftRoofPanelLength);
            Assert.Equal(132, _sut.RightRoofPanelLength);
            Assert.Equal(144, _sut.LeftPanelType);
            Assert.Equal(144, _sut.RightPanelType);
            Assert.Equal(139, NiceFraction(_sut.AttachedHeight, 16));
            Assert.Equal(152.1875, NiceFraction(_sut.MaxHeight, 16));
            Assert.Equal(95, NiceFraction(_sut.LeftSoffitWallHeight, 16));
            Assert.Equal(95, NiceFraction(_sut.RightSoffitWallHeight, 16));
            Assert.Equal(89.5, NiceFraction(_sut.LeftSoffitHeight, 16));
            Assert.Equal(89.5, NiceFraction(_sut.RightSoffitHeight, 16));
            Assert.Equal(102.6875, NiceFraction(_sut.LeftDripEdge, 16));
            Assert.Equal(102.6875, NiceFraction(_sut.RightDripEdge, 16));
            Assert.Equal(38016, _sut.RoofArea);
        }

        [Fact]
        public void TestSoffitHeightPitch()
        {
            _sut.SoffitHeightPitch(pitchList, soffitHeightList);
            Assert.False(_sut.LeftPanelCut);
            Assert.False(_sut.RightPanelCut);
            Assert.Equal(pitch, _sut.LeftPitch);
            Assert.Equal(pitch, _sut.RightPitch);
            Assert.Equal(132, _sut.LeftRoofPanelLength);
            Assert.Equal(132, _sut.RightRoofPanelLength);
            Assert.Equal(144, _sut.LeftPanelType);
            Assert.Equal(144, _sut.RightPanelType);
            Assert.Equal(139, NiceFraction(_sut.AttachedHeight, 16));
            Assert.Equal(152.1875, NiceFraction(_sut.MaxHeight, 16));
            Assert.Equal(95, NiceFraction(_sut.LeftSoffitWallHeight, 16));
            Assert.Equal(95, NiceFraction(_sut.RightSoffitWallHeight, 16));
            Assert.Equal(89.5, NiceFraction(_sut.LeftSoffitHeight, 16));
            Assert.Equal(89.5, NiceFraction(_sut.RightSoffitHeight, 16));
            Assert.Equal(102.6875, NiceFraction(_sut.LeftDripEdge, 16));
            Assert.Equal(102.6875, NiceFraction(_sut.RightDripEdge, 16));
            Assert.Equal(38016, _sut.RoofArea);
        }

        [Fact]
        public void TestDripEdgeAttachedHeight()
        {
            _sut.DripEdgeAttachedHeight(102.6875, 139);
            Assert.False(_sut.LeftPanelCut);
            Assert.False(_sut.RightPanelCut);
            Assert.Equal(pitch, _sut.LeftPitch, 3);
            Assert.Equal(pitch, _sut.RightPitch, 3);
            Assert.Equal(132, _sut.LeftRoofPanelLength);
            Assert.Equal(132, _sut.RightRoofPanelLength);
            Assert.Equal(144, _sut.LeftPanelType);
            Assert.Equal(144, _sut.RightPanelType);
            Assert.Equal(139, NiceFraction(_sut.AttachedHeight, 16));
            Assert.Equal(152.1875, NiceFraction(_sut.MaxHeight, 16));
            Assert.Equal(95.8125, NiceFraction(_sut.LeftSoffitWallHeight, 16));
            Assert.Equal(95.8125, NiceFraction(_sut.RightSoffitWallHeight, 16));
            Assert.Equal(90.3125, NiceFraction(_sut.LeftSoffitHeight, 16));
            Assert.Equal(90.3125, NiceFraction(_sut.RightSoffitHeight, 16));
            Assert.Equal(102.6875, NiceFraction(_sut.LeftDripEdge, 16));
            Assert.Equal(102.6875, NiceFraction(_sut.RightDripEdge, 16));
            Assert.Equal(38016, _sut.RoofArea);
        }

        [Fact]
        public void TestDripEdgePitch()
        {
            _sut.DripEdgePitch(102.6875, pitchList);
            Assert.False(_sut.LeftPanelCut);
            Assert.False(_sut.RightPanelCut);
            Assert.Equal(pitch, _sut.LeftPitch, 3);
            Assert.Equal(pitch, _sut.RightPitch, 3);
            Assert.Equal(132, _sut.LeftRoofPanelLength);
            Assert.Equal(132, _sut.RightRoofPanelLength);
            Assert.Equal(144, _sut.LeftPanelType);
            Assert.Equal(144, _sut.RightPanelType);
            Assert.Equal(139, NiceFraction(_sut.AttachedHeight, 16));
            Assert.Equal(152.1875, NiceFraction(_sut.MaxHeight, 16));
            Assert.Equal(95, NiceFraction(_sut.LeftSoffitWallHeight, 16));
            Assert.Equal(95, NiceFraction(_sut.RightSoffitWallHeight, 16));
            Assert.Equal(89.5, NiceFraction(_sut.LeftSoffitHeight, 16));
            Assert.Equal(89.5, NiceFraction(_sut.RightSoffitHeight, 16));
            Assert.Equal(102.6875, NiceFraction(_sut.LeftDripEdge, 16));
            Assert.Equal(102.6875, NiceFraction(_sut.RightDripEdge, 16));
            Assert.Equal(38016, _sut.RoofArea);
        }
    }
}