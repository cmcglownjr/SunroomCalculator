using System;
using System.Data;

namespace SunroomLib
{
    public abstract class Sunroom
    {
        // Fields
        private double _overhang, _leftWall, _frontWall, _rightWall, _thickness;
        private string _endCut, _panelWidth;
        public double Overhang
        {
            get { return _overhang; }
        }
        public double LeftWall
        {
            get { return _leftWall; }
        }
        public double FrontWall
        {
            get { return _frontWall; }
        }
        public double RightWall
        {
            get { return _rightWall; }
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

        public Sunroom(double leftWall, double frontWall, double rightWall, double overhang, double thickness, string endCut, 
            string panelWidth)
        {
            if (overhang > 0) {_overhang = overhang;}
            else {throw new ArgumentOutOfRangeException($"The overhang must be greater than zero.");}
            if (leftWall > 0) {_leftWall = leftWall;}
            else {throw new ArgumentOutOfRangeException($"The awall must be greater than zero.");}
            if (frontWall > 0) {_frontWall = frontWall;}
            else {throw new ArgumentOutOfRangeException($"The bwall must be greater than zero.");}
            if (rightWall > 0) {_rightWall = rightWall;}
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