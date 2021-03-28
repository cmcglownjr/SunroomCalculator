using Splat;
using SunroomCalculatorAvalonia.Models;
using SunroomCalculatorAvalonia.ViewModels;

namespace SunroomCalculatorAvalonia
{
    public class AppBootstrapper
    {
        public AppBootstrapper()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            Locator.CurrentMutable.RegisterConstant(new ResultsModel(), typeof(IResultsModel));
            Locator.CurrentMutable.RegisterConstant(new DiagramModel(), typeof(DiagramModel));
            Locator.CurrentMutable.RegisterConstant(new ScenarioViewModel(), typeof(ScenarioViewModel));
            Locator.CurrentMutable.RegisterConstant(new SunroomViewModel(), typeof(SunroomViewModel));
            Locator.CurrentMutable.RegisterConstant(new FloorPlanViewModel(), typeof(FloorPlanViewModel));
            Locator.CurrentMutable.RegisterConstant(new PanelViewModel(), typeof(PanelViewModel));
            Locator.CurrentMutable.RegisterConstant(new EndCutViewModel(), typeof(EndCutViewModel));
            Locator.CurrentMutable.RegisterConstant(new MainWindowViewModel(), typeof(MainWindowViewModel));
            Locator.CurrentMutable.RegisterConstant(new MainViewModel(), typeof(MainViewModel));
        }
    }
}