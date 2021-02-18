using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private ComboBox _sunroomTypeCombo;
        private UserControl _selectedViewModel;
        private UserControl _navigationWindow;
        private ObservableCollection<string> _sunroomStyles = new() {"Studio", "Gable"};
        private ObservableCollection<string> _sunroomScenarios = new()
        {
            "Wall Height & Pitch", 
            "Wall & Attached Height",
            "Max Height & Pitch",
            "Soffit & Attached Height",
            "Soffit Height & Pitch",
            "Drip Edge & Attached Height",
            "Drip Edge & Pitch"
        };
        SunroomViewLoader views = new();
        private MainWindow mainWindow = new MainWindow();
        private RadioButton testRadio;
        public ObservableCollection<string> SunroomStyles
        {
            get => _sunroomStyles;
        }

        [NotNull]
        [ItemNotNull]
        public ObservableCollection<string> SunroomScenarios => _sunroomScenarios;

        public ReactiveCommand<Unit, Unit> Diagram { get; }
        public ReactiveCommand<Unit, Unit> WallHeightPitch { get; }
        public ReactiveCommand<Unit, Unit> StudioStyle { get; }
        public ReactiveCommand<Unit, Unit> GableStyle { get; }
        public ReactiveCommand<Unit, Unit> SunroomStyleCombo { get; }

        [NotNull]
        public UserControl SelectedViewModel
        {
            get => _selectedViewModel;
            set => this.RaiseAndSetIfChanged(ref _selectedViewModel, value);
        }

        [NotNull]
        public UserControl NavigationWindow
        {
            get => _navigationWindow;
            set => this.RaiseAndSetIfChanged(ref _navigationWindow, value);
        }

        public MainWindowViewModel()
        {
            SelectedViewModel = views.GableVM;
            NavigationWindow = views.Navigation1VM;
            Diagram = ReactiveCommand.Create(() => ChangeToDiagram());
            WallHeightPitch = ReactiveCommand.Create(() => ChangeToWallHeightPitch());
            StudioStyle = ReactiveCommand.Create(() => StudioViewChange());
            GableStyle = ReactiveCommand.Create(() => GableViewChange());
            SunroomStyleCombo = ReactiveCommand.Create(() => SunroomComboChanged());
        }

        public void SunroomComboChanged()
        {
            if (mainWindow.ComboSunroomType.SelectedIndex == 0)
            {SelectedViewModel = views.StudioVM;}
            if (mainWindow.ComboSunroomType.SelectedIndex == 1)
            {SelectedViewModel = views.GableVM;}
        }
        public void StudioViewChange()
        {
            SelectedViewModel = views.StudioVM;
        }
        public void GableViewChange()
        {
            SelectedViewModel = views.GableVM;
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