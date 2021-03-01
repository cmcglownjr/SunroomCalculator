using System.Collections.Generic;

namespace SunroomCalculatorAvalonia.Models
{
    public class PanelThicknessModel
    {
        public static Dictionary<string, double> Foam1 = new()
        {
            {"3\"", 3}, 
            {"4\"", 4},
            {"6\"", 6},
            {"8\"", 8}
        };
        public static Dictionary<string, double> Foam2 = new()
        {
            {"4\"", 4},
            {"6\"", 6},
            {"8\"", 8},
            {"12\"", 12}
        };
        public static Dictionary<string, double> Aluminum = new()
        {
            {"4\"", 4},
            {"6\"", 6},
            {"8\"", 8}
        };
        public string ComboText { get; set; }
        public double ComboValue { get; set; }
        public override string ToString()
        {
            return ComboText;
        }
    }

    public class PanelThicknessCombo
    {
        public List<PanelThicknessModel> CreateList(Dictionary<string, double> comboList)
        {
            List<PanelThicknessModel> output = new();
            foreach (KeyValuePair<string, double> item in comboList)
            {
                output.Add(SetItem(item.Key, item.Value));
            }

            return output;
        }

        private PanelThicknessModel SetItem(string text, double value)
        {
            PanelThicknessModel output = new();
            output.ComboText = text;
            output.ComboValue = value;
            return output;
        }
    }
}