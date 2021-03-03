using System;
using System.Collections.Generic;
using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;
using SunroomCalculatorAvalonia.Views;
using SunroomCalculatorAvalonia.Models;

namespace SunroomCalculatorAvalonia.ViewModels
{
    public delegate void ComboHandler();
    public class Input4ViewModel : ViewModelBase
    {
        private List<PanelThicknessModel> _panelThicknessModels;
        private readonly PanelThicknessCombo _panelThicknessCombo = new();
        private bool _panelThicknessEnable;
        private PanelThicknessModel _selectedThickness;
        public ReactiveCommand<Unit, Unit> PanelTypeSelect1 { get; }
        public ReactiveCommand<Unit, Unit> PanelTypeSelect2 { get; }
        public ReactiveCommand<Unit, Unit> PanelTypeSelect3 { get; }
        public ReactiveCommand<Unit, Unit> ComboSelection { get; }
        public PanelThicknessModel SelectedThickness
        {
            get => _selectedThickness;
            set => this.RaiseAndSetIfChanged(ref _selectedThickness, value);
        }
        public delegate PanelThicknessModel TestPanelThickness();
        public delegate void EventHandler(object sender, EventArgs args);
        public event EventHandler ThrowEvent = delegate{};
        public void ThicknessChanged() => ThrowEvent(this, new EventArgs());

        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ThicknessChanged();
        }
        public PanelThicknessModel GetSelectedPanel()
        {
            // ThrowEvent(SelectedThickness, new EventArgs());
            return SelectedThickness;
        }
        public bool PanelThicknessEnable
        {
            get => _panelThicknessEnable;
            set => this.RaiseAndSetIfChanged(ref _panelThicknessEnable, value);
        }
        public List<PanelThicknessModel> PanelThickness
        {
            get => _panelThicknessModels;
            set => this.RaiseAndSetIfChanged(ref _panelThicknessModels, value);
        }

        public Input4ViewModel()
        {
            // _selectedThickness = new();
            PanelTypeSelect1 = ReactiveCommand.Create(()=> OnPanelTypeChange(0));
            PanelTypeSelect2 = ReactiveCommand.Create(()=> OnPanelTypeChange(1));
            PanelTypeSelect3 = ReactiveCommand.Create(()=> OnPanelTypeChange(2));
            ComboSelection = ReactiveCommand.Create(() => ThicknessChanged());
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