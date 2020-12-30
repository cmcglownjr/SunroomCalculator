using System;
using System.Collections.Generic;
using System.Data;

namespace SunroomLib
{
    public class Sunroom
    {
        readonly List <string> _endCutList = new() {"SquareCut", "PlumCut", "PlumCutTop"};
        private double _overhang;
        private double _aWall;
        private double _bWall;
        private double _cWall;
        private double _thickness;
        private double _sideOverhang;
        private string _endCut;
        public virtual double Overhang
        {
            get => _overhang;
            set
            {
                if (value >= 0)
                {
                    _overhang = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        $"The Overhang must be greater than or equal to zero.");
                }
            }
        }
        public virtual double AWall
        {
            get => _aWall;
            set
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
        public virtual double BWall
        {
            get => _bWall;
            set
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
        public virtual double CWall
        {
            get => _cWall;
            set
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
        public virtual double Thickness
        {
            get => _thickness;
            set
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
        public virtual double SideOverhang => _sideOverhang;

        public virtual string Endcut
        {
            get => _endCut;
            set
            {
                if (_endCutList.Contains(value))
                {
                    _endCut = value;
                }
                else
                {
                    throw new DataException($"The listed endcut, {value}, is not an acceptable input.");
                }
            }
        }
        public Sunroom()
        {
            _sideOverhang = _overhang > 16.0 ? 16.0 : _overhang;
        }
        public virtual double CalculateDripEdge(double soffit, double pitch)
        {
            double angledThickness = Utilities.Angled(pitch, Thickness);
            if (Endcut == "PlumCut")
                return soffit + angledThickness;
            return soffit + Thickness * Math.Cos(pitch);
        }
        public virtual Dictionary<string, object> CalculatePanelLength(double pitch, double pitchedWall)
        {
            Dictionary<string, object> values = new Dictionary<string,object>();
            bool maxPanelLength = false;
            bool panelTolerance = false;
            double pLength, pBottom, pTop;
            double panelLength;
            if (Endcut == "SquareCut")
            {
                pLength = (pitchedWall + Overhang) / Math.Cos(pitch);
            }
            else
            {
                pBottom = (pitchedWall + Overhang) / Math.Cos(pitch);
                pTop = (pitchedWall + Overhang + Thickness * Math.Sin(pitch)) / Math.Cos(pitch);
                pLength = Math.Max(pBottom, pTop);
            }
            if (pLength % 12 <= 1) // This checks to see if the panel length is a maximum 1 inch past the nearest foot
            {
                panelTolerance = true; // If it is 1 inch past the foot then it is within manufaturer's tolerance and is rounded down
                panelLength = Math.Floor(pLength / 12) * 12;
            }
            else
            {
                // If more than one inch past foot then it is rounded up to nearest foot.
                panelLength = Math.Ceiling(pLength / 12) * 12;
            }
            if (panelLength > 288)
            {
                maxPanelLength = true;
                panelLength /= 2;
            }
            values.Add("Panel Length", panelLength); // double
            values.Add("Max Length Check", maxPanelLength); // bool
            values.Add("Panel Tolerance", panelTolerance); // bool
            return values;
        }
        protected virtual void CalculateSunroom(){}
    }
}