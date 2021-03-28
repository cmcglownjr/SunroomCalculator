using System.Collections.Generic;
using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;
using SunroomCalculatorAvalonia.Models;
using Splat;

namespace SunroomCalculatorAvalonia.ViewModels
{
    public class PanelViewModel : ViewModelBase
    {
        private Image _diagramImage = new();
        private DiagramModel _diagramModel;
        private ResultsModel _resultsModel;
        private bool _panelThicknessEnable;
        public ReactiveCommand<Unit, Unit> PanelTypeSelect1 { get; }
        public ReactiveCommand<Unit, Unit> PanelTypeSelect2 { get; }
        public ReactiveCommand<Unit, Unit> PanelTypeSelect3 { get; }
        public Image DiagramImage => _diagramImage;
        public List<PanelThicknessModel> PanelThickness
        {
            get => _resultsModel.PanelThicknessModels;
            set => this.RaiseAndSetIfChanged(ref _resultsModel.PanelThicknessModels, value);
        }
        public PanelThicknessModel SelectedThickness
        {
            get => _resultsModel.SelectedThickness;
            set => this.RaiseAndSetIfChanged(ref _resultsModel.SelectedThickness, value);
        }
        public ComboBoxItem SelectedPanelWidth
        {
            get => _resultsModel.SelectedPanelWidth;
            set => this.RaiseAndSetIfChanged(ref _resultsModel.SelectedPanelWidth, value);
        }
        public string Overhang
        {
            get => _resultsModel.Overhang;
            set => this.RaiseAndSetIfChanged(ref _resultsModel.Overhang, value);
        }
        public bool PanelThicknessEnable
        {
            get => _panelThicknessEnable;
            set => this.RaiseAndSetIfChanged(ref _panelThicknessEnable, value);
        }

        public PanelViewModel()
        {
            _diagramModel = (DiagramModel)Locator.Current.GetService(typeof(DiagramModel));
            _resultsModel = (ResultsModel)Locator.Current.GetService(typeof(IResultsModel));
            _diagramImage.Source = _diagramModel.SunroomOverhang;
            PanelTypeSelect1 = ReactiveCommand.Create(()=> OnPanelTypeChange(0));
            PanelTypeSelect2 = ReactiveCommand.Create(()=> OnPanelTypeChange(1));
            PanelTypeSelect3 = ReactiveCommand.Create(()=> OnPanelTypeChange(2));
        }
        private void OnPanelTypeChange(int type)
        {
            switch (type)
            {
                case 0:
                    PanelThickness = _resultsModel.PanelThicknessCombo.CreateList(PanelThicknessModel.Foam1);
                    PanelThicknessEnable = true;
                    break;
                case 1:
                    PanelThickness = _resultsModel.PanelThicknessCombo.CreateList(PanelThicknessModel.Foam2);
                    PanelThicknessEnable = true;
                    break;
                case 2:
                    PanelThickness = _resultsModel.PanelThicknessCombo.CreateList(PanelThicknessModel.Aluminum);
                    PanelThicknessEnable = true;
                    break;
            }
        }
    }
}