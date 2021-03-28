using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;
using SunroomCalculatorAvalonia.Models;
using Splat;

namespace SunroomCalculatorAvalonia.ViewModels
{
    public class EndCutViewModel : ViewModelBase
    {
        private Image _diagramImage = new();
        private DiagramModel _diagramModel;
        private ResultsModel _resultsModel;
        
        public ReactiveCommand<Unit, Unit> EndCutSelect1 { get; }
        public ReactiveCommand<Unit, Unit> EndCutSelect2 { get; }
        public ReactiveCommand<Unit, Unit> EndCutSelect3 { get; }
        public Image DiagramImage => _diagramImage;

        public EndCutViewModel()
        {
            _diagramModel = (DiagramModel)Locator.Current.GetService(typeof(DiagramModel));
            _resultsModel = (ResultsModel)Locator.Current.GetService(typeof(IResultsModel));
            _diagramImage.Source = _diagramModel.SunroomPlumCut;
            EndCutSelect1 = ReactiveCommand.Create(() => OnEndCutSelectChange(0));
            EndCutSelect2 = ReactiveCommand.Create(() => OnEndCutSelectChange(1));
            EndCutSelect3 = ReactiveCommand.Create(() => OnEndCutSelectChange(2));
        }
        private void OnEndCutSelectChange(int endCut)
        {
            switch (endCut)
            {
                case 0:
                    DiagramImage.Source = _diagramModel.SunroomPlumCut;
                    break;
                case 1:
                    DiagramImage.Source = _diagramModel.SunroomPlumCutTop;
                    break;
                case 2:
                    DiagramImage.Source = _diagramModel.SunroomSquareCut;
                    break;
            }
            _resultsModel.EndCut = endCut;
        }
    }
}