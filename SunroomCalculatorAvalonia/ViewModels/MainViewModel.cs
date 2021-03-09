using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.ReactiveUI;
using Avalonia.Utilities;
using DynamicData.Binding;
using JetBrains.Annotations;
using ReactiveUI;
using SunroomCalculatorAvalonia.Views;
using SunroomCalculatorAvalonia.Models;
using SL = SunroomLib;
using EU = SunroomLib.EngineeringUnits;
using static SunroomLib.Utilities;

namespace SunroomCalculatorAvalonia.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private UserControl _selectedViewModel;
        private Image _diagramImage = new();
        private int _sunroomStyle; // Default to Studio(0), Gable is (1)
        private int _sunroomScenario, _navigation, _pitchUnits, _endCut;
        private string _scenarioLabel1, _scenarioLabel2, _scenarioLabel3, _scenarioLabel4;
        private string _navLabel = "Begin Here";
        private string _scenarioWatermark1, _scenarioWatermark2, _scenarioWatermark3, _scenarioWatermark4;
        private bool _scenarioPitch, _scenarioInput1, _scenarioInput2, _scenarioInput3, _scenarioInput4, 
            _panelThicknessEnable, _navBtn1Enable, _navBtn2Enable, _calcBtnEnable, _test;
        private string _scenarioTxtBx1, _scenarioTxtBx2, _scenarioTxtBx3, _scenarioTxtBx4, _floorPlanLeft, 
            _floorPlanRight, _floorPlanFront, _overhang;

        private ComboBoxItem _selectedPanelWidth;
        private readonly SunroomResources _sunroomResources = new();
        private List<PanelThicknessModel> _panelThicknessModels;
        private readonly PanelThicknessCombo _panelThicknessCombo = new();
        private PanelThicknessModel _selectedThickness;
        public ReactiveCommand<Unit, Unit> StudioStyle { get; }
        public ReactiveCommand<Unit, Unit> GableStyle { get; }
        public ReactiveCommand<Unit, Unit> Scenario1Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario2Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario3Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario4Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario5Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario6Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario7Select { get; }
        public ReactiveCommand<Unit, Unit> PitchUnit1 { get; }
        public ReactiveCommand<Unit, Unit> PitchUnit2 { get; }
        public ReactiveCommand<Unit, Unit> EndCutSelect1 { get; }
        public ReactiveCommand<Unit, Unit> EndCutSelect2 { get; }
        public ReactiveCommand<Unit, Unit> EndCutSelect3 { get; }
        public ReactiveCommand<Unit, Unit> PanelTypeSelect1 { get; }
        public ReactiveCommand<Unit, Unit> PanelTypeSelect2 { get; }
        public ReactiveCommand<Unit, Unit> PanelTypeSelect3 { get; }
        public ReactiveCommand<Unit, Unit> NavBtn1 { get; }
        public ReactiveCommand<Unit, Unit> NavBtn2 { get; }
        public ReactiveCommand<Unit, Unit> CalculateBtn { get; }
        
        public Image DiagramImage
        {
            get => _diagramImage;
            set => _diagramImage = value;
        }
        public string ScenarioLabel1
        {
            get => _scenarioLabel1;
            set => this.RaiseAndSetIfChanged(ref _scenarioLabel1, value);
        }
        public string ScenarioLabel2
        {
            get => _scenarioLabel2;
            set => this.RaiseAndSetIfChanged(ref _scenarioLabel2, value);
        }
        public string ScenarioLabel3
        {
            get => _scenarioLabel3;
            set => _scenarioLabel3 = this.RaiseAndSetIfChanged(ref _scenarioLabel3, value);
        }
        public string ScenarioLabel4
        {
            get => _scenarioLabel4;
            set => this.RaiseAndSetIfChanged(ref _scenarioLabel4, value);
        }
        public string ScenarioWatermark1
        {
            get => _scenarioWatermark1;
            set => this.RaiseAndSetIfChanged(ref _scenarioWatermark1, value);
        }
        public string ScenarioWatermark2
        {
            get => _scenarioWatermark2;
            set => this.RaiseAndSetIfChanged(ref _scenarioWatermark2, value);
        }
        public string ScenarioWatermark3
        {
            get => _scenarioWatermark3;
            set => this.RaiseAndSetIfChanged(ref _scenarioWatermark3, value);
        }
        public string ScenarioWatermark4
        {
            get => _scenarioWatermark4;
            set => this.RaiseAndSetIfChanged(ref _scenarioWatermark4, value);
        }
        public string NavLabel
        {
            get => _navLabel;
            set => this.RaiseAndSetIfChanged(ref _navLabel, value);
        }

        public bool ScenarioPitch
        {
            get => _scenarioPitch;
            set => this.RaiseAndSetIfChanged(ref _scenarioPitch, value);
        }
        public bool ScenarioInput1
        {
            get => _scenarioInput1;
            set => this.RaiseAndSetIfChanged(ref _scenarioInput1, value);
        }
        public bool ScenarioInput2
        {
            get => _scenarioInput2;
            set => this.RaiseAndSetIfChanged(ref _scenarioInput2, value);
        }
        public bool ScenarioInput3
        {
            get => _scenarioInput3;
            set => this.RaiseAndSetIfChanged(ref _scenarioInput3, value);
        }
        public bool ScenarioInput4
        {
            get => _scenarioInput4;
            set => this.RaiseAndSetIfChanged(ref _scenarioInput4, value);
        }
        public string ScenarioTxtBx1
        {
            get => _scenarioTxtBx1;
            set => this.RaiseAndSetIfChanged(ref _scenarioTxtBx1, value);
        }
        public string ScenarioTxtBx2
        {
            get => _scenarioTxtBx2;
            set => this.RaiseAndSetIfChanged(ref _scenarioTxtBx2, value);
        }
        public string ScenarioTxtBx3
        {
            get => _scenarioTxtBx3;
            set => this.RaiseAndSetIfChanged(ref _scenarioTxtBx3, value);
        }
        public string ScenarioTxtBx4
        {
            get => _scenarioTxtBx4;
            set => this.RaiseAndSetIfChanged(ref _scenarioTxtBx4, value);
        }
        public string FloorPlanLeft
        {
            get => _floorPlanLeft;
            set => this.RaiseAndSetIfChanged(ref _floorPlanLeft, value);
        }
        public string FloorPlanRight
        {
            get => _floorPlanRight;
            set => this.RaiseAndSetIfChanged(ref _floorPlanRight, value);
        }
        public string FloorPlanFront
        {
            get => _floorPlanFront;
            set => this.RaiseAndSetIfChanged(ref _floorPlanFront, value);
        }
        public string Overhang
        {
            get => _overhang;
            set => this.RaiseAndSetIfChanged(ref _overhang, value);
        }

        public bool PanelThicknessEnable
        {
            get => _panelThicknessEnable;
            set => this.RaiseAndSetIfChanged(ref _panelThicknessEnable, value);
        }

        public bool NavBtn1Enable
        {
            get => _navBtn1Enable;
            set => this.RaiseAndSetIfChanged(ref _navBtn1Enable, value);
        }
        public bool NavBtn2Enable
        {
            get => _navBtn2Enable;
            set => this.RaiseAndSetIfChanged(ref _navBtn2Enable, value);
        }

        public bool CalcBtnEnable
        {
            get => _calcBtnEnable;
            set => this.RaiseAndSetIfChanged(ref _calcBtnEnable, value);
        }

        public List<PanelThicknessModel> PanelThickness
        {
            get => _panelThicknessModels;
            set => this.RaiseAndSetIfChanged(ref _panelThicknessModels, value);
        }
        public PanelThicknessModel SelectedThickness
        {
            get => _selectedThickness;
            set => this.RaiseAndSetIfChanged(ref _selectedThickness, value);
        }
        public ComboBoxItem SelectedPanelWidth
        {
            get => _selectedPanelWidth;
            set => this.RaiseAndSetIfChanged(ref _selectedPanelWidth, value);
        }

        public UserControl SelectedViewModel
        {
            get => _selectedViewModel;
            set => this.RaiseAndSetIfChanged(ref _selectedViewModel, value);
        }

        public MainViewModel()
        {
            DiagramImage.Source = _sunroomResources.SunroomDefault;
            DiagramImage.Height = 300;
            DiagramImage.Width = 400;
            SelectedViewModel = _sunroomResources.InputDefaultVM;
            NavBtn2Enable = true;
            
            StudioStyle = ReactiveCommand.Create(() => OnSunroomStyleChange(0));
            GableStyle = ReactiveCommand.Create(() => OnSunroomStyleChange(1));
            
            NavBtn1 = ReactiveCommand.Create(() => OnNavigationChange(-1));
            NavBtn2 = ReactiveCommand.Create(() => OnNavigationChange(1));

            Scenario1Select = ReactiveCommand.Create(() => OnScenarioSelectChange(1));
            Scenario2Select = ReactiveCommand.Create(() => OnScenarioSelectChange(2));
            Scenario3Select = ReactiveCommand.Create(() => OnScenarioSelectChange(3));
            Scenario4Select = ReactiveCommand.Create(() => OnScenarioSelectChange(4));
            Scenario5Select = ReactiveCommand.Create(() => OnScenarioSelectChange(5));
            Scenario6Select = ReactiveCommand.Create(() => OnScenarioSelectChange(6));
            Scenario7Select = ReactiveCommand.Create(() => OnScenarioSelectChange(7));
            PitchUnit1 = ReactiveCommand.Create(() => OnPitchUnit(0));
            PitchUnit2 = ReactiveCommand.Create(() => OnPitchUnit(1));
            PanelTypeSelect1 = ReactiveCommand.Create(()=> OnPanelTypeChange(0));
            PanelTypeSelect2 = ReactiveCommand.Create(()=> OnPanelTypeChange(1));
            PanelTypeSelect3 = ReactiveCommand.Create(()=> OnPanelTypeChange(2));
            EndCutSelect1 = ReactiveCommand.Create(() => OnEndCutSelectChange(0));
            EndCutSelect2 = ReactiveCommand.Create(() => OnEndCutSelectChange(1));
            EndCutSelect3 = ReactiveCommand.Create(() => OnEndCutSelectChange(2));
            CalculateBtn = ReactiveCommand.Create(() => OnCalculateBTN());
        }
        private void OnNavigationChange(int direction)
        {
            _navigation += direction;
            switch (_navigation)
            {
                case 0:
                    SelectedViewModel = _sunroomResources.InputDefaultVM;
                    DiagramImage.Source = _sunroomResources.SunroomDefault;
                    NavLabel = "Start Here";
                    break;
                case 1:
                    SelectedViewModel = _sunroomResources.Input1VM;
                    NavLabel = "Sunroom Type";
                    break;
                case 2:
                    SelectedViewModel = _sunroomResources.Input2VM;
                    NavLabel = "Scenario Selection";
                    break;
                case 3:
                    SelectedViewModel = _sunroomResources.Input3VM;
                    DiagramImage.Source = _sunroomResources.SunroomFloorPlan;
                    NavLabel = "Floor Plan";
                    break;
                case 4:
                    SelectedViewModel = _sunroomResources.Input4VM;
                    DiagramImage.Source = _sunroomResources.SunroomOverhang;
                    NavLabel = "Roof Info";
                    break;
                case 5:
                    SelectedViewModel = _sunroomResources.Input5VM;
                    DiagramImage.Source = _sunroomResources.SunroomPlumCut;
                    NavLabel = "Panel Info";
                    break;
                case 6:
                    // OnCalculateBTN();
                    break;
            }
            NavBtn1Enable = true;
            NavBtn2Enable = true;
            CalcBtnEnable = false;
            if (_navigation == 0)
            {
                NavBtn1Enable = false;
            }
            if (_navigation == 5)
            {
                NavBtn2Enable = false;
                CalcBtnEnable = true;
            }
        }

        private void OnCalculateBTN()
        {
            string endCut = null, panelWidth = (string)SelectedPanelWidth.Content;
            switch (_endCut)
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
                SelectedThickness.ComboValue, _sunroomScenario);
            sunroom.CalculateSunroom(ScenarioTxtBx1, ScenarioTxtBx2, ScenarioTxtBx3, 
                ScenarioTxtBx4, _pitchUnits, _sunroomStyle);
            _test = true;
        }
        private void OnPitchUnit(int unit)
        {
            _pitchUnits = unit;
        }
        private void OnSunroomStyleChange(int style)
        {
            switch (style)
            {
                case 0:
                    DiagramImage.Source = _sunroomResources.SunroomStudio;
                    _sunroomStyle = style;
                    break;
                case 1:
                    DiagramImage.Source = _sunroomResources.SunroomGable;
                    _sunroomStyle = style;
                    break;
                default:
                    DiagramImage.Source = _sunroomResources.SunroomDefault;
                    break;
            }
        }

        private void OnEndCutSelectChange(int endCut)
        {
            switch (endCut)
            {
                case 0:
                    DiagramImage.Source = _sunroomResources.SunroomPlumCut;
                    break;
                case 1:
                    DiagramImage.Source = _sunroomResources.SunroomPlumCutTop;
                    break;
                case 2:
                    DiagramImage.Source = _sunroomResources.SunroomSquareCut;
                    break;
            }
            _endCut = endCut;
        }

        private void OnScenarioSelectChange(int scenario)
        {
            switch (_sunroomStyle)
            {
                case 0:
                    switch (scenario)
                    {
                        case 1:
                            DiagramImage.Source = _sunroomResources.StudioWallPitch;
                            _sunroomScenario = scenario;
                            InputChange();
                            break;
                        case 2:
                            DiagramImage.Source = _sunroomResources.StudioWallAttached;
                            _sunroomScenario = scenario;
                            InputChange();
                            break;
                        case 3:
                            DiagramImage.Source = _sunroomResources.StudioMaxPitch;
                            _sunroomScenario = scenario;
                            InputChange();
                            break;
                        case 4:
                            DiagramImage.Source = _sunroomResources.StudioSoffitPitch;
                            _sunroomScenario = scenario;
                            InputChange();
                            break;
                        case 5:
                            DiagramImage.Source = _sunroomResources.StudioSoffitAttached;
                            _sunroomScenario = scenario;
                            InputChange();
                            break;
                        case 6:
                            DiagramImage.Source = _sunroomResources.StudioDripEdgePitch;
                            _sunroomScenario = scenario;
                            InputChange();
                            break;
                        case 7:
                            DiagramImage.Source = _sunroomResources.StudioDripEdgeAttached;
                            _sunroomScenario = scenario;
                            InputChange();
                            break;
                        default:
                            DiagramImage.Source = _sunroomResources.SunroomDefault;
                            break;
                    }
                    break;
                case 1:
                    switch (scenario)
                    {
                        case 1:
                            DiagramImage.Source = _sunroomResources.GableWallPitch;
                            _sunroomScenario = scenario;
                            InputChange();
                            break;
                        case 2:
                            DiagramImage.Source = _sunroomResources.GableWallAttached;
                            _sunroomScenario = scenario;
                            InputChange();
                            break;
                        case 3:
                            DiagramImage.Source = _sunroomResources.GableMaxPitch;
                            _sunroomScenario = scenario;
                            InputChange();
                            break;
                        case 4:
                            DiagramImage.Source = _sunroomResources.GableSoffitPitch;
                            _sunroomScenario = scenario;
                            InputChange();
                            break;
                        case 5:
                            DiagramImage.Source = _sunroomResources.GableSoffitAttached;
                            _sunroomScenario = scenario;
                            InputChange();
                            break;
                        case 6:
                            DiagramImage.Source = _sunroomResources.GableDripEdgePitch;
                            _sunroomScenario = scenario;
                            InputChange();
                            break;
                        case 7:
                            DiagramImage.Source = _sunroomResources.GableDripEdgeAttached;
                            _sunroomScenario = scenario;
                            InputChange();
                            break;
                        default:
                            DiagramImage.Source = _sunroomResources.SunroomDefault;
                            break;
                    }
                    break;
            }
        }

        private void InputChange()
        {
            switch (_sunroomStyle)
            {
                case 0: // Studio
                    ScenarioInput1 = true;
                    ScenarioInput2 = true;
                    ScenarioInput3 = false;
                    ScenarioInput4 = false;
                    switch (_sunroomScenario)
                    {
                        case 1: // Wall Height and Pitch
                            ScenarioPitch = true;
                            ScenarioLabel1 = "Wall Height";
                            ScenarioWatermark1 = "0' or 0in";
                            ScenarioLabel2 = "Pitch";
                            ScenarioWatermark2 = "0in or 0deg";
                            break;
                        case 2: // Wall Height and Attached Height
                            ScenarioPitch = false;
                            ScenarioLabel1 = "Wall Height";
                            ScenarioWatermark1 = "0' or 0in";
                            ScenarioLabel2 = "Attached Height";
                            ScenarioWatermark2 = "0in or 0in";
                            break;
                        case 3: // Max Height and Pitch
                            ScenarioPitch = true;
                            ScenarioLabel1 = "Max Height";
                            ScenarioWatermark1 = "0' or 0in";
                            ScenarioLabel2 = "Pitch";
                            ScenarioWatermark2 = "0in or 0deg";
                            break;
                        case 4: // Soffit Height and Pitch
                            ScenarioPitch = true;
                            ScenarioLabel1 = "Soffit Height";
                            ScenarioWatermark1 = "0' or 0in";
                            ScenarioLabel2 = "Pitch";
                            ScenarioWatermark2 = "0in or 0deg";
                            break;
                        case 5: // Soffit Height and Attached Height
                            ScenarioPitch = false;
                            ScenarioLabel1 = "Soffit Height";
                            ScenarioWatermark1 = "0' or 0in";
                            ScenarioLabel2 = "Attached Height";
                            ScenarioWatermark2 = "0' or 0in";
                            break;
                        case 6: // Drip Edge and Pitch
                            ScenarioPitch = true;
                            ScenarioLabel1 = "Drip Edge";
                            ScenarioWatermark1 = "0' or 0in";
                            ScenarioLabel2 = "Pitch";
                            ScenarioWatermark2 = "0in or 0deg";
                            break;
                        case 7: // Drip Edge and Attached height
                            ScenarioPitch = false;
                            ScenarioLabel1 = "Drip Edge";
                            ScenarioWatermark1 = "0' or 0in";
                            ScenarioLabel2 = "Attached Height";
                            ScenarioWatermark2 = "0' or 0in";
                            break;
                    }
                    break;
                case 1: // Gable
                    ScenarioInput1 = true;
                    ScenarioInput2 = true;
                    ScenarioInput3 = true;
                    ScenarioInput4 = true;
                    switch (_sunroomScenario)
                    {
                        case 1: // Wall Height and Pitch
                            ScenarioPitch = true;
                            ScenarioLabel1 = "Left Wall Height";
                            ScenarioWatermark1 = "0' or 0in";
                            ScenarioLabel2 = "Left Pitch";
                            ScenarioWatermark2 = "0in or 0deg";
                            ScenarioLabel3 = "Right Wall Height";
                            ScenarioWatermark3 = "0' or 0in";
                            ScenarioLabel4 = "Right Pitch";
                            ScenarioWatermark4 = "0in or 0deg";
                            break;
                        case 2: // Wall Height and Attached Height
                            ScenarioPitch = false;
                            ScenarioLabel1 = "Left Wall Height";
                            ScenarioWatermark1 = "0' or 0in";
                            ScenarioLabel2 = "Attached Height";
                            ScenarioWatermark2 = "0in or 0in";
                            ScenarioLabel3 = "Right Wall Height";
                            ScenarioWatermark3 = "0' or 0in";
                            ScenarioInput4 = false;
                            break;
                        case 3: // Max Height and Pitch
                            ScenarioPitch = true;
                            ScenarioLabel1 = "Max Height";
                            ScenarioWatermark1 = "0' or 0in";
                            ScenarioLabel2 = "Left Pitch";
                            ScenarioWatermark2 = "0in or 0deg";
                            ScenarioInput3 = false;
                            ScenarioLabel4 = "Right Pitch";
                            ScenarioWatermark4 = "0in or 0deg";
                            break;
                        case 4: // Soffit Height and Pitch
                            ScenarioPitch = true;
                            ScenarioLabel1 = "Left Soffit Height";
                            ScenarioWatermark1 = "0' or 0in";
                            ScenarioLabel2 = "Left Pitch";
                            ScenarioWatermark2 = "0in or 0deg";
                            ScenarioLabel3 = "Right Soffit Height";
                            ScenarioWatermark3 = "0' or 0in";
                            ScenarioLabel4 = "Right Pitch";
                            ScenarioWatermark4 = "0in or 0deg";
                            break;
                        case 5: // Soffit Height and Attached Height
                            ScenarioPitch = false;
                            ScenarioLabel1 = "Left Soffit Height";
                            ScenarioWatermark1 = "0' or 0in";
                            ScenarioLabel2 = "Attached Height";
                            ScenarioWatermark2 = "0' or 0in";
                            ScenarioLabel3 = "Right Soffit Height";
                            ScenarioWatermark3 = "0' or 0in";
                            ScenarioInput4 = false;
                            break;
                        case 6: // Drip Edge and Pitch
                            ScenarioPitch = true;
                            ScenarioLabel1 = "Left Drip Edge";
                            ScenarioWatermark1 = "0' or 0in";
                            ScenarioLabel2 = "Left Pitch";
                            ScenarioWatermark2 = "0in or 0deg";
                            ScenarioLabel3 = "Right Drip Edge";
                            ScenarioWatermark3 = "0' or 0in";
                            ScenarioLabel4 = "Right Pitch";
                            ScenarioWatermark4 = "0in or 0deg";
                            break;
                        case 7: // Drip Edge and Attached height
                            ScenarioPitch = false;
                            ScenarioLabel1 = "Left Drip Edge";
                            ScenarioWatermark1 = "0' or 0in";
                            ScenarioLabel2 = "Attached Height";
                            ScenarioWatermark2 = "0' or 0in";
                            ScenarioLabel3 = "Right Drip Edge";
                            ScenarioWatermark3 = "0' or 0in";
                            ScenarioInput4 = false;
                            break;
                    }
                    break;
            }
        }

        private void OnPanelTypeChange(int type)
        {
            switch (type)
            {
                case 0:
                    PanelThickness = _panelThicknessCombo.CreateList(PanelThicknessModel.Foam1);
                    PanelThicknessEnable = true;
                    break;
                case 1:
                    PanelThickness = _panelThicknessCombo.CreateList(PanelThicknessModel.Foam2);
                    PanelThicknessEnable = true;
                    break;
                case 2:
                    PanelThickness = _panelThicknessCombo.CreateList(PanelThicknessModel.Aluminum);
                    PanelThicknessEnable = true;
                    break;
            }
        }
    }
}