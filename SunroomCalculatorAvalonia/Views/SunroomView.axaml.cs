using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SunroomCalculatorAvalonia.Views
{
    public class SunroomView : UserControl
    {
        public SunroomView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}