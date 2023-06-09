using System.Reactive;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ReactiveUI;
using SunroomCalculatorAvalonia.Models;
using Splat;

namespace SunroomCalculatorAvalonia.ViewModels
{
    public class FloorPlanViewModel : ViewModelBase
    {
        private Image _diagramImage = new();
        private DiagramModel _diagramModel;
        private ResultsModel _resultsModel;
        private string _floorPlanLeft, _floorPlanRight, _floorPlanFront;
        public ReactiveCommand<string, Unit> InputCheck { get; }
        public Image DiagramImage => _diagramImage;
        public string FloorPlanLeft
        {
            get => _resultsModel.FloorPlanLeft;
            set => this.RaiseAndSetIfChanged(ref _resultsModel.FloorPlanLeft, value);
        }
        public string FloorPlanRight
        {
            get => _resultsModel.FloorPlanRight;
            set => this.RaiseAndSetIfChanged(ref _resultsModel.FloorPlanRight, value);
        }
        public string FloorPlanFront
        {
            get => _resultsModel.FloorPlanFront;
            set => this.RaiseAndSetIfChanged(ref _resultsModel.FloorPlanFront, value);
        }

        public FloorPlanViewModel()
        {
            _diagramModel = (DiagramModel)Locator.Current.GetService(typeof(DiagramModel));
            _resultsModel = (ResultsModel)Locator.Current.GetService(typeof(IResultsModel));
            InputCheck = ReactiveCommand.Create<string>(stringIn => OnLostFocusTextBox(stringIn));
            _diagramImage.Source = _diagramModel.SunroomFloorPlan;
        }
        private void OnLostFocusTextBox(string input)
        {
            input = _floorPlanFront;
            SunroomMessageBox.SunroomMessageBoxDialog("Error", input);
        }
    }
}