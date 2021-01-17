using System;
using System.Data;
using System.Collections.Generic;

namespace SunroomLib
{
    public class Gabled : Sunroom, IGabled
    {
        private double _aPitch, _cPitch, _attachedHeight, _maxHeight, _aSoffitWallHeight, _cSoffitWallHeight, 
            _aSoffitWallLength, _cSoffitWallLength, _aSoffitHeight, _cSoffitHeight, _aPitchedWallLength, 
            _cPitchedWallLength, _aDripEdge, _cDripEdge, _roofArea, _aSideOverhang, _cSideOverhang;

        private int _aRoofPanelLength, _cRoofPanelLength;

        public bool APanelCut, CPanelCut;
        public int ANumberPanelCuts, CNumberPanelCuts, APanelType, CPanelType;
        public double APitch
        {
            get => _aPitch;
            set
            {
                if (value < Math.Atan(4.0 / 12.0))
                {
                    throw new DataException($"The A side pitch is less than 4/12 and is considered too low.");
                }
                if (value > Math.Atan(9.0 / 12.0))
                {
                    throw new DataException($"The A side pitch is greater than 9/12 and is considered too steep.");
                }
                _aPitch = value;
            }
        }
        public double CPitch
        {
            get => _cPitch;
            set
            {
                if (value < Math.Atan(4.0 / 12.0))
                {
                    throw new DataException($"The C side pitch is less than 4/12 and is considered too low.");
                }
                if (value > Math.Atan(9.0 / 12.0))
                {
                    throw new DataException($"The C side pitch is greater than 9/12 and is considered too steep.");
                }
                _cPitch = value;
            }
        }
        public double AttachedHeight
        {
            get => _attachedHeight;
            set => _attachedHeight = value;
        }
        public double MaxHeight
        {
            get => _maxHeight;
            set => _maxHeight = value;
        }
        public double ASoffitWallHeight
        {
            get => _aSoffitWallHeight;
            set => _aSoffitWallHeight = value;
        }
        public double CSoffitWallHeight
        {
            get => _cSoffitWallHeight;
            set => _cSoffitWallHeight = value;
        }
        public double ASoffitWallLength
        {
            get => _aSoffitWallLength;
            set => _aSoffitWallLength = value;
        }
        public double CSoffitWallLength
        {
            get => _cSoffitWallLength;
            set => _cSoffitWallLength = value;
        }

        public double ASoffitHeight
        {
            get => _aSoffitHeight;
            set => _aSoffitHeight = value;
        }

        public double CSoffitHeight
        {
            get => _cSoffitHeight;
            set => _cSoffitHeight = value;
        }

        public double APitchedWallLength
        {
            get => _aPitchedWallLength;
            set => _aPitchedWallLength = value;
        }

        public double CPitchedWallLength
        {
            get => _cPitchedWallLength;
            set => _cPitchedWallLength = value;
        }

        public double ADripEdge
        {
            get => _aDripEdge;
            set => _aDripEdge = value;
        }

        public double CDripEdge
        {
            get => _cDripEdge;
            set => _cDripEdge = value;
        }

        public double RoofArea
        {
            get => _roofArea;
            set => _roofArea = value;
        }

        public int ARoofPanelLength
        {
            get => _aRoofPanelLength;
            set => _aRoofPanelLength = value;
        }

        public int CRoofPanelLength
        {
            get => _cRoofPanelLength;
            set => _cRoofPanelLength = value;
        }

        public double ASideOverhang
        {
            get => _aSideOverhang;
            set => _aSideOverhang = value;
        }

        public double CSideOverhang
        {
            get => _cSideOverhang;
            set => _cSideOverhang = value;
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
            while (ARoofPanelLength > 192)
            {
                // Cut panel lengths in half because the lengths exceed allowed threshold
                APanelCut = true;
                ARoofPanelLength /= 2;
                ANumberPanelCuts += 1;
            }
            while (CRoofPanelLength > 192)
            {
                // Cut panel lengths in half because the lengths exceed allowed threshold
                CPanelCut = true;
                CRoofPanelLength /= 2;
                CNumberPanelCuts += 1;
            }
            foreach (var panelStandard in Utilities.StandardPanelLengths.Keys)
            {
                if (ARoofPanelLength <= panelStandard)
                {
                    APanelType = panelStandard;
                    break;
                }
            }
            foreach (var panelStandard in Utilities.StandardPanelLengths.Keys)
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
            if ((roofWidth / Utilities.StandardPanelWidths[PanelWidth]) ==
                (Math.Floor(roofWidth / Utilities.StandardPanelWidths[PanelWidth])))
            {
                return Math.Floor(roofWidth / Utilities.StandardPanelWidths[PanelWidth]);
            }
            if ((roofWidth / Utilities.StandardPanelWidths[PanelWidth]) >=
                     (Math.Floor(roofWidth / Utilities.StandardPanelWidths[PanelWidth]) + 0.5))
            {
                APanelCut = true;
                return Math.Floor(roofWidth / Utilities.StandardPanelWidths[PanelWidth]) + 0.5;
            }
            return Math.Ceiling(roofWidth / Utilities.StandardPanelWidths[PanelWidth]);
        }

        private double CalculateSideOverhang(double roofPanels, double soffitWall, double sideOverhang)
        {
            if ((roofPanels * Utilities.StandardPanelWidths[PanelWidth] - soffitWall) < sideOverhang)
            {
                // Overhang is too short
                return roofPanels * Utilities.StandardPanelWidths[PanelWidth] - soffitWall;
            }
            if ((roofPanels * Utilities.StandardPanelWidths[PanelWidth] - soffitWall) >
                     Utilities.StandardPanelWidths[PanelWidth] / 2)
            {
                // Overhang is too long
                return roofPanels * Utilities.StandardPanelWidths[PanelWidth] - soffitWall;
            }
            return sideOverhang;
        }

        private double CalculateRoofArea(int roofPanelLength, double roofPanels, bool panelCut, int numberPanelCuts)
        {
            if (panelCut)
            {
                return Convert.ToDouble(roofPanelLength) * numberPanelCuts * roofPanels *
                       Utilities.StandardPanelWidths[PanelWidth];
            }
            return Convert.ToDouble(roofPanelLength) * roofPanels * Utilities.StandardPanelWidths[PanelWidth];
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
            aRoofArea = CalculateRoofArea(ARoofPanelLength, aRoofPanels, APanelCut, ANumberPanelCuts);
            cRoofArea = CalculateRoofArea(CRoofPanelLength, cRoofPanels, CPanelCut, CNumberPanelCuts);
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
            MaxHeight = AttachedHeight + Math.Max(Utilities.Angled(APitch, Thickness),
                            Utilities.Angled(CPitch, Thickness)) +
                        (3.5 * Math.Sin(APitch) * Math.Sin(CPitch)) / Math.Sin(Math.PI - APitch - CPitch);
            ADripEdge = Utilities.CalculateDripEdge(ASoffitHeight, APitch, Thickness, Endcut);
            CDripEdge = Utilities.CalculateDripEdge(CSoffitHeight, CPitch, Thickness, Endcut);
            CalculateSunroom();
        }

        public void WallHeightAttachedHeight(List<double> soffitWallHeight, double peak)
        {
            throw new System.NotImplementedException();
        }

        public void MaxHeightPitch(List<double> pitch, double maxHeight)
        {
            throw new System.NotImplementedException();
        }

        public void SoffitHeightAttachedHeight(List<double> soffitHeight, double peak)
        {
            throw new System.NotImplementedException();
        }

        public void SoffitHeightPitch(List<double> pitch, List<double> soffitHeight)
        {
            throw new System.NotImplementedException();
        }

        public void DripEdgeAttachedHeight(double dripEdge, double peak)
        {
            throw new System.NotImplementedException();
        }

        public void DripEdgePitch(double dripEdge, List<double> pitch)
        {
            throw new System.NotImplementedException();
        }
    }
}