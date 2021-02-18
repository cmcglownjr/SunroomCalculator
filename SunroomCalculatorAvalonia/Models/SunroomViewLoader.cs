using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using SunroomCalculatorAvalonia.Views;

namespace SunroomCalculatorAvalonia.Models
{
    public class SunroomViewLoader
    {
        public UserControl GableVM, StudioVM;
        public UserControl FloorPlanVM, SunroomEndCutVM, SunroomOverhangVM, SunroomPanelThicknessVM;
        public UserControl GableDripEdgeAttachedHeightVM, GableDripEdgePitchVM, GableMaxHeightPitchVM, 
            GableSoffitAttachedHeightVM, GableSoffitHeightPitchVM, GableWallAttachedHeightVM, GableWallHeightPitchVM;
        public UserControl StudioDripEdgeAttachedHeightVM, StudioDripEdgePitchVM, StudioMaxHeightPitchVM, 
            StudioSoffitAttachedHeightVM, StudioSoffitHeightPitchVM, StudioWallAttachedHeightVM, StudioWallHeightPitchVM;

        public UserControl Navigation1VM, Navigation2VM, Navigation3VM, Navigation4VM, Navigation5VM;
        public SunroomViewLoader()
        {
            GableVM = new GableDiagramView();
            StudioVM = new StudioDiagramView();
            
            FloorPlanVM = new FloorPlanView();
            SunroomEndCutVM = new SunroomEndCutView();
            SunroomOverhangVM = new SunroomOverhangView();
            SunroomPanelThicknessVM = new SunroomPanelThicknessView();
            
            GableDripEdgeAttachedHeightVM = new GableDripEdgeAttachedHeightView();
            GableDripEdgePitchVM = new GableDripEdgePitchView();
            GableMaxHeightPitchVM = new GableMaxHeightPitchView();
            GableSoffitAttachedHeightVM = new GableSoffitHeightAttachedHeightView();
            GableSoffitHeightPitchVM = new GableSoffitHeightPitchView();
            GableWallAttachedHeightVM = new GableWallHeightAttachedHeightView();
            GableWallHeightPitchVM = new GableWallHeightPitchView();
            
            StudioDripEdgeAttachedHeightVM = new StudioDripEdgeAttachedHeightView();
            StudioDripEdgePitchVM = new StudioDripEdgePitchView();
            StudioMaxHeightPitchVM = new StudioMaxHeightPitchView();
            StudioSoffitAttachedHeightVM = new StudioSoffitHeightAttachedHeightView();
            StudioSoffitHeightPitchVM = new StudioSoffitHeightPitchView();
            StudioWallAttachedHeightVM = new StudioWallHeightAttachedHeightView();
            StudioWallHeightPitchVM = new StudioWallHeightPitchView();
            
            Navigation1VM = new Navigation1View();
            Navigation2VM = new Navigation2View();
            Navigation3VM = new Navigation3View();
            Navigation4VM = new Navigation4View();
            Navigation5VM = new Navigation5View();
        }
    }
}