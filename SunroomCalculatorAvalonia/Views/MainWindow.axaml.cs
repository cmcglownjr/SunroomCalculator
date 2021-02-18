using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using SunroomCalculatorAvalonia.ViewModels;

namespace SunroomCalculatorAvalonia.Views
{
    public class MainWindow : Window
    {
        public ComboBox ComboSunroomType;
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            ComboSunroomType = this.Find<ComboBox>("ComboSunroomType");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}