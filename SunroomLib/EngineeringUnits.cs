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

        readonly List<string> _unitType = new() {"angle", "length"};
        public double BaseMeasure;
        public string BaseUnit;
        string Measurement {get; set; }
        string UnitType {get; set; }
        double toRadians(double input)
        {
            return (Math.PI/180) * input;
        }
        public EngineeringUnits(string pMeasurement, string pUnitType)
        {
            Measurement = pMeasurement;
            if (_unitType.Any(pUnitType.Contains)) {UnitType = pUnitType;}
            else {throw new System.ArgumentException("The unit type selected is not valid!");}
            SetBase();
        }
        static bool FeetSearch(string text)
        {
            //Debug.WriteLine("Checking for feet.");
            List<string> unit = new List<string> {"ft", "'", "feet"};
            return (unit.Any(text.Contains));
        }
        static bool InchSearch(string text)
        {
            //Debug.WriteLine("Checking for inches.");
            List<string> unit = new List<string> {"in", "\""};
            return unit.Any(text.Contains);
        }
        string fractSign = "/";
        private Regex _degrees = new(@"(\d*\.?\d*)deg");
        private Regex _fract = new (@"(\d*\s?)(\d+\/\d+)");
        private Regex _ftOrIn = new (@"(\d*\.\d+|\d+)\s?[""|in|\'|ft|feet]");
        private Regex _ftAndInFract = new (@"(\d+\.?\d*)(\s?\d+\/\d+)*(\s?\D+\s?)(\d*\.?\d*)(\s?\d+\/\d+)*");

        void SetBase()
        {
            if (UnitType == "angle")
            {
                Match match = _degrees.Match(Measurement);
                if (match.Success)
                {
                    var number = double.TryParse(match.Groups[1].Value, out double n);
                    BaseMeasure = toRadians(n);
                    BaseUnit = "radian";
                }
                else
                {
                    var number = double.TryParse(Measurement, out double n);
                    BaseMeasure = toRadians(n);
                    BaseUnit = "radian";
                }
            }
            else if (UnitType == "length")
            {
                //Log.Information("Setting base");
                BaseUnit = "inch";
                double aGroup, bGroup, cGroup, dGroup;
                Match matchFract = _fract.Match(Measurement);
                Match matchFtIn = _ftOrIn.Match(Measurement);
                Match matchFtAndInFract = _ftAndInFract.Match(Measurement);
                if (FeetSearch(Measurement) & InchSearch(Measurement))
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

                        BaseMeasure = aGroup + bGroup + cGroup + dGroup;
                        //Log.Debug($"base measure is set to {base_measure} in.");
                    }
                    else
                    {
                        //Log.Error($"Unable to match this measurement: {measurement}.");
                        throw new System.ArgumentException("Unable to match current Regex.");
                    }
                }
                else if (FeetSearch(Measurement) ^ InchSearch(Measurement))
                {
                    if (FeetSearch(Measurement))
                    {
                        if (fractSign.Any(Measurement.Contains))
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
                            BaseMeasure = aGroup + bGroup;
                        }
                        else
                        {
                            BaseMeasure = Evaluate(matchFtIn.Groups[1].Value) * 12;
                        }
                    }
                    else if (InchSearch(Measurement))
                    {
                        if (fractSign.Any(Measurement.Contains))
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
                            BaseMeasure = aGroup + bGroup;
                        }
                        else
                        {
                            BaseMeasure = Evaluate(matchFtIn.Groups[1].Value);
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