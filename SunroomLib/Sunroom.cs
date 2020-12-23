using System;
using System.Collections.Generic;

namespace SunroomLib
{
    public class Sunroom
    {
        readonly List <string> _endcutList = new List<string>() {"uncut", "plum_T_B", "plum_T"};
        public virtual double Overhang {get; private set; }
        public virtual double Awall {get; private set; }
        public virtual double Bwall {get; private set; }
        public virtual double Cwall {get; private set; }
        public virtual double Thickness {get; private set; }
        public virtual string Endcut {get; private set; }
        public virtual double SideOverhang {get; set; }
        public Sunroom(double pOverhang, double pAwall, double pBwall, double pCwall, double pThickness, string pEndcut)
        {
            if (pOverhang >= 0) {Overhang = pOverhang;}
            else {throw new ArgumentOutOfRangeException("The overhang must be greater than or equal to zero.");}
            if (pAwall >= 0) {Awall = pAwall;}
            else {throw new ArgumentOutOfRangeException("The awall must be greater than or equal to zero.");}
            if (pBwall >= 0) {Bwall = pBwall;}
            else {throw new ArgumentOutOfRangeException("The bwall must be greater than or equal to zero.");}
            if (pCwall >= 0) {Cwall = pCwall;}
            else {throw new ArgumentOutOfRangeException("The cwall must be greater than or equal to zero.");}
            if (pThickness >= 0) {Thickness = pThickness;}
            else {throw new ArgumentOutOfRangeException("The thickness must be greater than or equal to zero.");}
            if (_endcutList.Contains(pEndcut)) {Endcut = pEndcut;}
            else {throw new System.Data.DataException($"The listed endcut, {pEndcut}, is not an acceptable input.");}
            if (Overhang > 16.0) {SideOverhang = 16.0;}
            else {SideOverhang = Overhang;}
        }
        public virtual double calculate_drip_edge(double soffit, double pitch)
        {
            double angledThickness = Methods.Angled(pitch, Thickness);
            if (Endcut == "plum_T_B")
            {
                return soffit + angledThickness;
            }
            else
            {
                return soffit + Thickness * Math.Cos(pitch);
            }
        }
        public virtual Dictionary<string, object> calculate_panel_length(double pitch, double pitchedWall)
        {
            Dictionary<string, object> values = new Dictionary<string,object>();
            bool maxPanelLength = false;
            bool panelTolerance = false;
            double pLength, pBottom, pTop;
            double panelLength;
            if (Endcut == "uncut")
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
        protected virtual void calculate_sunroom(){}
    }
}