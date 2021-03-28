using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Splat;
using SunroomCalculatorAvalonia.ViewModels;

namespace SunroomCalculatorAvalonia.Views
{
    public class FloorPlanView : UserControl
    {
        public FloorPlanView()
        {
            DataContext = Locator.Current.GetService(typeof(FloorPlanViewModel));
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}