using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using Avalonia.Interactivity;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using JetBrains.Annotations;
using ReactiveUI;
using SunroomCalculatorAvalonia.Views;
using SunroomCalculatorAvalonia.Models;

namespace SunroomCalculatorAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";
        private UserControl _selectedViewModel;
        SunroomViewLoader views = new SunroomViewLoader();
        public ReactiveCommand<Unit, Unit> Diagram { get; }
        public ReactiveCommand<Unit, Unit> WallHeightPitch { get; }

        [NotNull]
        public UserControl SelectedViewModel
        {
            get => _selectedViewModel;
            set => this.RaiseAndSetIfChanged(ref _selectedViewModel, value);
        }

        public MainWindowViewModel()
        {
            
            Diagram = ReactiveCommand.Create(() => ChangeToDiagram());
            WallHeightPitch = ReactiveCommand.Create(() => ChangeToWallHeightPitch());
        }

        void ChangeToWallHeightPitch()
        {
            SelectedViewModel = views.GableWallHeightPitchVM;
        }

        void ChangeToDiagram()
        {
            SelectedViewModel = views.SunroomEndCutVM;
        }
    }
}