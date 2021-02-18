using System;
using System.Reactive;
using Avalonia.Controls;
using JetBrains.Annotations;
using ReactiveUI;
using SunroomCalculatorAvalonia.Models;

namespace SunroomCalculatorAvalonia.ViewModels
{
    public class GableWallHeightPitchViewModel: ViewModelBase
    {
        private string _pitchLabelText = "/12";
        private string _pitchWatermarkText = "5.5 in.";
        public ReactiveCommand<Unit, Unit> RadioAngle { get; }
        public ReactiveCommand<Unit, Unit> RadioRatio { get; }

        public GableWallHeightPitchViewModel()
        {
            RadioAngle = ReactiveCommand.Create(() => ChangePitchInput("deg", "24deg"));
            RadioRatio = ReactiveCommand.Create(() => ChangePitchInput("/12", "5.5 in."));
        }

        public void ChangePitchInput(string label, string watermark)
        {
            PitchLabelText = label;
            PitchWatermarkText = watermark;
        }
        public string PitchLabelText
        {
            get => _pitchLabelText;
            set => this.RaiseAndSetIfChanged(ref _pitchLabelText, value);
        }
        public string PitchWatermarkText
        {
            get => _pitchWatermarkText;
            set => this.RaiseAndSetIfChanged(ref _pitchWatermarkText, value);
        }
    }
}