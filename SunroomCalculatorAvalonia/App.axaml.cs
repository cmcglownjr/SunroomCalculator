using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SunroomCalculatorAvalonia.ViewModels;
using SunroomCalculatorAvalonia.Views;
using Splat;

namespace SunroomCalculatorAvalonia
{
    public class App : Application
    {
        public override void Initialize()
        {
            new AppBootstrapper();
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    // DataContext = new MainViewModel()
                    DataContext = Locator.Current.GetService(typeof(MainViewModel))
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}