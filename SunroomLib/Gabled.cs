using System;
using System.Collections.Generic;
using static SunroomLib.Utilities;

namespace SunroomLib
{
    /// <summary>
    /// Class <c>Gabled</c> Models a gabled style sunroom.
    /// </summary>
    public class Gabled : Sunroom, IGabled
    {
        private double _leftPitch, _rightPitch, _attachedHeight, _maxHeight, _leftSoffitWallHeight, _rightSoffitWallHeight, 
            _leftSoffitWallLength, _rightSoffitWallLength, _leftSoffitHeight, _rightSoffitHeight, _leftPitchedWallLength, 
            _rightPitchedWallLength, _leftDripEdge, _rightDripEdge, _roofArea, _leftSideOverhang, _rightSideOverhang,
            _leftRoofPanels, _rightRoofPanels;

        private int _leftRoofPanelLength, _rightRoofPanelLength;

        public bool LeftPanelCut, RightPanelCut;
        public int LeftPanelType, RightPanelType, LeftRakeLength, RightRakeLength;
        public double LeftNumberPanelCuts, RightNumberPanelCuts;
        public double LeftPitch
        {
            get => _leftPitch;
            private set => _leftPitch = PitchCheck(value);
        }
        public double RightPitch
        {
            get => _rightPitch;
            private set => _rightPitch = PitchCheck(value);
        }
        public double AttachedHeight
        {
            get => _attachedHeight;
            private set => _attachedHeight = value;
        }
        public double MaxHeight
        {
            get => _maxHeight;
            private set => _maxHeight = value;
        }
        public double LeftSoffitWallHeight
        {
            get => _leftSoffitWallHeight;
            private set => _leftSoffitWallHeight = value;
        }
        public double RightSoffitWallHeight
        {
            get => _rightSoffitWallHeight;
            private set => _rightSoffitWallHeight = value;
        }
        public double LeftSoffitWallLength
        {
            get => _leftSoffitWallLength;
            private set => _leftSoffitWallLength = value;
        }
        public double RightSoffitWallLength
        {
            get => _rightSoffitWallLength;
            private set => _rightSoffitWallLength = value;
        }

        public double LeftSoffitHeight
        {
            get => _leftSoffitHeight;
            private set => _leftSoffitHeight = value;
        }

        public double RightSoffitHeight
        {
            get => _rightSoffitHeight;
            private set => _rightSoffitHeight = value;
        }

        public double LeftPitchedWallLength
        {
            get => _leftPitchedWallLength;
            private set => _leftPitchedWallLength = value;
        }

        public double RightPitchedWallLength
        {
            get => _rightPitchedWallLength;
            private set => _rightPitchedWallLength = value;
        }

        public double LeftDripEdge
        {
            get => _leftDripEdge;
            private set => _leftDripEdge = value;
        }

        public double RightDripEdge
        {
            get => _rightDripEdge;
            private set => _rightDripEdge = value;
        }

        public double RoofArea
        {
            get => _roofArea;
            private set => _roofArea = value;
        }

        public int LeftRoofPanelLength
        {
            get => _leftRoofPanelLength;
            private set => _leftRoofPanelLength = value;
        }

        public int RightRoofPanelLength
        {
            get => _rightRoofPanelLength;
            private set => _rightRoofPanelLength = value;
        }

        public double LeftRoofPanels
        {
            get => _leftRoofPanels;
            set => _leftRoofPanels = value;
        }

        public double RightRoofPanels
        {
            get => _rightRoofPanels;
            set => _rightRoofPanels = value;
        }

        public double LeftSideOverhang
        {
            get => _leftSideOverhang;
            private set => _leftSideOverhang = value;
        }

        public double RightSideOverhang
        {
            get => _rightSideOverhang;
            private set => _rightSideOverhang = value;
        }

        public Gabled(double leftWall, double frontWall, double rightWall, double overhang, double thickness, string endCut,
            string panelWidth) : base(leftWall, frontWall, rightWall, overhang, thickness, endCut, panelWidth)
        {
            LeftPitchedWallLength = frontWall / 2;
            RightPitchedWallLength = frontWall / 2;
            LeftSoffitWallLength = leftWall;
            RightSoffitWallLength = rightWall;
        }

        protected override void CalculatePanelLength()
        {
            double leftPanelLength, rightPanelLength, panelBottom, panelTop;
            if (Endcut == "SquareCut")
            {
                leftPanelLength = (LeftPitchedWallLength + Overhang) / Math.Cos(LeftPitch);
                rightPanelLength = (RightPitchedWallLength + Overhang) / Math.Cos(RightPitch);
            }
            else
            {
                panelBottom = (LeftPitchedWallLength + Overhang) / Math.Cos(LeftPitch);
                panelTop = (LeftPitchedWallLength + Overhang + Thickness * Math.Sin(LeftPitch)) / Math.Cos(LeftPitch);
                leftPanelLength = Math.Max(panelBottom, panelTop);
                panelBottom = (RightPitchedWallLength + Overhang) / Math.Cos(RightPitch);
                panelTop = (RightPitchedWallLength + Overhang + Thickness * Math.Sin(RightPitch)) / Math.Cos(RightPitch);
                rightPanelLength = Math.Max(panelBottom, panelTop);
            }

            LeftRoofPanelLength = Convert.ToInt32(Math.Ceiling(leftPanelLength / 12) * 12);
            RightRoofPanelLength = Convert.ToInt32(Math.Ceiling(rightPanelLength / 12) * 12);
            LeftRakeLength = LeftRoofPanelLength;
            RightRakeLength = RightRoofPanelLength;
            if (LeftRoofPanelLength > 192)
            {
                // Cut panel lengths in half because the lengths exceed allowed threshold
                LeftPanelCut = true;
                LeftNumberPanelCuts = RoundUpToNearest((LeftRakeLength/192.0), 0.5);
                LeftRoofPanelLength = 192;
            }
            if (RightRoofPanelLength > 192)
            {
                // Cut panel lengths in half because the lengths exceed allowed threshold
                RightPanelCut = true;
                RightNumberPanelCuts = RoundUpToNearest((RightRakeLength/192.0), 0.5);
                RightRoofPanelLength = 192;
            }
            foreach (var panelStandard in StandardPanelLengths.Keys)
            {
                if (LeftRoofPanelLength <= panelStandard)
                {
                    LeftPanelType = panelStandard;
                    break;
                }
            }
            foreach (var panelStandard in StandardPanelLengths.Keys)
            {
                if (RightRoofPanelLength <= panelStandard)
                {
                    RightPanelType = panelStandard;
                    break;
                }
            }
        }

        private double CalculateRoofPanelLength(double roofWidth)
        {
            if (Math.Abs((roofWidth / StandardPanelWidths[PanelWidth]) - 
                         (Math.Floor(roofWidth / StandardPanelWidths[PanelWidth]))) < Double.Epsilon)
            {
                return Math.Floor(roofWidth / StandardPanelWidths[PanelWidth]);
            }
            if ((roofWidth / StandardPanelWidths[PanelWidth]) >=
                     (Math.Floor(roofWidth / StandardPanelWidths[PanelWidth]) + 0.5))
            {
                LeftPanelCut = true;
                return Math.Floor(roofWidth / StandardPanelWidths[PanelWidth]) + 0.5;
            }
            return Math.Ceiling(roofWidth / StandardPanelWidths[PanelWidth]);
        }

        private double CalculateSideOverhang(double roofPanels, double soffitWall, double sideOverhang)
        {
            if ((roofPanels * StandardPanelWidths[PanelWidth] - soffitWall) < sideOverhang)
            {
                // Overhang is too short
                return roofPanels * StandardPanelWidths[PanelWidth] - soffitWall;
            }
            if ((roofPanels * StandardPanelWidths[PanelWidth] - soffitWall) >
                     StandardPanelWidths[PanelWidth] / 2)
            {
                // Overhang is too long
                return roofPanels * StandardPanelWidths[PanelWidth] - soffitWall;
            }
            return sideOverhang;
        }

        private double CalculateRoofArea(double rakeLength, double roofPanels, string panelWidth)
        {
            return rakeLength * roofPanels * StandardPanelWidths[panelWidth];
        }
        protected override void CalculateRoofPanels()
        {
            double leftRoofWidth, rightRoofWidth, leftRoofArea, rightRoofArea;
            leftRoofWidth = LeftSoffitWallLength + LeftSideOverhang;
            rightRoofWidth = RightSoffitWallLength + RightSideOverhang;
            LeftRoofPanels = CalculateRoofPanelLength(leftRoofWidth);
            RightRoofPanels = CalculateRoofPanelLength(rightRoofWidth);
            LeftSideOverhang = CalculateSideOverhang(LeftRoofPanels, LeftSoffitWallHeight, LeftSideOverhang);
            RightSideOverhang = CalculateSideOverhang(RightRoofPanels, RightSoffitWallHeight, RightSideOverhang);
            leftRoofArea = CalculateRoofArea(LeftRakeLength, LeftRoofPanels, PanelWidth);
            rightRoofArea = CalculateRoofArea(RightRakeLength, RightRoofPanels, PanelWidth);
            RoofArea = leftRoofArea + rightRoofArea;
        }

        protected override void CalculateSunroom()
        {
            CalculatePanelLength();
            CalculateRoofPanels();
        }

        public void WallHeightPitch(List<double> pitch, List<double> soffitWallHeight)
        {
            LeftPitch = pitch[0];
            RightPitch = pitch[1];
            LeftSoffitWallHeight = soffitWallHeight[0];
            RightSoffitWallHeight = soffitWallHeight[0];
            LeftSoffitHeight = LeftSoffitWallHeight - Overhang * Math.Tan(LeftPitch);
            RightSoffitHeight = RightSoffitWallHeight - Overhang * Math.Tan(RightPitch);
            AttachedHeight = (FrontWall * Math.Sin(LeftPitch) * Math.Sin(RightPitch)) / Math.Sin(Math.PI - LeftPitch - RightPitch) +
                             Math.Max(LeftSoffitWallHeight, RightSoffitWallHeight);
            MaxHeight = AttachedHeight + Math.Max(Angled(LeftPitch, Thickness),
                            Angled(RightPitch, Thickness)) +
                        (StandardPostWidth * Math.Sin(LeftPitch) * Math.Sin(RightPitch)) /
                        Math.Sin(Math.PI - LeftPitch - RightPitch);
            LeftDripEdge = CalculateDripEdge(LeftSoffitHeight, LeftPitch, Thickness, Endcut);
            RightDripEdge = CalculateDripEdge(RightSoffitHeight, RightPitch, Thickness, Endcut);
            CalculateSunroom();
        }

        public void WallHeightAttachedHeight(List<double> soffitWallHeight, double attachedHeight)
        {
            AttachedHeight = attachedHeight;
            LeftSoffitWallHeight = soffitWallHeight[0];
            RightSoffitWallHeight = soffitWallHeight[1];
            LeftPitch = Math.Atan2((AttachedHeight - LeftSoffitWallHeight),
                LeftPitchedWallLength);
            RightPitch = Math.Atan2((AttachedHeight - RightSoffitWallHeight),
                RightPitchedWallLength );
            LeftSoffitHeight = LeftSoffitWallHeight - Overhang * Math.Tan(LeftPitch);
            RightSoffitHeight = RightSoffitWallHeight - Overhang * Math.Tan(RightPitch);
            double aMaxHeight = AttachedHeight + Angled(LeftPitch, Thickness) +
                                (StandardPostWidth * Math.Sin(LeftPitch) * Math.Sin(RightPitch)) /
                                Math.Sin(Math.PI - LeftPitch - RightPitch);
            double cMaxHeight = AttachedHeight + Angled(RightPitch, Thickness) +
                                (StandardPostWidth * Math.Sin(LeftPitch) * Math.Sin(RightPitch)) /
                                Math.Sin(Math.PI - LeftPitch - RightPitch);
            MaxHeight = Math.Max(aMaxHeight, cMaxHeight);
            LeftDripEdge = CalculateDripEdge(LeftSoffitHeight, LeftPitch, Thickness, Endcut);
            RightDripEdge = CalculateDripEdge(RightSoffitHeight, RightPitch, Thickness, Endcut);
            CalculateSunroom();
        }

        public void MaxHeightPitch(List<double> pitch, double maxHeight)
        {
            LeftPitch = pitch[0];
            RightPitch = pitch[1];
            MaxHeight = maxHeight;
            LeftSoffitWallHeight = MaxHeight -
                                Math.Max(Angled(LeftPitch, Thickness), Angled(RightPitch, Thickness)) -
                                (FrontWall * Math.Sin(LeftPitch) * Math.Sin(RightPitch)) / Math.Sin(Math.PI - LeftPitch - RightPitch);
            RightSoffitWallHeight = LeftSoffitWallHeight;
            AttachedHeight = (FrontWall * Math.Sin(LeftPitch) * Math.Sin(RightPitch)) / Math.Sin(Math.PI - LeftPitch - RightPitch) +
                             LeftSoffitWallHeight;
            LeftSoffitHeight = LeftSoffitWallHeight - Overhang * Math.Tan(LeftPitch);
            RightSoffitHeight = RightSoffitWallHeight - Overhang * Math.Tan(RightPitch);
            LeftDripEdge = CalculateDripEdge(LeftSoffitHeight, LeftPitch, Thickness, Endcut);
            RightDripEdge = CalculateDripEdge(RightSoffitHeight, RightPitch, Thickness, Endcut);
            CalculateSunroom();
        }

        public void SoffitHeightAttachedHeight(List<double> soffitHeight, double attachedHeight)
        {
            LeftSoffitHeight = soffitHeight[0];
            RightSoffitHeight = soffitHeight[1];
            AttachedHeight = attachedHeight;
            LeftPitch = Math.Atan((AttachedHeight - LeftSoffitHeight) /
                               (LeftPitchedWallLength + Overhang));
            RightPitch = Math.Atan((AttachedHeight - RightSoffitHeight) /
                               (RightPitchedWallLength + Overhang));
            LeftSoffitWallHeight = LeftSoffitHeight + Overhang * Math.Tan(LeftPitch);
            RightSoffitWallHeight = RightSoffitHeight + Overhang * Math.Tan(RightPitch);
            MaxHeight = Math.Max(AttachedHeight + Angled(LeftPitch, Thickness),
                AttachedHeight + Angled(RightPitch, Thickness));
            LeftDripEdge = CalculateDripEdge(LeftSoffitHeight, LeftPitch, Thickness, Endcut);
            RightDripEdge = CalculateDripEdge(RightSoffitHeight, RightPitch, Thickness, Endcut);
            CalculateSunroom();
        }

        public void SoffitHeightPitch(List<double> pitch, List<double> soffitHeight)
        {
            LeftPitch = pitch[0];
            RightPitch = pitch[1];
            LeftSoffitHeight = soffitHeight[0];
            RightSoffitHeight = soffitHeight[1];
            LeftSoffitWallHeight = LeftSoffitHeight + Overhang * Math.Tan(LeftPitch);
            RightSoffitWallHeight = RightSoffitHeight + Overhang * Math.Tan(RightPitch);
            AttachedHeight = (FrontWall * Math.Sin(LeftPitch) * Math.Sin(RightPitch)) / Math.Sin(Math.PI - LeftPitch - RightPitch) +
                             Math.Max(LeftSoffitWallHeight, RightSoffitWallHeight);
            MaxHeight = AttachedHeight + Math.Max(Angled(LeftPitch, Thickness), Angled(RightPitch, Thickness));
            LeftDripEdge = CalculateDripEdge(LeftSoffitHeight, LeftPitch, Thickness, Endcut);
            RightDripEdge = CalculateDripEdge(RightSoffitHeight, RightPitch, Thickness, Endcut);
            CalculateSunroom();
        }

        public void DripEdgeAttachedHeight(double dripEdge, double attachedHeight)
        {
            LeftDripEdge = dripEdge;
            RightDripEdge = dripEdge;
            AttachedHeight = attachedHeight;
            double tolerance = 0.01;
            double diff = 100;
            double incr = 0.1;
            double ratioPitch = 0.0;
            double pitch = 0;
            double oldRatio, dripEstimate;
            while (diff > tolerance)
            {
                oldRatio = ratioPitch;
                ratioPitch += incr;
                pitch = Math.Atan2(ratioPitch, 12);
                dripEstimate = EstimateDripFromAttached(AttachedHeight, pitch, FrontWall / 2,
                    Overhang, Thickness, Endcut);
                diff = Math.Abs(dripEdge - dripEstimate);
                if (ratioPitch > 12){break;}

                if (dripEstimate < dripEdge)
                {
                    ratioPitch = oldRatio;
                    incr /= 2;
                }
            }

            LeftSoffitWallHeight = AttachedHeight - (FrontWall / 2 - StandardPostWidth / 2) * Math.Tan(pitch);
            RightSoffitWallHeight = LeftSoffitWallHeight;
            LeftSoffitHeight = LeftSoffitWallHeight - Overhang * Math.Tan(pitch);
            RightSoffitHeight = RightSoffitWallHeight - Overhang * Math.Tan(pitch);
            LeftPitch = pitch;
            RightPitch = pitch;
            MaxHeight = AttachedHeight + Angled(pitch, Thickness);
            CalculateSunroom();
        }

        public void DripEdgePitch(double dripEdge, List<double> pitch)
        {
            LeftPitch = pitch[0];
            RightPitch = pitch[1];
            LeftDripEdge = dripEdge;
            RightDripEdge = dripEdge;
            LeftSoffitHeight = LeftDripEdge - Angled(LeftPitch, Thickness);
            RightSoffitHeight = RightDripEdge - Angled(RightPitch, Thickness);
            double maxSoffit = Math.Max(LeftSoffitHeight, RightSoffitHeight);
            LeftSoffitWallHeight = maxSoffit + Overhang * Math.Tan(LeftPitch);
            RightSoffitWallHeight = maxSoffit + Overhang * Math.Tan(RightPitch);
            AttachedHeight = (FrontWall * Math.Sin(LeftPitch) * Math.Sin(RightPitch)) / Math.Sin(Math.PI - LeftPitch - RightPitch) +
                             Math.Max(LeftSoffitWallHeight, RightSoffitWallHeight);
            MaxHeight = AttachedHeight + Math.Max(Angled(LeftPitch, Thickness), Angled(RightPitch, Thickness));
            CalculateSunroom();
        }
    }
}