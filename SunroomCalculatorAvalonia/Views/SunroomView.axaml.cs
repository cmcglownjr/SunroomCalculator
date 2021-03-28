using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Splat;
using SunroomCalculatorAvalonia.ViewModels;

namespace SunroomCalculatorAvalonia.Views
{
    public class SunroomView : UserControl
    {
        public SunroomView()
        {
            DataContext = Locator.Current.GetService(typeof(SunroomViewModel));
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}