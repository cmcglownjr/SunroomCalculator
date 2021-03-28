using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;
using SunroomCalculatorAvalonia.Views;
using SunroomCalculatorAvalonia.Models;
using SL = SunroomLib;
using EU = SunroomLib.EngineeringUnits;
using Splat;

namespace SunroomCalculatorAvalonia.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private UserControl _selectedViewModel;
        private int _navigation;
        private string _navLabel = "Begin Here";
        private bool _navBtn1Enable, _navBtn2Enable, _calcBtnEnable;
        private string _pitchLabel, _pitchResults, _maxHeightLabel, _maxHeightResults, _soffitHeightLabel, 
            _soffitHeightResults, _dripEdgeLabel, _dripEdgeResults, _attachedHeightLabel, _attachedHeightResults,
            _panelLengthLabel, _panelLengthResults, _panelNumberLabel, _panelNumberResults, _roofAreaLabel, 
            _roofAreaResults;
        private readonly SunroomCalculatorViews _sunroomCalculatorViews = new();
        private ResultsModel _resultsModel;
        private ResultsView _resultsView;
        public ReactiveCommand<Unit, Unit> NavBtn1 { get; }
        public ReactiveCommand<Unit, Unit> NavBtn2 { get; }
        public ReactiveCommand<Unit, Unit> CalculateBtn { get; }
        public string NavLabel
        {
            get => _navLabel;
            set => this.RaiseAndSetIfChanged(ref _navLabel, value);
        }
        public string PitchLabel
        {
            get => _pitchLabel;
            set => this.RaiseAndSetIfChanged(ref _pitchLabel, value);
        }
        public string PitchResults
        {
            get => _pitchResults;
            set => this.RaiseAndSetIfChanged(ref _pitchResults, value);
        }
        public string MaxHeightLabel
        {
            get => _maxHeightLabel;
            set => this.RaiseAndSetIfChanged(ref _maxHeightLabel, value);
        }
        public string MaxHeightResults
        {
            get => _maxHeightResults;
            set => this.RaiseAndSetIfChanged(ref _maxHeightResults, value);
        }
        public string SoffitHeightLabel
        {
            get => _soffitHeightLabel;
            set => this.RaiseAndSetIfChanged(ref _soffitHeightLabel, value);
        }
        public string SoffitHeightResults
        {
            get => _soffitHeightResults;
            set => this.RaiseAndSetIfChanged(ref _soffitHeightResults, value);
        }
        public string DripEdgeLabel
        {
            get => _dripEdgeLabel;
            set => this.RaiseAndSetIfChanged(ref _dripEdgeLabel, value);
        }
        public string DripEdgeResults
        {
            get => _dripEdgeResults;
            set => this.RaiseAndSetIfChanged(ref _dripEdgeResults, value);
        }
        public string AttachedHeightLabel
        {
            get => _attachedHeightLabel;
            set => this.RaiseAndSetIfChanged(ref _attachedHeightLabel, value);
        }
        public string AttachedHeightResults
        {
            get => _attachedHeightResults;
            set => this.RaiseAndSetIfChanged(ref _attachedHeightResults, value);
        }
        public string PanelLengthLabel
        {
            get => _panelLengthLabel;
            set => this.RaiseAndSetIfChanged(ref _panelLengthLabel, value);
        }
        public string PanelLengthResults
        {
            get => _panelLengthResults;
            set => this.RaiseAndSetIfChanged(ref _panelLengthResults, value);
        }
        public string PanelNumberLabel
        {
            get => _panelNumberLabel;
            set => this.RaiseAndSetIfChanged(ref _panelNumberLabel, value);
        }
        public string PanelNumberResults
        {
            get => _panelNumberResults;
            set => this.RaiseAndSetIfChanged(ref _panelNumberResults, value);
        }
        public string RoofAreaLabel
        {
            get => _roofAreaLabel;
            set => this.RaiseAndSetIfChanged(ref _roofAreaLabel, value);
        }
        public string RoofAreaResults
        {
            get => _roofAreaResults;
            set => this.RaiseAndSetIfChanged(ref _roofAreaResults, value);
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
        public UserControl SelectedViewModel
        {
            get => _selectedViewModel;
            set => this.RaiseAndSetIfChanged(ref _selectedViewModel, value);
        }

        public MainViewModel()
        {
            SelectedViewModel = new InputDefaultView();
            NavBtn2Enable = true;
            _resultsModel = (ResultsModel)Locator.Current.GetService(typeof(IResultsModel));
            _resultsView = new ResultsView() {DataContext = this};
            NavBtn1 = ReactiveCommand.Create(() => OnNavigationChange(-1));
            NavBtn2 = ReactiveCommand.Create(() => OnNavigationChange(1));
            CalculateBtn = ReactiveCommand.Create(OnCalculateBTN);
        }
        private void OnNavigationChange(int direction)
        {
            _navigation += direction;
            switch (_navigation)
            {
                case 0:
                    SelectedViewModel = _sunroomCalculatorViews.InputDefaultView;
                    NavLabel = "Start Here";
                    break;
                case 1:
                    SelectedViewModel = _sunroomCalculatorViews.SunroomView;
                    NavLabel = "Sunroom Type";
                    break;
                case 2:
                    SelectedViewModel = _sunroomCalculatorViews.ScenarioView;
                    NavLabel = "Scenario Selection";
                    break;
                case 3:
                    SelectedViewModel = _sunroomCalculatorViews.FloorPlanView;
                    NavLabel = "Floor Plan";
                    break;
                case 4:
                    SelectedViewModel = _sunroomCalculatorViews.PanelView;
                    NavLabel = "Roof Info";
                    break;
                case 5:
                    SelectedViewModel = _sunroomCalculatorViews.EndCutView;
                    NavLabel = "Panel Info";
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
            _resultsModel.FormatResults();
            PitchLabel = _resultsModel.PitchLabel;
            PitchResults = _resultsModel.PitchResults;
            AttachedHeightLabel = _resultsModel.AttachedHeightLabel;
            AttachedHeightResults = _resultsModel.AttachedHeightResults;
            MaxHeightLabel = _resultsModel.MaxHeightLabel;
            MaxHeightResults = _resultsModel.MaxHeightResults;
            SoffitHeightLabel = _resultsModel.SoffitHeightLabel;
            SoffitHeightResults = _resultsModel.SoffitHeightResults;
            DripEdgeLabel = _resultsModel.DripEdgeLabel;
            DripEdgeResults = _resultsModel.DripEdgeResults;
            PanelLengthLabel = _resultsModel.PanelLengthLabel;
            PanelLengthResults = _resultsModel.PanelLengthResults;
            PanelNumberLabel = _resultsModel.PanelNumberLabel;
            PanelNumberResults = _resultsModel.PanelNumberResults;
            RoofAreaLabel = _resultsModel.RoofAreaLabel;
            RoofAreaResults = _resultsModel.RoofAreaResults;
            SelectedViewModel = _resultsView;
            _navigation = 6;
            NavLabel = "Results";
        }
    }
}