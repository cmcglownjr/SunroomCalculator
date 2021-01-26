using System;
using System.Collections.Generic;
using System.Linq;

namespace SunroomLib
{
    /// <summary>
    /// Class <c>Utilities</c> Houses common methods used throughout library
    /// </summary>
    public static class Utilities
    {
        public static double StandardPostWidth = 3.5; // The width, in inches, of a standard 2X4
        // Describes the way the roof panels are cut. SquareCut = no cut, PlumCut = cut so its perpendicular to ground,
        // PlumCutTop = only the peak is plum cut.
        public static readonly List <string> EndCutList = new() {"SquareCut", "PlumCut", "PlumCutTop"};
        // Roof panels range from 8ft to 16ft long. Since this program is generic there is no company standard.
        public static readonly Dictionary<int, string> StandardPanelLengths = new()
        {
            {96, "8ft"},
            {120, "10ft"},
            {144, "12ft"},
            {192, "16ft"}
        };
        // Roof panels range from 24in to 36in wide. Since this program is generic there is no company standard.
        public static readonly Dictionary<string, double> StandardPanelWidths = new()
        {
            {"24in", 24.0},
            {"36in", 36.0}
        };
        // This checks to make sure the roof pitch is in a range more acceptable in colder climates.
        public static double PitchCheck(double pitch)
        {
            if (pitch < Math.Atan(4.0 / 12.0))
            {
                throw new System.Data.DataException("The pitch is less than 4/12 and is considered too low.");
            }
            if (pitch > Math.Atan(9.0 / 12.0))
            {
                throw new System.Data.DataException("The pitch is greater than 9/12 and is considered too steep.");
            }
            return pitch;
        }
        // Returns the length of a panel edge that is plum cut.
        public static double Angled(double pitch, double thickness)
        {
            return thickness * (Math.Sin(Math.PI / 2) / Math.Sin(Math.PI / 2 - pitch));
        }
        // If the input string doesn't have the listed unit then an assumed unit is appended to the string.
        public static string AssumeUnits(string stringIn, string assume)
        {
            List<string> units = new List<string> {"'", "ft", "feet", "\"", "in"};
            if (units.Any(stringIn.Contains))
            {
                return stringIn;
            }
            return stringIn + assume;
        }
        // Takes the pitch as an EngineeringUnits class and converts to radians if not already radians.
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
        // Rounds a number up to the roundTo value.
        public static double RoundUpToNearest(double number, double roundTo)
        {
            if (roundTo == 0)
            {
                return number;
            }
            return Math.Ceiling(number * (1 / roundTo)) / (1/roundTo);
        }
        // Rounds a double to a fraction such as 16ths, 8ths, etc.
        public static double NiceFraction(double input, double fraction)
        {
            return Math.Round(input * fraction) / fraction;
        }
        // Calculates the height of the drip edge given 4 inputs.
        public static double CalculateDripEdge(double soffitHeight, double pitch, double thickness, string endCut)
        {
            double angledThickness = Angled(pitch, thickness);
            if (endCut == "PlumCut")
                return soffitHeight + angledThickness;
            return soffitHeight + thickness * Math.Cos(pitch);
        }
        // Estimates the height of the drip edge given the attached height.
        public static double EstimateDripFromAttached(double attachedHeight, double estimatePitch, 
            double pitchedWallLength, double overhang, double thickness, string endCut)
        {
            double wallHeight = attachedHeight - pitchedWallLength * Math.Tan(estimatePitch);
            double soffitHeight = wallHeight - overhang * Math.Tan(estimatePitch);
            return CalculateDripEdge(soffitHeight, estimatePitch, thickness, endCut);
        }
    }
}