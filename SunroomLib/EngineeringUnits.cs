using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace SunroomLib
{
    public class EngineeringUnits
    {
        public static double Evaluate(string expression)
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
        string fract_sign = "/";
        Regex degrees = new Regex(@"(\d*\.?\d*)deg");
        Regex fract = new Regex(@"(\d*\s?)(\d+\/\d+)");
        Regex ft_or_in = new Regex(@"(\d*\.\d+|\d+)\s?[""|in|\'|ft|feet]");
        Regex ft_and_in_fract = new Regex(@"(\d+\.?\d*)(\s?\d+\/\d+)*(\s?\D+\s?)(\d*\.?\d*)(\s?\d+\/\d+)*");

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
                double a_group, b_group, c_group, d_group;
                Match match_fract = fract.Match(measurement);
                Match match_ftin = ft_or_in.Match(measurement);
                Match match_ft_and_in_fract = ft_and_in_fract.Match(measurement);
                if (feet_search(measurement) & inch_search(measurement))
                {
                    //Log.Debug("This measuement has both inches and feet.");
                    if (match_ft_and_in_fract.Success)
                    {
                        if (match_ft_and_in_fract.Groups[1].Value != "")
                        {
                            a_group = Convert.ToDouble(match_ft_and_in_fract.Groups[1].Value) * 12;
                            //Log.Debug($"{match_ft_and_in_fract.Groups[1].Value}ft converted to {a_group} in.");
                        }
                        else
                        {
                            a_group = 0.0;
                            //Log.Debug("No whole feet so set to zero.");
                        }

                        if (match_ft_and_in_fract.Groups[2].Success)
                        {
                            b_group = Evaluate(match_ft_and_in_fract.Groups[2].Value) * 12;
                            //Log.Debug($"{match_ft_and_in_fract.Groups[2].Value}ft converted to {b_group} in.");
                        }
                        else
                        {
                            b_group = 0.0;
                            //Log.Debug("No fractional feet so set to zero.");
                        }

                        if (match_ft_and_in_fract.Groups[4].Value != "")
                        {
                            c_group = Convert.ToDouble(match_ft_and_in_fract.Groups[4].Value);
                            //Log.Debug($"{match_ft_and_in_fract.Groups[4].Value}in converted to {c_group} in.");
                        }
                        else
                        {
                            c_group = 0.0;
                            //Log.Debug("No whole inches so set to zero.");
                        }

                        if (match_ft_and_in_fract.Groups[5].Success)
                        {
                            d_group = Evaluate(match_ft_and_in_fract.Groups[5].Value);
                            //Log.Debug($"{match_ft_and_in_fract.Groups[5].Value}in converted to {d_group} in.");
                        }
                        else
                        {
                            d_group = 0.0;
                            //Log.Debug("No fractional inches so set to zero.");
                        }

                        base_measure = a_group + b_group + c_group + d_group;
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
                        if (fract_sign.Any(measurement.Contains))
                        {
                            if (match_fract.Groups[1].Value != "")
                            {
                                a_group = Convert.ToDouble(match_fract.Groups[1].Value) * 12;
                            }
                            else
                            {
                                a_group = 0.0;
                            }

                            if (match_fract.Groups[2].Success)
                            {
                                b_group = Evaluate(match_fract.Groups[2].Value) * 12;
                            }
                            else
                            {
                                b_group = 0.0;
                            }

                            base_measure = a_group + b_group;
                        }
                        else
                        {
                            base_measure = Evaluate(match_ftin.Groups[1].Value) * 12;
                        }
                    }
                    else if (inch_search(measurement))
                    {
                        if (fract_sign.Any(measurement.Contains))
                        {
                            if (match_fract.Groups[1].Value != "")
                            {
                                a_group = Convert.ToDouble(match_fract.Groups[1].Value);
                            }
                            else
                            {
                                a_group = 0.0;
                            }

                            if (match_fract.Groups[2].Success)
                            {
                                b_group = Evaluate(match_fract.Groups[2].Value);
                            }
                            else
                            {
                                b_group = 0.0;
                            }

                            base_measure = a_group + b_group;
                        }
                        else
                        {
                            base_measure = Evaluate(match_ftin.Groups[1].Value);
                        }
                    }
                }
            }
            else
            {
                throw new System.ArgumentException("I'm not sure what to do.");
            }
        }
    }
}