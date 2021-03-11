using System;
using static SunroomLib.Utilities;

namespace SunroomCalculatorAvalonia.Models
{
    public class ResultsModel
    {
        public string PitchLabel, PitchResults, AttachedHeightLabel, AttachedHeightResults, MaxHeightLabel, 
            MaxHeightResults, SoffitHeightLabel, SoffitHeightResults, DripEdgeLabel, DripEdgeResults, PanelLengthLabel,
            PanelLengthResults, PanelNumberLabel, PanelNumberResults, RoofAreaLabel, RoofAreaResults;
        public void FormatResults(SunroomModel sunroomModel, int style)
        {
            switch (style)
            {
                case 0:
                    StudioResults(sunroomModel);
                    break;
                case 1:
                    GableResults(sunroomModel);
                    break;
            }
        }

        private void StudioResults(SunroomModel sunroomModel)
        {
            PitchLabel = "Roof Pitch:";
            PitchResults = $"{RoundUpToNearest((Math.Tan(sunroomModel.Results["Pitch"]) * 12.0), 0.1)}/12";
            AttachedHeightLabel = "Attached Height:";
            AttachedHeightResults = $"{NiceFraction(sunroomModel.Results["Attached Height"], 16)} in.";
            MaxHeightLabel = "Max Height:";
            MaxHeightResults = $"{NiceFraction(sunroomModel.Results["Max Height"], 16)} in.";
            SoffitHeightLabel = "Soffit Height:";
            SoffitHeightResults = $"{NiceFraction(sunroomModel.Results["Soffit Height"], 16)} in.";
            DripEdgeLabel = "Drip Edge Height:";
            DripEdgeResults = $"{NiceFraction(sunroomModel.Results["Drip Edge"], 16)} in.";
            PanelLengthLabel = "Panel Length:";
            PanelLengthResults = $"{sunroomModel.Results["Panel Lengths"]} in.";
            PanelNumberLabel = "Number of Panels:";
            PanelNumberResults = $"{sunroomModel.Results["Panel Number"]}";
            RoofAreaLabel = "Roof Area:";
            RoofAreaResults = $"{sunroomModel.Results["Roof Area"]/144} ft\xB2";
        }

        private void GableResults(SunroomModel sunroomModel)
        {
            PitchLabel = "Left Roof Pitch:\nRight Roof Pitch:";
            PitchResults = $"{RoundUpToNearest((Math.Tan(sunroomModel.Results["Left Pitch"]) * 12.0), 0.1)}/12\n" +
                           $"{RoundUpToNearest((Math.Tan(sunroomModel.Results["Right Pitch"]) * 12.0), 0.1)}/12";
            AttachedHeightLabel = "Attached Height:";
            AttachedHeightResults = $"{NiceFraction(sunroomModel.Results["Attached Height"], 16)} in.";
            MaxHeightLabel = "Max Height:";
            MaxHeightResults = $"{NiceFraction(sunroomModel.Results["Max Height"], 16)} in.";
            SoffitHeightLabel = "Left Soffit Height:\nRight Soffit Height:";
            SoffitHeightResults = $"{NiceFraction(sunroomModel.Results["Left Soffit Height"], 16)} in.\n" +
                                  $"{NiceFraction(sunroomModel.Results["Right Soffit Height"], 16)} in.";
            DripEdgeLabel = "Left Drip Edge Height:\nRight Drip Edge Height:";
            DripEdgeResults = $"{NiceFraction(sunroomModel.Results["Left Drip Edge"], 16)} in.\n" +
                              $"{NiceFraction(sunroomModel.Results["Right Drip Edge"], 16)} in.";
            PanelLengthLabel = "Left Panel Length:\nRight Panel Length:";
            PanelLengthResults = $"{sunroomModel.Results["Left Panel Lengths"]} in.\n" +
                                 $"{sunroomModel.Results["Left Panel Lengths"]} in.";
            PanelNumberLabel = "Number of Panels:";
            PanelNumberResults = $"{sunroomModel.Results["Left Panel Number"] + sunroomModel.Results["Right Panel Number"]}";
            RoofAreaLabel = "Roof Area:";
            RoofAreaResults = $"{sunroomModel.Results["Roof Area"]/144} ft\xB2";
        }
    }
}