using System;
using System.Data;
using System.Collections.Generic;

namespace SunroomLib
{
    public class Gabled : Sunroom, IGabled
    {
        private double _aPitch, _cPitch, _attachedHeight, _maxHeight, _aSoffitWallHeight, _cSoffitWallHeight, 
            _aSoffitWallLength, _cSoffitWallLength, _aSoffitHeight, _cSoffitHeight, _aPitchedWallLength, 
            _cPitchedWallLength, _aDripEdge, _cDripEdge, _roofArea;

        private int _aRoofPanelLength, _cRoofPanelLength;

        public bool APanelCut, CPanelCut;
        public int ANumberPanelCuts, CNumberPanelCuts, APanelType, CPanelType;
        public double APitch
        {
            get => _aPitch;
            set
            {
                if (value < Math.Tan(4.0 / 12.0))
                {
                    throw new DataException($"The A side pitch is less than 4/12 and is considered too low.");
                }
                else if (value > Math.Tan(9.0 / 12.0))
                {
                    throw new DataException($"The A side pitch is greater than 9/12 and is considered too steep.");
                }
                else
                {
                    _aPitch = value;
                }
            }
        }
        public double CPitch
        {
            get => _cPitch;
            set
            {
                if (value < Math.Tan(4.0 / 12.0))
                {
                    throw new DataException($"The C side pitch is less than 4/12 and is considered too low.");
                }
                else if (value > Math.Tan(9.0 / 12.0))
                {
                    throw new DataException($"The C side pitch is greater than 9/12 and is considered too steep.");
                }
                else
                {
                    _cPitch = value;
                }
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

        public Gabled(double aWall, double bWall, double cWall, double overhang, double thickness, string endCut,
            string panelWidth) : base(aWall, bWall, cWall, overhang, thickness, endCut, panelWidth)
        {
            APitchedWallLength = bWall / 2;
            CPitchedWallLength = bWall / 2;
            ASoffitWallLength = aWall;
            CSoffitWallLength = cWall;
        }

        public override void CalculatePanelLength()
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

        protected override void CalculateRoofPanels(double soffitWall)
        {
            double roofPanels, roofWidth;
        }

        public void WallHeightPitch(List<double> pitch, List<double> soffitWallHeight)
        {
            throw new System.NotImplementedException();
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