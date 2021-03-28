using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;
using SunroomCalculatorAvalonia.Models;
using Splat;

namespace SunroomCalculatorAvalonia.ViewModels
{
    public class ScenarioViewModel : ViewModelBase
    {
        private string _scenarioLabel1, _scenarioLabel2, _scenarioLabel3, _scenarioLabel4;
        private string _scenarioWatermark1, _scenarioWatermark2, _scenarioWatermark3, _scenarioWatermark4;
        private bool _scenarioPitch, _scenarioInput1, _scenarioInput2, _scenarioInput3, _scenarioInput4;
        private Image _diagramImage = new();
        private DiagramModel _diagramModel;
        private ResultsModel _resultsModel;
        public ReactiveCommand<Unit, Unit> Scenario1Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario2Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario3Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario4Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario5Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario6Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario7Select { get; }
        public ReactiveCommand<Unit, Unit> PitchUnit1 { get; }
        public ReactiveCommand<Unit, Unit> PitchUnit2 { get; }
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
            set => this.RaiseAndSetIfChanged(ref _scenarioLabel3, value);
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
            get => _resultsModel.ScenarioTxtBx1;
            set => this.RaiseAndSetIfChanged(ref _resultsModel.ScenarioTxtBx1, value);
        }
        public string ScenarioTxtBx2
        {
            get => _resultsModel.ScenarioTxtBx2;
            set => this.RaiseAndSetIfChanged(ref _resultsModel.ScenarioTxtBx2, value);
        }
        public string ScenarioTxtBx3
        {
            get => _resultsModel.ScenarioTxtBx3;
            set => this.RaiseAndSetIfChanged(ref _resultsModel.ScenarioTxtBx3, value);
        }
        public string ScenarioTxtBx4
        {
            get => _resultsModel.ScenarioTxtBx4;
            set => this.RaiseAndSetIfChanged(ref _resultsModel.ScenarioTxtBx4, value);
        }

        public ScenarioViewModel()
        {
            _diagramModel = (DiagramModel)Locator.Current.GetService(typeof(DiagramModel));
            _resultsModel = (ResultsModel)Locator.Current.GetService(typeof(IResultsModel));
            Scenario1Select = ReactiveCommand.Create(() => OnScenarioSelectChange(1));
            Scenario2Select = ReactiveCommand.Create(() => OnScenarioSelectChange(2));
            Scenario3Select = ReactiveCommand.Create(() => OnScenarioSelectChange(3));
            Scenario4Select = ReactiveCommand.Create(() => OnScenarioSelectChange(4));
            Scenario5Select = ReactiveCommand.Create(() => OnScenarioSelectChange(5));
            Scenario6Select = ReactiveCommand.Create(() => OnScenarioSelectChange(6));
            Scenario7Select = ReactiveCommand.Create(() => OnScenarioSelectChange(7));
            PitchUnit1 = ReactiveCommand.Create(() => OnPitchUnit(0));
            PitchUnit2 = ReactiveCommand.Create(() => OnPitchUnit(1));
        }
        private void OnPitchUnit(int unit)
        {
            _resultsModel.PitchUnits = unit;
        }

        private void OnScenarioSelectChange(int scenario)
        {
            switch (_resultsModel.SunroomStyle)
            {
                case 0:
                    switch (scenario)
                    {
                        case 1:
                            DiagramImage.Source = _diagramModel.StudioWallPitch;
                            _resultsModel.SunroomScenario = scenario;
                            InputChange();
                            break;
                        case 2:
                            DiagramImage.Source = _diagramModel.StudioWallAttached;
                            _resultsModel.SunroomScenario = scenario;
                            InputChange();
                            break;
                        case 3:
                            DiagramImage.Source = _diagramModel.StudioMaxPitch;
                            _resultsModel.SunroomScenario = scenario;
                            InputChange();
                            break;
                        case 4:
                            DiagramImage.Source = _diagramModel.StudioSoffitPitch;
                            _resultsModel.SunroomScenario = scenario;
                            InputChange();
                            break;
                        case 5:
                            DiagramImage.Source = _diagramModel.StudioSoffitAttached;
                            _resultsModel.SunroomScenario = scenario;
                            InputChange();
                            break;
                        case 6:
                            DiagramImage.Source = _diagramModel.StudioDripEdgePitch;
                            _resultsModel.SunroomScenario = scenario;
                            InputChange();
                            break;
                        case 7:
                            DiagramImage.Source = _diagramModel.StudioDripEdgeAttached;
                            _resultsModel.SunroomScenario = scenario;
                            InputChange();
                            break;
                        default:
                            DiagramImage.Source = _diagramModel.SunroomDefault;
                            break;
                    }
                    break;
                case 1:
                    switch (scenario)
                    {
                        case 1:
                            DiagramImage.Source = _diagramModel.GableWallPitch;
                            _resultsModel.SunroomScenario = scenario;
                            InputChange();
                            break;
                        case 2:
                            DiagramImage.Source = _diagramModel.GableWallAttached;
                            _resultsModel.SunroomScenario = scenario;
                            InputChange();
                            break;
                        case 3:
                            DiagramImage.Source = _diagramModel.GableMaxPitch;
                            _resultsModel.SunroomScenario = scenario;
                            InputChange();
                            break;
                        case 4:
                            DiagramImage.Source = _diagramModel.GableSoffitPitch;
                            _resultsModel.SunroomScenario = scenario;
                            InputChange();
                            break;
                        case 5:
                            DiagramImage.Source = _diagramModel.GableSoffitAttached;
                            _resultsModel.SunroomScenario = scenario;
                            InputChange();
                            break;
                        case 6:
                            DiagramImage.Source = _diagramModel.GableDripEdgePitch;
                            _resultsModel.SunroomScenario = scenario;
                            InputChange();
                            break;
                        case 7:
                            DiagramImage.Source = _diagramModel.GableDripEdgeAttached;
                            _resultsModel.SunroomScenario = scenario;
                            InputChange();
                            break;
                        default:
                            DiagramImage.Source = _diagramModel.SunroomDefault;
                            break;
                    }
                    break;
            }
        }
        private void InputChange()
        {
            switch (_resultsModel.SunroomStyle)
            {
                case 0: // Studio
                    ScenarioInput1 = true;
                    ScenarioInput2 = true;
                    ScenarioInput3 = false;
                    ScenarioInput4 = false;
                    switch (_resultsModel.SunroomScenario)
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
                    switch (_resultsModel.SunroomScenario)
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