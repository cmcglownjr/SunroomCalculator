using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SunroomCalculatorAvalonia.ViewModels;
using Splat;

namespace SunroomCalculatorAvalonia.Views
{
    public class PanelView : UserControl
    {
        public PanelView()
        {
            DataContext = Locator.Current.GetService(typeof(PanelViewModel));
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}