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
        private double _aPitch, _cPitch, _attachedHeight, _maxHeight, _aSoffitWallHeight, _cSoffitWallHeight, 
            _aSoffitWallLength, _cSoffitWallLength, _aSoffitHeight, _cSoffitHeight, _aPitchedWallLength, 
            _cPitchedWallLength, _aDripEdge, _cDripEdge, _roofArea, _aSideOverhang, _cSideOverhang;

        private int _aRoofPanelLength, _cRoofPanelLength;

        public bool APanelCut, CPanelCut;
        public int APanelType, CPanelType, ARakeLength, CRakeLength;
        public double ANumberPanelCuts, CNumberPanelCuts;
        public double APitch
        {
            get => _aPitch;
            private set => _aPitch = PitchCheck(value);
        }
        public double CPitch
        {
            get => _cPitch;
            private set => _cPitch = PitchCheck(value);
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
        public double ASoffitWallHeight
        {
            get => _aSoffitWallHeight;
            private set => _aSoffitWallHeight = value;
        }
        public double CSoffitWallHeight
        {
            get => _cSoffitWallHeight;
            private set => _cSoffitWallHeight = value;
        }
        public double ASoffitWallLength
        {
            get => _aSoffitWallLength;
            private set => _aSoffitWallLength = value;
        }
        public double CSoffitWallLength
        {
            get => _cSoffitWallLength;
            private set => _cSoffitWallLength = value;
        }

        public double ASoffitHeight
        {
            get => _aSoffitHeight;
            private set => _aSoffitHeight = value;
        }

        public double CSoffitHeight
        {
            get => _cSoffitHeight;
            private set => _cSoffitHeight = value;
        }

        public double APitchedWallLength
        {
            get => _aPitchedWallLength;
            private set => _aPitchedWallLength = value;
        }

        public double CPitchedWallLength
        {
            get => _cPitchedWallLength;
            private set => _cPitchedWallLength = value;
        }

        public double ADripEdge
        {
            get => _aDripEdge;
            private set => _aDripEdge = value;
        }

        public double CDripEdge
        {
            get => _cDripEdge;
            private set => _cDripEdge = value;
        }

        public double RoofArea
        {
            get => _roofArea;
            private set => _roofArea = value;
        }

        public int ARoofPanelLength
        {
            get => _aRoofPanelLength;
            private set => _aRoofPanelLength = value;
        }

        public int CRoofPanelLength
        {
            get => _cRoofPanelLength;
            private set => _cRoofPanelLength = value;
        }

        public double ASideOverhang
        {
            get => _aSideOverhang;
            private set => _aSideOverhang = value;
        }

        public double CSideOverhang
        {
            get => _cSideOverhang;
            private set => _cSideOverhang = value;
        }

        public Gabled(double aWall, double bWall, double cWall, double overhang, double thickness, string endCut,
            string panelWidth) : base(aWall, bWall, cWall, overhang, thickness, endCut, panelWidth)
        {
            APitchedWallLength = bWall / 2;
            CPitchedWallLength = bWall / 2;
            ASoffitWallLength = aWall;
            CSoffitWallLength = cWall;
        }

        protected override void CalculatePanelLength()
        {
            double aPanelLength, cPanelLength, panelBottom, panelTop;
            if (Endcut == "SquareCut")
            {
                aPanelLength = (APitchedWallLength + Overhang) / Math.Cos(APitch);
                cPanelLength = (CPitchedWallLength + Overhang) / Math.Cos(CPitch);
            }
            else
            {
                panelBottom = (APitchedWallLength + Overhang) / Math.Cos(APitch);
                panelTop = (APitchedWallLength + Overhang + Thickness * Math.Sin(APitch)) / Math.Cos(APitch);
                aPanelLength = Math.Max(panelBottom, panelTop);
                panelBottom = (CPitchedWallLength + Overhang) / Math.Cos(CPitch);
                panelTop = (CPitchedWallLength + Overhang + Thickness * Math.Sin(CPitch)) / Math.Cos(CPitch);
                cPanelLength = Math.Max(panelBottom, panelTop);
            }

            ARoofPanelLength = Convert.ToInt32(Math.Ceiling(aPanelLength / 12) * 12);
            CRoofPanelLength = Convert.ToInt32(Math.Ceiling(cPanelLength / 12) * 12);
            ARakeLength = ARoofPanelLength;
            CRakeLength = CRoofPanelLength;
            if (ARoofPanelLength > 192)
            {
                // Cut panel lengths in half because the lengths exceed allowed threshold
                APanelCut = true;
                ANumberPanelCuts = RoundUpToNearest((ARakeLength/192.0), 0.5);
                ARoofPanelLength = 192;
            }
            if (CRoofPanelLength > 192)
            {
                // Cut panel lengths in half because the lengths exceed allowed threshold
                CPanelCut = true;
                CNumberPanelCuts = RoundUpToNearest((CRakeLength/192.0), 0.5);
                CRoofPanelLength = 192;
            }
            foreach (var panelStandard in StandardPanelLengths.Keys)
            {
                if (ARoofPanelLength <= panelStandard)
                {
                    APanelType = panelStandard;
                    break;
                }
            }
            foreach (var panelStandard in StandardPanelLengths.Keys)
            {
                if (CRoofPanelLength <= panelStandard)
                {
                    CPanelType = panelStandard;
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
                APanelCut = true;
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
            double aRoofPanels, cRoofPanels, aRoofWidth, cRoofWidth, aRoofArea, cRoofArea;
            aRoofWidth = ASoffitWallLength + ASideOverhang;
            cRoofWidth = CSoffitWallLength + CSideOverhang;
            aRoofPanels = CalculateRoofPanelLength(aRoofWidth);
            cRoofPanels = CalculateRoofPanelLength(cRoofWidth);
            ASideOverhang = CalculateSideOverhang(aRoofPanels, ASoffitWallHeight, ASideOverhang);
            CSideOverhang = CalculateSideOverhang(cRoofPanels, CSoffitWallHeight, CSideOverhang);
            aRoofArea = CalculateRoofArea(ARakeLength, aRoofPanels, PanelWidth);
            cRoofArea = CalculateRoofArea(CRakeLength, cRoofPanels, PanelWidth);
            RoofArea = aRoofArea + cRoofArea;
        }

        protected override void CalculateSunroom()
        {
            CalculatePanelLength();
            CalculateRoofPanels();
        }

        public void WallHeightPitch(List<double> pitch, List<double> soffitWallHeight)
        {
            APitch = pitch[0];
            CPitch = pitch[1];
            ASoffitWallHeight = soffitWallHeight[0];
            CSoffitWallHeight = soffitWallHeight[0];
            ASoffitHeight = ASoffitWallHeight - Overhang * Math.Tan(APitch);
            CSoffitHeight = CSoffitWallHeight - Overhang * Math.Tan(CPitch);
            AttachedHeight = (BWall * Math.Sin(APitch) * Math.Sin(CPitch)) / Math.Sin(Math.PI - APitch - CPitch) +
                             Math.Max(ASoffitWallHeight, CSoffitWallHeight);
            MaxHeight = AttachedHeight + Math.Max(Angled(APitch, Thickness),
                            Angled(CPitch, Thickness)) +
                        (StandardPostWidth * Math.Sin(APitch) * Math.Sin(CPitch)) /
                        Math.Sin(Math.PI - APitch - CPitch);
            ADripEdge = CalculateDripEdge(ASoffitHeight, APitch, Thickness, Endcut);
            CDripEdge = CalculateDripEdge(CSoffitHeight, CPitch, Thickness, Endcut);
            CalculateSunroom();
        }

        public void WallHeightAttachedHeight(List<double> soffitWallHeight, double attachedHeight)
        {
            AttachedHeight = attachedHeight;
            ASoffitWallHeight = soffitWallHeight[0];
            CSoffitWallHeight = soffitWallHeight[1];
            APitch = Math.Atan2((AttachedHeight - ASoffitWallHeight),
                (APitchedWallLength - StandardPostWidth / 2));
            CPitch = Math.Atan2((AttachedHeight - CSoffitWallHeight),
                (CPitchedWallLength - StandardPostWidth / 2));
            ASoffitHeight = ASoffitWallHeight - Overhang * Math.Tan(APitch);
            CSoffitHeight = CSoffitWallHeight - Overhang * Math.Tan(CPitch);
            double aMaxHeight = AttachedHeight + Angled(APitch, Thickness) +
                                (StandardPostWidth * Math.Sin(APitch) * Math.Sin(CPitch)) /
                                Math.Sin(Math.PI - APitch - CPitch);
            double cMaxHeight = AttachedHeight + Angled(CPitch, Thickness) +
                                (StandardPostWidth * Math.Sin(APitch) * Math.Sin(CPitch)) /
                                Math.Sin(Math.PI - APitch - CPitch);
            MaxHeight = Math.Max(aMaxHeight, cMaxHeight);
            ADripEdge = CalculateDripEdge(ASoffitHeight, APitch, Thickness, Endcut);
            CDripEdge = CalculateDripEdge(CSoffitHeight, CPitch, Thickness, Endcut);
            CalculateSunroom();
        }

        public void MaxHeightPitch(List<double> pitch, double maxHeight)
        {
            APitch = pitch[0];
            CPitch = pitch[1];
            MaxHeight = maxHeight;
            AttachedHeight = (BWall * Math.Sin(APitch) * Math.Sin(CPitch)) / Math.Sin(Math.PI - APitch - CPitch);
            ASoffitWallHeight = MaxHeight -
                                Math.Max(Angled(APitch, Thickness), Angled(CPitch, Thickness)) -
                                (BWall * Math.Sin(APitch) * Math.Sin(CPitch)) / Math.Sin(Math.PI - APitch - CPitch);
            CSoffitWallHeight = ASoffitWallHeight;
            ASoffitHeight = ASoffitWallHeight - Overhang * Math.Tan(APitch);
            CSoffitHeight = CSoffitWallHeight - Overhang * Math.Tan(CPitch);
            ADripEdge = CalculateDripEdge(ASoffitHeight, APitch, Thickness, Endcut);
            CDripEdge = CalculateDripEdge(CSoffitHeight, CPitch, Thickness, Endcut);
            CalculateSunroom();
        }

        public void SoffitHeightAttachedHeight(List<double> soffitHeight, double attachedHeight)
        {
            ASoffitHeight = soffitHeight[0];
            CSoffitHeight = soffitHeight[1];
            AttachedHeight = attachedHeight;
            APitch = Math.Atan((AttachedHeight - ASoffitHeight) /
                               (APitchedWallLength + Overhang - StandardPostWidth / 2));
            CPitch = Math.Atan((AttachedHeight - CSoffitHeight) /
                               (CPitchedWallLength + Overhang - StandardPostWidth / 2));
            ASoffitWallHeight = ASoffitHeight + Overhang * Math.Tan(APitch);
            CSoffitWallHeight = CSoffitHeight + Overhang * Math.Tan(CPitch);
            MaxHeight = Math.Max(AttachedHeight + Angled(APitch, Thickness),
                AttachedHeight + Angled(CPitch, Thickness));
            ADripEdge = CalculateDripEdge(ASoffitHeight, APitch, Thickness, Endcut);
            CDripEdge = CalculateDripEdge(CSoffitHeight, CPitch, Thickness, Endcut);
            CalculateSunroom();
        }

        public void SoffitHeightPitch(List<double> pitch, List<double> soffitHeight)
        {
            APitch = pitch[0];
            CPitch = pitch[1];
            ASoffitHeight = soffitHeight[0];
            CSoffitHeight = soffitHeight[1];
            ASoffitWallHeight = ASoffitHeight + Overhang * Math.Tan(APitch);
            CSoffitWallHeight = CSoffitHeight + Overhang * Math.Tan(CPitch);
            AttachedHeight = (BWall * Math.Sin(APitch) * Math.Sin(CPitch)) / Math.Sin(Math.PI - APitch - CPitch) +
                             Math.Max(ASoffitWallHeight, CSoffitWallHeight);
            MaxHeight = AttachedHeight + Math.Max(Angled(APitch, Thickness), Angled(CPitch, Thickness));
            ADripEdge = CalculateDripEdge(ASoffitHeight, APitch, Thickness, Endcut);
            CDripEdge = CalculateDripEdge(CSoffitHeight, CPitch, Thickness, Endcut);
            CalculateSunroom();
        }

        public void DripEdgeAttachedHeight(double dripEdge, double attachedHeight)
        {
            ADripEdge = dripEdge;
            CDripEdge = dripEdge;
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
                dripEstimate = EstimateDripFromAttached(AttachedHeight, pitch, BWall / 2 - StandardPostWidth / 2,
                    Overhang, Thickness, Endcut);
                diff = Math.Abs(dripEdge - dripEstimate);
                if (ratioPitch > 12){break;}

                if (dripEstimate < dripEdge)
                {
                    ratioPitch = oldRatio;
                    incr /= 2;
                }
            }

            ASoffitWallHeight = AttachedHeight - (BWall / 2 - StandardPostWidth / 2) * Math.Tan(pitch);
            CSoffitWallHeight = ASoffitWallHeight;
            ASoffitHeight = ASoffitWallHeight - Overhang * Math.Tan(pitch);
            CSoffitHeight = CSoffitWallHeight - Overhang * Math.Tan(pitch);
            APitch = pitch;
            CPitch = pitch;
            MaxHeight = AttachedHeight + Angled(pitch, Thickness);
            CalculateSunroom();
        }

        public void DripEdgePitch(double dripEdge, List<double> pitch)
        {
            APitch = pitch[0];
            CPitch = pitch[1];
            ADripEdge = dripEdge;
            CDripEdge = dripEdge;
            ASoffitHeight = ADripEdge - Angled(APitch, Thickness);
            CSoffitHeight = CDripEdge - Angled(CPitch, Thickness);
            double maxSoffit = Math.Max(ASoffitHeight, CSoffitHeight);
            ASoffitWallHeight = maxSoffit + Overhang * Math.Tan(APitch);
            CSoffitWallHeight = maxSoffit + Overhang * Math.Tan(CPitch);
            AttachedHeight = (BWall * Math.Sin(APitch) * Math.Sin(CPitch)) / Math.Sin(Math.PI - APitch - CPitch) +
                             Math.Max(ASoffitWallHeight, CSoffitWallHeight);
            MaxHeight = AttachedHeight + Math.Max(Angled(APitch, Thickness), Angled(CPitch, Thickness));
            CalculateSunroom();
        }
    }
}