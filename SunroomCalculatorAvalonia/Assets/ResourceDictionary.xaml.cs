using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using SunroomCalculatorAvalonia.ViewModels;

namespace SunroomCalculatorAvalonia.Assets
{
    public class ViewResources: ResourceDictionary
    {
        public ViewResources()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}