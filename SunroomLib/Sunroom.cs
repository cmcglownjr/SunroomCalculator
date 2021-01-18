using System;
using System.Data;

namespace SunroomLib
{
    public class Sunroom
    {
        // Fields
        private double _overhang, _aWall, _bWall, _cWall, _thickness;
        private string _endCut, _panelWidth;
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

        public string Endcut
        {
            get { return _endCut; }
        }

        public string PanelWidth
        {
            get { return _panelWidth; }
        }

        public Sunroom(double aWall, double bWall, double cWall, double overhang, double thickness, string endCut, 
            string panelWidth)
        {
            if (overhang > 0) {_overhang = overhang;}
            else {throw new ArgumentOutOfRangeException($"The overhang must be greater than zero.");}
            if (aWall > 0) {_aWall = aWall;}
            else {throw new ArgumentOutOfRangeException($"The awall must be greater than zero.");}
            if (bWall > 0) {_bWall = bWall;}
            else {throw new ArgumentOutOfRangeException($"The bwall must be greater than zero.");}
            if (cWall > 0) {_cWall = cWall;}
            else {throw new ArgumentOutOfRangeException($"The cwall must be greater than zero.");}
            if (thickness > 0) {_thickness = thickness;}
            else {throw new ArgumentOutOfRangeException($"The thickness must be greater than zero.");}
            if (Utilities.EndCutList.Contains(endCut))
            {
                _endCut = endCut;
            }
            else
            {
                throw new DataException($"The listed endcut, {endCut}, is not an acceptable input.");
            }

            if (Utilities.StandardPanelWidths.ContainsKey(panelWidth))
            {
                _panelWidth = panelWidth;
            }
            else
            {
                throw new DataException($"The listed panel width, {panelWidth}, is an invalid input.");
            }
        }
        protected virtual void CalculatePanelLength(){} 
        protected virtual void CalculateRoofPanels(){}
        protected virtual void CalculateSunroom(){}
    }
}