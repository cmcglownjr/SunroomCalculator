using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.ReactiveUI;
using Avalonia.Utilities;
using DynamicData.Binding;
using JetBrains.Annotations;
using ReactiveUI;
using SunroomCalculatorAvalonia.Views;
using SunroomCalculatorAvalonia.Models;

namespace SunroomCalculatorAvalonia.ViewModels
{
    public class Input3ViewModel : ViewModelBase
    {
        
        private string _text1 = "";
        private string _text2 = "";
        private string _text3 = "";

        public string Text1
        {
            get => _text1;
            set => _text1 = value;
        }
        public string Text2
        {
            get => _text2;
            set => _text2 = value;
        }
        public string Text3
        {
            get => _text3;
            set => _text3 = value;
        }

        public Input3ViewModel()
        {
        }
    }
}