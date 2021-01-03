using System;
using System.Collections.Generic;
using System.Linq;

namespace SunroomLib
{
    public class Utilities
    {
        public static readonly List <string> EndCutList = new() {"SquareCut", "PlumCut", "PlumCutTop"};
        public static readonly Dictionary<int, string> StandardPanelLengths = new()
        {
            {96, "8ft"},
            {120, "10ft"},
            {144, "12ft"},
            {192, "16ft"}
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
        public static double PitchInput(EngineeringUnits pInput)
        {
            switch (pInput.base_unit)
            {
                case "inch":
                    return Math.Atan(pInput.base_measure / 12.0);
                case "radian":
                    return pInput.base_measure;
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
        public static double EstimateDripFromAttached(double peak, double estimatePitch, double pitchedWallLength,
            double overhang, double thickness, double awall, double bwall, double cwall, string endcut)
        {
            double wallHeight = peak - pitchedWallLength * Math.Tan(estimatePitch);
            double soffit = wallHeight - overhang * Math.Tan(estimatePitch);
            Sunroom estimateDrip = new Sunroom
            {
                Overhang = overhang,
                AWall = awall,
                BWall = bwall,
                CWall = cwall,
                Thickness = thickness,
                Endcut = endcut
            };

            return estimateDrip.CalculateDripEdge(soffit, estimatePitch);
        }
        // public static double calculate_armstrong_panels(double pitch, double pitched_wall, double unpitched_wall)
        // {
        //     double armstrong_area = (pitched_wall / Math.Cos(pitch)) * unpitched_wall / 144;
        //     return Math.Ceiling((armstrong_area + (armstrong_area * 0.1)) / 29);
        // }
    }
}