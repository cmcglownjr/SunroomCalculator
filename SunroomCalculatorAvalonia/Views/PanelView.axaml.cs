using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SunroomCalculatorAvalonia.ViewModels;

namespace SunroomCalculatorAvalonia.Views
{
    public class PanelView : UserControl
    {
        public PanelView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}