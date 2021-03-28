using System;
using System.Collections.Generic;
using Avalonia.Controls;
using static SunroomLib.Utilities;

namespace SunroomCalculatorAvalonia.Models
{
    public interface IResultsModel
    {
        void FormatResults();
        void StudioResults(SunroomModel sunroomModel);
        void GableResults(SunroomModel sunroomModel);
    }

    public class ResultsModel : IResultsModel
    {
        public string? ScenarioTxtBx1, ScenarioTxtBx2, ScenarioTxtBx3, ScenarioTxtBx4;
        public string? FloorPlanLeft, FloorPlanRight, FloorPlanFront;
        public string? Overhang;
        public int SunroomStyle; // Default to Studio(0), Gable is (1)
        public int SunroomScenario, PitchUnits, EndCut;
        public ComboBoxItem? SelectedPanelWidth;
        public List<PanelThicknessModel>? PanelThicknessModels;
        public readonly PanelThicknessCombo PanelThicknessCombo = new();
        public PanelThicknessModel SelectedThickness = new();
        
        public string? PitchLabel, PitchResults, AttachedHeightLabel, AttachedHeightResults, MaxHeightLabel, 
            MaxHeightResults, SoffitHeightLabel, SoffitHeightResults, DripEdgeLabel, DripEdgeResults, PanelLengthLabel,
            PanelLengthResults, PanelNumberLabel, PanelNumberResults, RoofAreaLabel, RoofAreaResults;

        private SunroomModel CalculateSunroom()
        {
            string? endCut = null;
            string? panelWidth = (string)SelectedPanelWidth.Content;
            switch (EndCut)
            {
                case 0:
                    endCut = "PlumCut";
                    break;
                case 1:
                    endCut = "PlumCutTop";
                    break;
                case 2:
                    endCut = "SquareCut";
                    break;
            }
            var sunroom = new SunroomModel(FloorPlanLeft, FloorPlanRight, FloorPlanFront, Overhang, endCut, panelWidth, 
                SelectedThickness.ComboValue, SunroomScenario);
            try
            {
                sunroom.CalculateSunroom(ScenarioTxtBx1, ScenarioTxtBx2, ScenarioTxtBx3, 
                    ScenarioTxtBx4, PitchUnits, SunroomStyle);
            }
            catch (Exception e)
            {
                SunroomMessageBox.SunroomMessageBoxDialog("Error", e.Message);
                throw;
            }

            return sunroom;
        }
        public void FormatResults()
        {
            var sunroomModel = CalculateSunroom();
            switch (SunroomStyle)
            {
                case 0:
                    StudioResults(sunroomModel);
                    break;
                case 1:
                    GableResults(sunroomModel);
                    break;
            }
        }

        public void StudioResults(SunroomModel sunroomModel)
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

        public void GableResults(SunroomModel sunroomModel)
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