using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SunroomCalculatorAvalonia.Views
{
    public class WallHeightPitchView : UserControl
    {
        public WallHeightPitchView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}