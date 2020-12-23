using System;
using System.Collections.Generic;
using System.Linq;

namespace SunroomLib
{
    public class Methods
    {
        public static double Angled(double pitch, double thickness)
        {
            return thickness * (Math.Sin(Math.PI / 2) / Math.Sin(Math.PI / 2 - pitch));
        }
        public static string assume_units(string string_in, string assume)
        {
            List<string> units = new List<string> {"'", "ft", "feet", "\"", "in"};
            if (units.Any(string_in.Contains)) {return string_in;}
            else {return string_in + assume;}
        }
        public static double pitch_input(EngineeringUnits p_input)
        {
            //Debug.WriteLine("Checking Pitch Input.");
            if (p_input.base_unit == "inch") {return Math.Atan(p_input.base_measure / 12.0);}
            else if (p_input.base_unit == "radian") {return p_input.base_measure;}
            else {throw new System.ArgumentException("The base unit needs to be 'inch' or 'radian'.");}
        }
        public static double pitch_estimate(double number)
        {
            return Math.Round(number * 2) / 2;
        }
        public static double sixteenth(double number)
        {
            return Math.Round(number * 16) / 16;
        }
        public static double estimate_drip_from_peak(double peak, double estimate_pitch, double pitched_wall_length,
            double overhang, double thickness, double awall, double bwall, double cwall, string endcut)
        {
            double wall_height = peak - pitched_wall_length * Math.Tan(estimate_pitch);
            double soffit = wall_height - overhang * Math.Tan(estimate_pitch);
            Sunroom estimate_drip = new Sunroom(overhang, awall, bwall, cwall, thickness, endcut);
            return estimate_drip.calculate_drip_edge(soffit, estimate_pitch);
        }
        // public static double calculate_armstrong_panels(double pitch, double pitched_wall, double unpitched_wall)
        // {
        //     double armstrong_area = (pitched_wall / Math.Cos(pitch)) * unpitched_wall / 144;
        //     return Math.Ceiling((armstrong_area + (armstrong_area * 0.1)) / 29);
        // }
    }
}