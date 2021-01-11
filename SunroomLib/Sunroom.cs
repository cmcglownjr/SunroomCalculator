using System;
using System.Collections.Generic;
using System.Data;

namespace SunroomLib
{
    public class Sunroom : ISunroom
    {
        // Fields
        private double _overhang, _aWall, _bWall, _cWall, _thickness, _sideOverhang;
        private string _endCut;
        public bool PanelCut;
        public int NumPanelCuts = 0;
        public int RoofPanelLength = 0;
        public int PanelType;
        public double Overhang
        {
            get { return _overhang; }
        }
        public double AWall
        {
            get { return _aWall; }
        }
        public double BWall
        {
            get { return _bWall; }
        }
        public double CWall
        {
            get { return _cWall; }
        }
        public double Thickness
        {
            get { return _thickness; }
        }
        public double SideOverhang => _sideOverhang;

        public string Endcut
        {
            get { return _endCut; }
        }

        public Sunroom(double aWall, double bWall, double cWall, double overhang, double thickness, string endCut)
        {
            if (overhang > 0) {_overhang = overhang;}
            else {throw new ArgumentOutOfRangeException($"The overhang must be greater than zero.");}
            if (aWall > 0) {_aWall = aWall;}
            else {throw new System.ArgumentOutOfRangeException($"The awall must be greater than zero.");}
            if (bWall > 0) {_bWall = bWall;}
            else {throw new System.ArgumentOutOfRangeException($"The bwall must be greater than zero.");}
            if (cWall > 0) {_cWall = cWall;}
            else {throw new System.ArgumentOutOfRangeException($"The cwall must be greater than zero.");}
            if (thickness > 0) {_thickness = thickness;}
            else {throw new System.ArgumentOutOfRangeException($"The thickness must be greater than zero.");}
            if (Utilities.EndCutList.Contains(endCut))
            {
                _endCut = endCut;
            }
            else
            {
                throw new DataException($"The listed endcut, {endCut}, is not an acceptable input.");
            }
            // TODO - this should account for the width of both types of roof panels. Not just 32in.
            if (overhang > 16.0)
            {
                _sideOverhang = 16.0;
            }
            else
            {
                _sideOverhang = overhang;
            }
        }
        public double CalculateDripEdge(double soffit, double pitch)
        {
            double angledThickness = Utilities.Angled(pitch, Thickness);
            if (Endcut == "PlumCut")
                return soffit + angledThickness;
            return soffit + Thickness * Math.Cos(pitch);
        }
        public virtual void CalculatePanelLength(double pitch, double pitchedWall)
        {
            double pLength;
            if (Endcut == "SquareCut")
            {
                pLength = (pitchedWall + Overhang) / Math.Cos(pitch);
            }
            else
            {
                var pBottom = (pitchedWall + Overhang) / Math.Cos(pitch);
                var pTop = (pitchedWall + Overhang + Thickness * Math.Sin(pitch)) / Math.Cos(pitch);
                pLength = Math.Max(pBottom, pTop);
            }
            RoofPanelLength = Convert.ToInt32(Math.Ceiling(pLength / 12) * 12);
            while (RoofPanelLength > 192)
            {
                PanelCut = true;
                RoofPanelLength /= 2;
                NumPanelCuts += 1;
            }

            foreach (var panelStandard in Utilities.StandardPanelLengths.Keys)
            {
                if (RoofPanelLength <= panelStandard)
                {
                    PanelType = panelStandard;
                    break;
                }
            }
        }
        public virtual Dictionary<string, object> CalculateRoofPanels(double soffitWall, Dictionary<string, object> 
            panelLengthDict)
        {
            throw new InvalidOperationException();
        }
        protected virtual void CalculateSunroom(){}
    }
}