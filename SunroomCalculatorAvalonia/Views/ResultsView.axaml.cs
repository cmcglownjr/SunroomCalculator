using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SunroomCalculatorAvalonia.ViewModels;

namespace SunroomCalculatorAvalonia.Views
{
    public class ResultsView : UserControl
    {
        public ResultsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}