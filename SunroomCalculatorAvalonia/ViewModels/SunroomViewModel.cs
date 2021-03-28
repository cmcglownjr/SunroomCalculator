using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;
using SunroomCalculatorAvalonia.Models;
using Splat;

namespace SunroomCalculatorAvalonia.ViewModels
{
    public class SunroomViewModel
    {
        private Image _diagramImage = new();
        private DiagramModel _diagramModel;
        private ResultsModel _resultsModel;
        private int _sunroomStyle; // Default to Studio(0), Gable is (1)
        public ReactiveCommand<Unit, Unit> StudioStyle { get; }
        public ReactiveCommand<Unit, Unit> GableStyle { get; }
        public Image DiagramImage => _diagramImage;

        public SunroomViewModel()
        {
            _diagramModel = (DiagramModel)Locator.Current.GetService(typeof(DiagramModel));
            _resultsModel = (ResultsModel)Locator.Current.GetService(typeof(IResultsModel));
            StudioStyle = ReactiveCommand.Create(() => OnSunroomStyleChange(0));
            GableStyle = ReactiveCommand.Create(() => OnSunroomStyleChange(1));
        }
        private void OnSunroomStyleChange(int style)
        {
            switch (style)
            {
                case 0:
                    DiagramImage.Source = _diagramModel.SunroomStudio;
                    _resultsModel.SunroomStyle = style;
                    break;
                case 1:
                    DiagramImage.Source = _diagramModel.SunroomGable;
                    _resultsModel.SunroomStyle = style;
                    break;
                default:
                    DiagramImage.Source = _diagramModel.SunroomDefault;
                    break;
            }
        }
    }
}