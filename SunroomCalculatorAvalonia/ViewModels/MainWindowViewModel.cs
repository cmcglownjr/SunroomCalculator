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
using JetBrains.Annotations;
using ReactiveUI;
using SunroomCalculatorAvalonia.Views;
using SunroomCalculatorAvalonia.Models;

namespace SunroomCalculatorAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private UserControl _selectedViewModel;
        private Image _diagramImage = new();
        private int _sunroomStyle; // Default to Studio(0), Gable is (1)
        private int _sunroomScenario;
        private string _scenarioLabel1, _scenarioLabel2, _scenarioLabel3, _scenarioLabel4;
        private string _scenarioWatermark1, _scenarioWatermark2, _scenarioWatermark3, _scenarioWatermark4;
        private bool _scenarioPitch, _scenarioInput1, _scenarioInput2, _scenarioInput3, _scenarioInput4;
        private readonly SunroomResources _sunroomResources = new();
        public Image DiagramImage
        {
            get => _diagramImage;
            set => _diagramImage = value;
        }
        public ReactiveCommand<Unit, Unit> StudioStyle { get; }
        public ReactiveCommand<Unit, Unit> GableStyle { get; }
        public ReactiveCommand<Unit, Unit> RadioNavSunroomType { get; }
        public ReactiveCommand<Unit, Unit> RadioNavScenario { get; }
        public ReactiveCommand<Unit, Unit> RadioNavFloorPlan { get; }
        public ReactiveCommand<Unit, Unit> RadioNavRoofInfo { get; }
        public ReactiveCommand<Unit, Unit> RadioNavRoofPanel { get; }
        public ReactiveCommand<Unit, Unit> Scenario1Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario2Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario3Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario4Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario5Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario6Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario7Select { get; }
        public ReactiveCommand<Unit, Unit> EndCutSelect1 { get; }
        public ReactiveCommand<Unit, Unit> EndCutSelect2 { get; }
        public ReactiveCommand<Unit, Unit> EndCutSelect3 { get; }

        [NotNull]
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

        [NotNull]
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
        }public bool ScenarioInput3
        {
            get => _scenarioInput3;
            set => this.RaiseAndSetIfChanged(ref _scenarioInput3, value);
        }public bool ScenarioInput4
        {
            get => _scenarioInput4;
            set => this.RaiseAndSetIfChanged(ref _scenarioInput4, value);
        }

        public UserControl SelectedViewModel
        {
            get => _selectedViewModel;
            set => this.RaiseAndSetIfChanged(ref _selectedViewModel, value);
        }

        public MainWindowViewModel()
        {
            DiagramImage.Source = _sunroomResources.SunroomDefault;
            DiagramImage.Height = 300;
            DiagramImage.Width = 400;
            SelectedViewModel = _sunroomResources.InputDefaultVM;
            StudioStyle = ReactiveCommand.Create(() => SunroomStyleChange(0));
            GableStyle = ReactiveCommand.Create(() => SunroomStyleChange(1));
            RadioNavSunroomType = ReactiveCommand.Create(() => NavigationChange(1));
            RadioNavScenario = ReactiveCommand.Create(() => NavigationChange(2));
            RadioNavFloorPlan = ReactiveCommand.Create(() => NavigationChange(3));
            RadioNavRoofInfo = ReactiveCommand.Create(() => NavigationChange(4));
            RadioNavRoofPanel = ReactiveCommand.Create(() => NavigationChange(5));
            Scenario1Select = ReactiveCommand.Create(() => ScenarioSelectChange(1));
            Scenario2Select = ReactiveCommand.Create(() => ScenarioSelectChange(2));
            Scenario3Select = ReactiveCommand.Create(() => ScenarioSelectChange(3));
            Scenario4Select = ReactiveCommand.Create(() => ScenarioSelectChange(4));
            Scenario5Select = ReactiveCommand.Create(() => ScenarioSelectChange(5));
            Scenario6Select = ReactiveCommand.Create(() => ScenarioSelectChange(6));
            Scenario7Select = ReactiveCommand.Create(() => ScenarioSelectChange(7));
            EndCutSelect1 = ReactiveCommand.Create(() => EndCutSelectChange(0));
            EndCutSelect2 = ReactiveCommand.Create(() => EndCutSelectChange(1));
            EndCutSelect3 = ReactiveCommand.Create(() => EndCutSelectChange(2));
        }
        
        private void NavigationChange(int navigation)
        {
            switch (navigation)
            {
                case 1:
                    SelectedViewModel = _sunroomResources.Input1VM;
                    break;
                case 2:
                    SelectedViewModel = _sunroomResources.Input2VM;
                    break;
                case 3:
                    SelectedViewModel = _sunroomResources.Input3VM;
                    DiagramImage.Source = _sunroomResources.SunroomFloorPlan;
                    break;
                case 4:
                    SelectedViewModel = _sunroomResources.Input4VM;
                    DiagramImage.Source = _sunroomResources.SunroomOverhang;
                    break;
                case 5:
                    SelectedViewModel = _sunroomResources.Input5VM;
                    DiagramImage.Source = _sunroomResources.SunroomPlumCut;
                    break;
                default:
                    SelectedViewModel = _sunroomResources.InputDefaultVM;
                    break;
            }
        }
        private void SunroomStyleChange(int style)
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

        private void EndCutSelectChange(int endCut)
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
        }

        private void ScenarioSelectChange(int scenario)
        {
            switch (_sunroomStyle)
            {
                case 0:
                    switch (scenario)
                    {
                        case 1:
                            DiagramImage.Source = _sunroomResources.StudioWallPitch;
                            _sunroomScenario = scenario;
                            ScenarioInputChange();
                            break;
                        case 2:
                            DiagramImage.Source = _sunroomResources.StudioWallAttached;
                            _sunroomScenario = scenario;
                            ScenarioInputChange();
                            break;
                        case 3:
                            DiagramImage.Source = _sunroomResources.StudioMaxPitch;
                            _sunroomScenario = scenario;
                            ScenarioInputChange();
                            break;
                        case 4:
                            DiagramImage.Source = _sunroomResources.StudioSoffitPitch;
                            _sunroomScenario = scenario;
                            ScenarioInputChange();
                            break;
                        case 5:
                            DiagramImage.Source = _sunroomResources.StudioSoffitAttached;
                            _sunroomScenario = scenario;
                            ScenarioInputChange();
                            break;
                        case 6:
                            DiagramImage.Source = _sunroomResources.StudioDripEdgePitch;
                            _sunroomScenario = scenario;
                            ScenarioInputChange();
                            break;
                        case 7:
                            DiagramImage.Source = _sunroomResources.StudioDripEdgeAttached;
                            _sunroomScenario = scenario;
                            ScenarioInputChange();
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
                            ScenarioInputChange();
                            break;
                        case 2:
                            DiagramImage.Source = _sunroomResources.GableWallAttached;
                            _sunroomScenario = scenario;
                            ScenarioInputChange();
                            break;
                        case 3:
                            DiagramImage.Source = _sunroomResources.GableMaxPitch;
                            _sunroomScenario = scenario;
                            ScenarioInputChange();
                            break;
                        case 4:
                            DiagramImage.Source = _sunroomResources.GableSoffitPitch;
                            _sunroomScenario = scenario;
                            ScenarioInputChange();
                            break;
                        case 5:
                            DiagramImage.Source = _sunroomResources.GableSoffitAttached;
                            _sunroomScenario = scenario;
                            ScenarioInputChange();
                            break;
                        case 6:
                            DiagramImage.Source = _sunroomResources.GableDripEdgePitch;
                            _sunroomScenario = scenario;
                            ScenarioInputChange();
                            break;
                        case 7:
                            DiagramImage.Source = _sunroomResources.GableDripEdgeAttached;
                            _sunroomScenario = scenario;
                            ScenarioInputChange();
                            break;
                        default:
                            DiagramImage.Source = _sunroomResources.SunroomDefault;
                            break;
                    }
                    break;
            }
        }

        private void ScenarioInputChange()
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
    }
}