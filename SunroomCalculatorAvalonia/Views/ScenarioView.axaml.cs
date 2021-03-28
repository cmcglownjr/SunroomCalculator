using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SunroomCalculatorAvalonia.ViewModels;
using Splat;

namespace SunroomCalculatorAvalonia.Views
{
    public class ScenarioView : UserControl
    {
        public ScenarioView()
        {
            DataContext = Locator.Current.GetService(typeof(ScenarioViewModel));
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}