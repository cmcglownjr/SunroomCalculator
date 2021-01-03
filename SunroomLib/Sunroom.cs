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
            get => _overhang;
            init
            {
                if (value >= 0)
                {
                    _overhang = value;
                    _sideOverhang = _overhang > 16.0 ? 16.0 : _overhang;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        $"The Overhang must be greater than or equal to zero.");
                }
            }
        }
        public double AWall
        {
            get => _aWall;
            init
            {
                if (value >= 0)
                {
                    _aWall = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        $"The A Wall must be greater than or equal to zero.");
                }
            }
        }
        public double BWall
        {
            get => _bWall;
            init
            {
                if (value >= 0)
                {
                    _bWall = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        $"The B Wall must be greater than or equal to zero.");
                }
            }
        }
        public double CWall
        {
            get => _cWall;
            init
            {
                if (value >= 0)
                {
                    _cWall = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        $"The C Wall must be greater than or equal to zero.");
                }
            }
        }
        public double Thickness
        {
            get => _thickness;
            init
            {
                if (value >= 0)
                {
                    _thickness = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        $"The Thickness must be greater than or equal to zero.");
                }
            }
        }
        public double SideOverhang => _sideOverhang;

        public string Endcut
        {
            get => _endCut;
            init
            {
                if (Utilities.EndCutList.Contains(value))
                {
                    _endCut = value;
                }
                else
                {
                    throw new DataException($"The listed endcut, {value}, is not an acceptable input.");
                }
            }
        }
        public virtual double CalculateDripEdge(double soffit, double pitch)
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

            foreach (int panelStandard in Utilities.StandardPanelLengths.Keys)
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