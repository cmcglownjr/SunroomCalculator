using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;
using SunroomCalculatorAvalonia.Views;
using SunroomCalculatorAvalonia.Models;
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
            RoofAreaResults = $"{sunroomModel.Results["Roof Area"]/144} ft^2";
        }

        private void GableResults(SunroomModel sunroomModel)
        {
            
        }
    }
}