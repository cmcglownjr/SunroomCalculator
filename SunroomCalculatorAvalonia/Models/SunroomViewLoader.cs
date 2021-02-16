using Avalonia.Controls;
using SunroomCalculatorAvalonia.Views;

namespace SunroomCalculatorAvalonia.Models
{
    public class SunroomViewLoader
    {
        public UserControl GableVM;
        public UserControl StudioVM;
        public UserControl FloorPlanVM;
        public UserControl GableWallHeightPitchVM;
        public UserControl SunroomEndCutVM;
        public SunroomViewLoader()
        {
            GableVM = new GableDiagramView();
            StudioVM = new StudioDiagramView();
            FloorPlanVM = new FloorPlanView();
            GableWallHeightPitchVM = new GableWallHeightPitchView();
            SunroomEndCutVM = new SunroomEndCutView();
        }
    }
}