using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace SunroomLib
{
    public class EngineeringUnits
    {
        private static double Evaluate(string expression)
        {
            DataTable table = new DataTable();
            table.Columns.Add("expression", typeof(string), expression);
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }
        List<string> unit_type = new List<string>{"angle", "length"};
        public double base_measure;
        public string base_unit;
        string measurement {get; set; }
        string u_type {get; set; }
        double toRadians(double degrees)
        {
            return (Math.PI/180) * degrees;
        }
        public EngineeringUnits(string pMeasurement, string pU_type)
        {
            measurement = pMeasurement;
            if (unit_type.Any(pU_type.Contains)) {u_type = pU_type;}
            else {throw new System.ArgumentException("The unit type selected is not valid!");}
            set_base();
        }
        static bool feet_search(string text)
        {
            //Debug.WriteLine("Checking for feet.");
            List<string> unit = new List<string> {"ft", "'", "feet"};
            return (unit.Any(text.Contains));
        }
        static bool inch_search(string text)
        {
            //Debug.WriteLine("Checking for inches.");
            List<string> unit = new List<string> {"in", "\""};
            return unit.Any(text.Contains);
        }
        string fractSign = "/";
        Regex degrees = new Regex(@"(\d*\.?\d*)deg");
        Regex fract = new Regex(@"(\d*\s?)(\d+\/\d+)");
        Regex ftOrIn = new Regex(@"(\d*\.\d+|\d+)\s?[""|in|\'|ft|feet]");
        Regex ftAndInFract = new Regex(@"(\d+\.?\d*)(\s?\d+\/\d+)*(\s?\D+\s?)(\d*\.?\d*)(\s?\d+\/\d+)*");

        void set_base()
        {
            if (u_type == "angle")
            {
                Match match = degrees.Match(measurement);
                if (match.Success)
                {
                    var number = double.TryParse(match.Groups[1].Value, out double n);
                    base_measure = toRadians(n);
                    base_unit = "radian";
                }
                else
                {
                    var number = double.TryParse(measurement, out double n);
                    base_measure = toRadians(n);
                    base_unit = "radian";
                }
            }
            else if (u_type == "length")
            {
                //Log.Information("Setting base");
                base_unit = "inch";
                double aGroup, bGroup, cGroup, dGroup;
                Match matchFract = fract.Match(measurement);
                Match matchFtin = ftOrIn.Match(measurement);
                Match matchFtAndInFract = ftAndInFract.Match(measurement);
                if (feet_search(measurement) & inch_search(measurement))
                {
                    //Log.Debug("This measuement has both inches and feet.");
                    if (matchFtAndInFract.Success)
                    {
                        if (matchFtAndInFract.Groups[1].Value != "")
                        {
                            aGroup = Convert.ToDouble(matchFtAndInFract.Groups[1].Value) * 12;
                            //Log.Debug($"{match_ft_and_in_fract.Groups[1].Value}ft converted to {a_group} in.");
                        }
                        else
                        {
                            aGroup = 0.0;
                            //Log.Debug("No whole feet so set to zero.");
                        }

                        if (matchFtAndInFract.Groups[2].Success)
                        {
                            bGroup = Evaluate(matchFtAndInFract.Groups[2].Value) * 12;
                            //Log.Debug($"{match_ft_and_in_fract.Groups[2].Value}ft converted to {b_group} in.");
                        }
                        else
                        {
                            bGroup = 0.0;
                            //Log.Debug("No fractional feet so set to zero.");
                        }

                        if (matchFtAndInFract.Groups[4].Value != "")
                        {
                            cGroup = Convert.ToDouble(matchFtAndInFract.Groups[4].Value);
                            //Log.Debug($"{match_ft_and_in_fract.Groups[4].Value}in converted to {c_group} in.");
                        }
                        else
                        {
                            cGroup = 0.0;
                            //Log.Debug("No whole inches so set to zero.");
                        }

                        if (matchFtAndInFract.Groups[5].Success)
                        {
                            dGroup = Evaluate(matchFtAndInFract.Groups[5].Value);
                            //Log.Debug($"{match_ft_and_in_fract.Groups[5].Value}in converted to {d_group} in.");
                        }
                        else
                        {
                            dGroup = 0.0;
                            //Log.Debug("No fractional inches so set to zero.");
                        }

                        base_measure = aGroup + bGroup + cGroup + dGroup;
                        //Log.Debug($"base measure is set to {base_measure} in.");
                    }
                    else
                    {
                        //Log.Error($"Unable to match this measurement: {measurement}.");
                        throw new System.ArgumentException("Unable to match current Regex.");
                    }
                }
                else if (feet_search(measurement) ^ inch_search(measurement))
                {
                    if (feet_search(measurement))
                    {
                        if (fractSign.Any(measurement.Contains))
                        {
                            if (matchFract.Groups[1].Value != "")
                            {
                                aGroup = Convert.ToDouble(matchFract.Groups[1].Value) * 12;
                            }
                            else
                            {
                                aGroup = 0.0;
                            }

                            if (matchFract.Groups[2].Success)
                            {
                                bGroup = Evaluate(matchFract.Groups[2].Value) * 12;
                            }
                            else
                            {
                                bGroup = 0.0;
                            }

                            base_measure = aGroup + bGroup;
                        }
                        else
                        {
                            base_measure = Evaluate(matchFtin.Groups[1].Value) * 12;
                        }
                    }
                    else if (inch_search(measurement))
                    {
                        if (fractSign.Any(measurement.Contains))
                        {
                            if (matchFract.Groups[1].Value != "")
                            {
                                aGroup = Convert.ToDouble(matchFract.Groups[1].Value);
                            }
                            else
                            {
                                aGroup = 0.0;
                            }

                            if (matchFract.Groups[2].Success)
                            {
                                bGroup = Evaluate(matchFract.Groups[2].Value);
                            }
                            else
                            {
                                bGroup = 0.0;
                            }

                            base_measure = aGroup + bGroup;
                        }
                        else
                        {
                            base_measure = Evaluate(matchFtin.Groups[1].Value);
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("I'm not sure what to do.");
            }
        }
    }
}