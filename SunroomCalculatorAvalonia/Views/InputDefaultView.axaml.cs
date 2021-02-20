using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SunroomCalculatorAvalonia.Views
{
    public class InputDefaultView : UserControl
    {
        public InputDefaultView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}