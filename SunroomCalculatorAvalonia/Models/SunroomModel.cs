using System;
using System.Collections.Generic;
using SL = SunroomLib;
using EU = SunroomLib.EngineeringUnits;
using static SunroomLib.Utilities;

namespace SunroomCalculatorAvalonia.Models
{
    public class SunroomModel
    {
        private EU? _leftWall;
        private EU? _rightWall;
        private EU? _frontWall;
        private EU? _overhang;
        private string? _endCut;
        private string? _panelWidth;
        private double _thickness;
        private int _sunroomScenario;
        public Dictionary<string, double> Results;

        public string LeftWall
        {
            get => _leftWall?.BaseMeasure.ToString();
            set => _leftWall = new EU(AssumeUnits(value, "in"), "length");
        }
        public string RightWall
        {
            get => _rightWall?.BaseMeasure.ToString();
            set => _rightWall = new EU(AssumeUnits(value, "in"), "length");
        }
        public string FrontWall
        {
            get => _frontWall?.BaseMeasure.ToString();
            set => _frontWall = new EU(AssumeUnits(value, "in"), "length");
        }
        public string Overhang
        {
            get => _overhang?.BaseMeasure.ToString();
            set => _overhang = new EU(AssumeUnits(value, "in"), "length");
        }
        public string? EndCut
        {
            get => _endCut;
            set => _endCut = value;
        }
        public string? PanelWidth
        {
            get => _panelWidth;
            set => _panelWidth = value;
        }

        public double Thickness
        {
            get => _thickness;
            set => _thickness = value;
        }

        public SunroomModel(string leftWall, string rightWall, string frontWall, string overhang, string endCut, 
            string panelWidth, double thickness, int scenario)
        {
            LeftWall = leftWall;
            RightWall = rightWall;
            FrontWall = frontWall;
            Overhang = overhang;
            EndCut = endCut;
            PanelWidth = panelWidth;
            Thickness = thickness;
            _sunroomScenario = scenario;
            Results = new();
        }

        private SL.Studio StudioCalculations(string input1, string input2, int pitchUnit)
        {
            EU pitch = null, attachedHeight = null, wallHeight, soffitHeight, maxHeight, dripEdge;
            SL.Studio studio = new (_leftWall.BaseMeasure, _frontWall.BaseMeasure, _rightWall.BaseMeasure,
                _overhang.BaseMeasure, _thickness, _endCut, _panelWidth);
            switch (_sunroomScenario)
            {
                case 1:
                case 3:
                case 4:
                case 6:
                    if (pitchUnit == 1)
                    {
                        pitch = new EU(input2, "angle");
                    }
                    else
                    {
                        pitch = new EU(AssumeUnits(input2, "in"), "length");
                    }
                    break;
                case 2:
                case 5:
                case 7:
                    attachedHeight = new EU(AssumeUnits(input2, "in"), "length");
                    break;
            }

            switch (_sunroomScenario)
            {
                case 1:
                    wallHeight = new EU(AssumeUnits(input1, "in"), "length");
                    try
                    {
                        studio.WallHeightPitch(PitchInput(pitch), wallHeight.BaseMeasure);
                    }
                    catch (Exception e)
                    {
                        SunroomResources.SunroomMessageBox("Error", e.Message);
                        throw;
                    }
                    break;
                case 3:
                    maxHeight = new EU(AssumeUnits(input1, "in"), "length");
                    try
                    {
                        studio.MaxHeightPitch(PitchInput(pitch), maxHeight.BaseMeasure);
                    }
                    catch (Exception e)
                    {
                        SunroomResources.SunroomMessageBox("Error", e.Message);
                        throw;
                    }
                    break;
                case 4:
                    soffitHeight = new EU(AssumeUnits(input1, "in"), "length");
                    try
                    {
                        studio.SoffitHeightPitch(PitchInput(pitch), soffitHeight.BaseMeasure);
                    }
                    catch (Exception e)
                    {
                        SunroomResources.SunroomMessageBox("Error", e.Message);
                        throw;
                    }
                    break;
                case 6:
                    dripEdge = new EU(AssumeUnits(input1, "in"), "length");
                    try
                    {
                        studio.DripEdgePitch(dripEdge.BaseMeasure, PitchInput(pitch));
                    }
                    catch (Exception e)
                    {
                        SunroomResources.SunroomMessageBox("Error", e.Message);
                        throw;
                    }
                    break;
                case 2:
                    wallHeight = new EU(AssumeUnits(input1, "in"), "length");
                    try
                    {
                        studio.WallHeightAttachedHeight(wallHeight.BaseMeasure, attachedHeight.BaseMeasure);
                    }
                    catch (Exception e)
                    {
                        SunroomResources.SunroomMessageBox("Error", e.Message);
                        throw;
                    }
                    break;
                case 5:
                    soffitHeight = new EU(AssumeUnits(input1, "in"), "length");
                    try
                    {
                        studio.SoffitHeightAttachedHeight(soffitHeight.BaseMeasure, attachedHeight.BaseMeasure);
                    }
                    catch (Exception e)
                    {
                        SunroomResources.SunroomMessageBox("Error", e.Message);
                        throw;
                    }
                    break;
                case 7:
                    dripEdge = new EU(AssumeUnits(input1, "in"), "length");
                    try
                    {
                        studio.DripEdgeAttachedHeight(dripEdge.BaseMeasure, attachedHeight.BaseMeasure);
                    }
                    catch (Exception e)
                    {
                        SunroomResources.SunroomMessageBox("Error", e.Message);
                        throw;
                    }
                    break;
            }

            return studio;
        }

        private SL.Gabled GableCalculations(string input1, string input2, string input3, string input4, int pitchUnit)
        {
            EU leftPitch = null, attachedHeight = null, rightPitch = null, maxHeight;
            EU leftWallHeight, rightWallHeight, leftSoffitHeight, rightSoffitHeight, leftDripEdge, rightDripEdge;
            List<double> wallHeight, soffitHeight, dripEdge, pitch;
            SL.Gabled gabled = new(_leftWall.BaseMeasure, _frontWall.BaseMeasure, _rightWall.BaseMeasure,
                _overhang.BaseMeasure, _thickness, _endCut, _panelWidth);
            switch (_sunroomScenario)
            {
                case 1:
                case 3:
                case 4:
                case 6:
                    if (pitchUnit == 1)
                    {
                        leftPitch = new(input2, "angle");
                        rightPitch = new(input4, "angle");
                    }
                    else
                    {
                        leftPitch = new(AssumeUnits(input2, "in"), "length");
                        rightPitch = new(AssumeUnits(input4, "in"), "length");
                    }
                    break;
                case 2:
                case 5:
                case 7:
                    attachedHeight = new(AssumeUnits(input2, "in"), "length");
                    break;
            }

            switch (_sunroomScenario)
            {
                case 1:
                    leftWallHeight = new(AssumeUnits(input1, "in"), "length");
                    rightWallHeight = new(AssumeUnits(input3, "in"), "length");
                    pitch = new List<double>{PitchInput(leftPitch), PitchInput(rightPitch)};
                    wallHeight = new List<double> {leftWallHeight.BaseMeasure, rightWallHeight.BaseMeasure};
                    try
                    {
                        gabled.WallHeightPitch(pitch, wallHeight);
                    }
                    catch (Exception e)
                    {
                        SunroomResources.SunroomMessageBox("Error", e.Message);
                        throw;
                    }
                    break;
                case 2:
                    leftWallHeight = new(AssumeUnits(input1, "in"), "length");
                    rightWallHeight = new(AssumeUnits(input3, "in"), "length");
                    wallHeight = new List<double> {leftWallHeight.BaseMeasure, rightWallHeight.BaseMeasure};
                    try
                    {
                        gabled.WallHeightAttachedHeight(wallHeight, attachedHeight.BaseMeasure);
                    }
                    catch (Exception e)
                    {
                        SunroomResources.SunroomMessageBox("Error", e.Message);
                        throw;
                    }
                    break;
                case 3:
                    maxHeight = new(AssumeUnits(input1, "in"), "length");
                    pitch = new List<double>{PitchInput(leftPitch), PitchInput(rightPitch)};
                    try
                    {
                        gabled.MaxHeightPitch(pitch, maxHeight.BaseMeasure);
                    }
                    catch (Exception e)
                    {
                        SunroomResources.SunroomMessageBox("Error", e.Message);
                        throw;
                    }
                    break;
                case 4:
                    leftSoffitHeight = new(AssumeUnits(input1, "in"), "length");
                    rightSoffitHeight = new(AssumeUnits(input3, "in"), "length");
                    pitch = new List<double>{PitchInput(leftPitch), PitchInput(rightPitch)};
                    soffitHeight = new List<double> {leftSoffitHeight.BaseMeasure, rightSoffitHeight.BaseMeasure};
                    try
                    {
                        gabled.SoffitHeightPitch(pitch, soffitHeight);
                    }
                    catch (Exception e)
                    {
                        SunroomResources.SunroomMessageBox("Error", e.Message);
                        throw;
                    }
                    break;
                case 5:
                    leftSoffitHeight = new(AssumeUnits(input1, "in"), "length");
                    rightSoffitHeight = new(AssumeUnits(input3, "in"), "length");
                    soffitHeight = new List<double> {leftSoffitHeight.BaseMeasure, rightSoffitHeight.BaseMeasure};
                    try
                    {
                        gabled.SoffitHeightAttachedHeight(soffitHeight, attachedHeight.BaseMeasure);
                    }
                    catch (Exception e)
                    {
                        SunroomResources.SunroomMessageBox("Error", e.Message);
                        throw;
                    }
                    break;
                case 6:
                    leftDripEdge = new(AssumeUnits(input1, "in"), "length");
                    rightDripEdge = new(AssumeUnits(input3, "in"), "length");
                    pitch = new List<double>{PitchInput(leftPitch), PitchInput(rightPitch)};
                    dripEdge = new List<double> {leftDripEdge.BaseMeasure, rightDripEdge.BaseMeasure};
                    try
                    {
                        gabled.DripEdgePitch(dripEdge[0], pitch);
                    }
                    catch (Exception e)
                    {
                        SunroomResources.SunroomMessageBox("Error", e.Message);
                        throw;
                    }
                    break;
                case 7:
                    leftDripEdge = new(AssumeUnits(input1, "in"), "length");
                    rightDripEdge = new(AssumeUnits(input3, "in"), "length");
                    dripEdge = new List<double> {leftDripEdge.BaseMeasure, rightDripEdge.BaseMeasure};
                    try
                    {
                        gabled.DripEdgeAttachedHeight(dripEdge[0], attachedHeight.BaseMeasure);
                    }
                    catch (Exception e)
                    {
                        SunroomResources.SunroomMessageBox("Error", e.Message);
                        throw;
                    }
                    break;
            }
            return gabled;
        }

        public void CalculateSunroom(string input1, string input2, string input3, string input4, int pitchUnit,
            int sunroomStyle)
        {
            switch (sunroomStyle)
            {
                case 0:
                    var studio = StudioCalculations(input1, input2, pitchUnit);
                    Results.Add("Pitch", studio.Pitch);
                    Results.Add("Attached Height", studio.AttachedHeight);
                    Results.Add("Soffit Height", studio.SoffitHeight);
                    Results.Add("Drip Edge", studio.DripEdge);
                    Results.Add("Max Height", studio.MaxHeight);
                    Results.Add("Panel Lengths", studio.RoofPanelLength);
                    Results.Add("Panel Number", studio.RoofPanelNumber);
                    Results.Add("Roof Area", studio.RoofArea);
                    break;
                case 1:
                    var gabled = GableCalculations(input1, input2, input3, input4, pitchUnit);
                    Results.Add("Left Pitch", gabled.LeftPitch);
                    Results.Add("Right Pitch", gabled.RightPitch);
                    Results.Add("Attached Height", gabled.AttachedHeight);
                    Results.Add("Left Soffit Height", gabled.LeftSoffitHeight);
                    Results.Add("Right Soffit Height", gabled.RightSoffitHeight);
                    Results.Add("Left Drip Edge", gabled.LeftDripEdge);
                    Results.Add("Right Drip Edge", gabled.RightDripEdge);
                    Results.Add("Max Height", gabled.MaxHeight);
                    Results.Add("Left Panel Lengths", gabled.LeftRoofPanelLength);
                    Results.Add("Right Panel Lengths", gabled.RightRoofPanelLength);
                    Results.Add("Left Panel Number", gabled.LeftRoofPanels);
                    Results.Add("Right Panel Number", gabled.RightRoofPanels);
                    Results.Add("Roof Area", gabled.RoofArea);
                    break;
            }
        }
    }
}