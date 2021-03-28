using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Splat;
using SunroomCalculatorAvalonia.ViewModels;

namespace SunroomCalculatorAvalonia.Views
{
    public class EndCutView : UserControl
    {
        public EndCutView()
        {
            DataContext = Locator.Current.GetService(typeof(EndCutViewModel));
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}