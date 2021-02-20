using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.ReactiveUI;
using Avalonia.Utilities;
using JetBrains.Annotations;
using ReactiveUI;
using SunroomCalculatorAvalonia.Views;
using SunroomCalculatorAvalonia.Models;

namespace SunroomCalculatorAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private UserControl _selectedViewModel;
        private Image _diagramImage = new();
        private int _sunroomStyle; // Default to Studio(0), Gable is (1)
        private int _sunroomScenario;
        SunroomResources sunroomResources = new();
        [NotNull]
        public Image DiagramImage
        {
            get => _diagramImage;
            set => _diagramImage = value ?? throw new ArgumentNullException(nameof(value));
        }
        public ReactiveCommand<Unit, Unit> StudioStyle { get; }
        public ReactiveCommand<Unit, Unit> GableStyle { get; }
        public ReactiveCommand<Unit, Unit> RadioNavSunroomType { get; }
        public ReactiveCommand<Unit, Unit> RadioNavScenario { get; }
        public ReactiveCommand<Unit, Unit> RadioNavFloorPlan { get; }
        public ReactiveCommand<Unit, Unit> RadioNavRoofInfo { get; }
        public ReactiveCommand<Unit, Unit> RadioNavRoofPanel { get; }
        public ReactiveCommand<Unit, Unit> Scenario1Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario2Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario3Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario4Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario5Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario6Select { get; }
        public ReactiveCommand<Unit, Unit> Scenario7Select { get; }

        [NotNull]
        public UserControl SelectedViewModel
        {
            get => _selectedViewModel;
            set => this.RaiseAndSetIfChanged(ref _selectedViewModel, value);
        }

        public MainWindowViewModel()
        {
            DiagramImage.Source = sunroomResources.SunroomDefault;
            SelectedViewModel = sunroomResources.InputDefaultVM;
            StudioStyle = ReactiveCommand.Create(() => SunroomStyleChange(0));
            GableStyle = ReactiveCommand.Create(() => SunroomStyleChange(1));
            RadioNavSunroomType = ReactiveCommand.Create(() => NavigationChange(1));
            RadioNavScenario = ReactiveCommand.Create(() => NavigationChange(2));
            RadioNavFloorPlan = ReactiveCommand.Create(() => NavigationChange(3));
            RadioNavRoofInfo = ReactiveCommand.Create(() => NavigationChange(4));
            RadioNavRoofPanel = ReactiveCommand.Create(() => NavigationChange(5));
            Scenario1Select = ReactiveCommand.Create(() => ScenarioSelectChange(1));
            Scenario2Select = ReactiveCommand.Create(() => ScenarioSelectChange(2));
            Scenario3Select = ReactiveCommand.Create(() => ScenarioSelectChange(3));
            Scenario4Select = ReactiveCommand.Create(() => ScenarioSelectChange(4));
            Scenario5Select = ReactiveCommand.Create(() => ScenarioSelectChange(5));
            Scenario6Select = ReactiveCommand.Create(() => ScenarioSelectChange(6));
            Scenario7Select = ReactiveCommand.Create(() => ScenarioSelectChange(7));
        }
        private void NavigationChange(int navigation)
        {
            switch (navigation)
            {
                case 1:
                    SelectedViewModel = sunroomResources.Input1VM;
                    break;
                case 2:
                    SelectedViewModel = sunroomResources.Input2VM;
                    break;
                case 3:
                    SelectedViewModel = sunroomResources.Input3VM;
                    break;
                case 4:
                    SelectedViewModel = sunroomResources.Input4VM;
                    break;
                case 5:
                    SelectedViewModel = sunroomResources.Input5VM;
                    break;
                default:
                    SelectedViewModel = sunroomResources.InputDefaultVM;
                    break;
            }
        }
        private void SunroomStyleChange(int style)
        {
            switch (style)
            {
                case 0:
                    DiagramImage.Source = sunroomResources.SunroomStudio;
                    _sunroomStyle = style;
                    break;
                case 1:
                    DiagramImage.Source = sunroomResources.SunroomGable;
                    _sunroomStyle = style;
                    break;
                default:
                    DiagramImage.Source = sunroomResources.SunroomDefault;
                    break;
            }
        }

        private void ScenarioSelectChange(int scenario)
        {
            switch (_sunroomStyle)
            {
                case 0:
                    switch (scenario)
                    {
                        case 1:
                            DiagramImage.Source = sunroomResources.StudioWallPitch;
                            _sunroomScenario = scenario;
                            break;
                        case 2:
                            DiagramImage.Source = sunroomResources.StudioWallAttached;
                            _sunroomScenario = scenario;
                            break;
                        case 3:
                            DiagramImage.Source = sunroomResources.StudioMaxPitch;
                            _sunroomScenario = scenario;
                            break;
                        case 4:
                            DiagramImage.Source = sunroomResources.StudioSoffitPitch;
                            _sunroomScenario = scenario;
                            break;
                        case 5:
                            DiagramImage.Source = sunroomResources.StudioSoffitAttached;
                            _sunroomScenario = scenario;
                            break;
                        case 6:
                            DiagramImage.Source = sunroomResources.StudioDripEdgePitch;
                            _sunroomScenario = scenario;
                            break;
                        case 7:
                            DiagramImage.Source = sunroomResources.StudioDripEdgeAttached;
                            _sunroomScenario = scenario;
                            break;
                        default:
                            DiagramImage.Source = sunroomResources.SunroomDefault;
                            break;
                    }
                    break;
                case 1:
                    switch (scenario)
                    {
                        case 1:
                            DiagramImage.Source = sunroomResources.GableWallPitch;
                            _sunroomScenario = scenario;
                            break;
                        case 2:
                            DiagramImage.Source = sunroomResources.GableWallAttached;
                            _sunroomScenario = scenario;
                            break;
                        case 3:
                            DiagramImage.Source = sunroomResources.GableMaxPitch;
                            _sunroomScenario = scenario;
                            break;
                        case 4:
                            DiagramImage.Source = sunroomResources.GableSoffitPitch;
                            _sunroomScenario = scenario;
                            break;
                        case 5:
                            DiagramImage.Source = sunroomResources.GableSoffitAttached;
                            _sunroomScenario = scenario;
                            break;
                        case 6:
                            DiagramImage.Source = sunroomResources.GableDripEdgePitch;
                            _sunroomScenario = scenario;
                            break;
                        case 7:
                            DiagramImage.Source = sunroomResources.GableDripEdgeAttached;
                            _sunroomScenario = scenario;
                            break;
                        default:
                            DiagramImage.Source = sunroomResources.SunroomDefault;
                            break;
                    }
                    break;
            }
        }
    }
}