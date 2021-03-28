using Avalonia.Controls;
using SunroomCalculatorAvalonia.Views;

namespace SunroomCalculatorAvalonia.Models
{
    public class SunroomCalculatorViews
    {
        public readonly UserControl SunroomView, ScenarioView, FloorPlanView, PanelView, EndCutView, InputDefaultView;
        public SunroomCalculatorViews()
        {
            SunroomView = new SunroomView();
            ScenarioView = new ScenarioView();
            FloorPlanView = new FloorPlanView();
            PanelView = new PanelView();
            EndCutView = new EndCutView();
            InputDefaultView = new InputDefaultView();
        }
    }
}