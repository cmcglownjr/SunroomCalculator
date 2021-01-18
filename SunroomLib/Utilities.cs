using System;
using System.Collections.Generic;
using System.Linq;

namespace SunroomLib
{
    public static class Utilities
    {
        public static double StandardPostWidth = 3.5; // The width, in inches, of a standard 2X4
        public static readonly List <string> EndCutList = new() {"SquareCut", "PlumCut", "PlumCutTop"};
        public static readonly Dictionary<int, string> StandardPanelLengths = new()
        {
            {96, "8ft"},
            {120, "10ft"},
            {144, "12ft"},
            {192, "16ft"}
        };
        public static readonly Dictionary<string, double> StandardPanelWidths = new()
        {
            {"24in", 24.0},
            {"36in", 36.0}
        };
        public static double Angled(double pitch, double thickness)
        {
            return thickness * (Math.Sin(Math.PI / 2) / Math.Sin(Math.PI / 2 - pitch));
        }
        public static string AssumeUnits(string stringIn, string assume)
        {
            List<string> units = new List<string> {"'", "ft", "feet", "\"", "in"};
            if (units.Any(stringIn.Contains)) {return stringIn;}
            else {return stringIn + assume;}
        }
        public static double PitchInput(EngineeringUnits pitchInput)
        {
            switch (pitchInput.BaseUnit)
            {
                case "inch":
                    return Math.Atan(pitchInput.BaseMeasure / 12.0);
                case "radian":
                    return pitchInput.BaseMeasure;
                default:
                    throw new ArgumentException("The base unit needs to be 'inch' or 'radian'.");
            }
        }
        public static double PitchEstimate(double number)
        {
            return Math.Round(number * 2) / 2;
        }
        public static double NiceFraction(double input, double fraction)
        {
            return Math.Round(input * fraction) / fraction;
        }
        public static double CalculateDripEdge(double soffitHeight, double pitch, double thickness, string endCut)
        {
            double angledThickness = Angled(pitch, thickness);
            if (endCut == "PlumCut")
                return soffitHeight + angledThickness;
            return soffitHeight + thickness * Math.Cos(pitch);
        }
        public static double EstimateDripFromAttached(double attachedHeight, double estimatePitch, double pitchedWallLength,
            double overhang, double thickness, string endCut)
        {
            double wallHeight = attachedHeight - pitchedWallLength * Math.Tan(estimatePitch);
            double soffitHeight = wallHeight - overhang * Math.Tan(estimatePitch);
            return CalculateDripEdge(soffitHeight, estimatePitch, thickness, endCut);
        }
    }
}