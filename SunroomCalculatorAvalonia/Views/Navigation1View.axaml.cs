using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SunroomCalculatorAvalonia.Views
{
    public class Navigation1View : UserControl
    {
        public Navigation1View()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}